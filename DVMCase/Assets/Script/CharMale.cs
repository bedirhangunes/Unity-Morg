using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using TMPro;
using UnityEngine.SceneManagement;
public class CharMale : MonoBehaviour
{
    bool opentheDoor = false;
    bool closeTheDoor = true;
    bool walk = false;
    bool dest = false;
    bool[] targets = new bool[9];
    bool started = true;
    GameObject lamp;
    GameObject door;
    GameObject rightHandTransform;
    GameObject rightHandT;
    GameObject leftHandTransform;
    GameObject leftHandT;
    GameObject boxe;
    GameObject targetPoints;
    GameObject agentTargetPoints;
    GameObject tabl;
    GameObject morgueDoor;
    GameObject skateBoard;
    NavMeshAgent agent;
    RigBuilder rig;
    Animator animator;
    Vector3 firstPosition;
    Vector3 targetAgent;
    int step = 0;
    int speed = 400, speed2 = 3;
    GameObject lights;
    GameObject cabine;
    GameObject cabineDoor;
    void Start()
    {
        firstPosition = new Vector3(2.718f, 0, -4.158f);
        transform.position = firstPosition;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rig = GetComponent<RigBuilder>();
        GetComponent<RigBuilder>().enabled = true;
        targetPoints = GameObject.Find("agentPoint").gameObject;
        agentTargetPoints = GameObject.Find("agentPoint").gameObject;
        lamp = GameObject.Find("desk_lamp").gameObject;
        tabl = GameObject.Find("writing_board").gameObject;
        cabine = GameObject.Find("cabinet").gameObject;
        cabineDoor = cabine.transform.GetChild(0).gameObject;
        lights = lamp.transform.GetChild(5).gameObject;
        lights.SetActive(false);
        rightHandT = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
        leftHandT = transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).gameObject;
        skateBoard = transform.GetChild(3).gameObject;
        targetAgent = targetPoints.transform.GetChild(4).transform.position;
        skateBoard.SetActive(false);
        OnEnable();
    }

    private void OnEnable()
    {
        opentheDoor = true;
        closeTheDoor = true;
        step = 0;
        started = true;
        walk = false;
        dest = false;
        for(int i = 0; i < targets.Length; i++)
        {
            targets[i] = true;
        }
   

    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetAgent)) < 1 && walk)
        {
            walk = false;
        }
        if (walk)
        {
            agent.enabled = true;
            if (dest)
            {
                agent.SetDestination(targetAgent);
                dest = false;

            }
            rig.enabled = false;
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Animator>().applyRootMotion = true;
        }
        else
        {
            agent.enabled = false;
            rig.enabled = true;
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().applyRootMotion = false;
            if (step == 0 && targets[0])
            {
                StartCoroutine(startingTo());
            }
            if (step == 1 && targets[1])
            {
                StartCoroutine(openLamp());
            }
            if (step == 2 && targets[2])
            {
                StartCoroutine(closeLamp());
            }
            if (step == 3 && targets[3])
            {
                StartCoroutine(boardReverse());
            }
            if (step == 4 && opentheDoor)
            {
                StartCoroutine(cabineOpen());
            }
            //if (step == 5 && closeTheDoor)
            //{
            //    StartCoroutine(closeCabinet());
            //}
            if (step == 5 && targets[4])
            {
                StartCoroutine(gotoMenu());
            }
           
        }
    }
    IEnumerator startingTo()
    {
       // started = false;
        targets[0] = false;
       // yield return returnTo(4, 0, 0.02f, true);
        while (Mathf.Abs(Vector3.Distance(transform.position, targetAgent)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetAgent, 3 * 0.01f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        step++;
      
    }
    IEnumerator openLamp()
    {
        targets[1] = false;
        GetComponent<Animator>().SetBool("Walk", true);
       // yield return returnTo(4, -60, 0.02f, true);
        transform.rotation = Quaternion.Euler(0, 30, 0);
    
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(0).transform.position)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(0).transform.position, 3 * 0.0333f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        transform.position = agentTargetPoints.transform.GetChild(0).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        GetComponent<Animator>().enabled = true;
      GetComponent<Animator>().SetBool("lamp", true);
      
        yield return new WaitForSecondsRealtime(3f);
        lights.SetActive(true);
        GetComponent<Animator>().SetBool("lamp", false);
        yield return new WaitForSecondsRealtime(0.02f);

      

        targetAgent = agentTargetPoints.transform.GetChild(0).transform.position;
        walk = false;
        step++;

    }
    IEnumerator closeLamp()
    {
        targets[2] = false;
      //  GetComponent<Animator>().SetBool("Walk", false);
        //while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(0).transform.position)) > 0.01f)
        //{

        //    transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(0).transform.position, 3 * 0.0333f);
        //    yield return new WaitForSecondsRealtime(0.02f);

        //}
        //     yield return returnTo(4, 1, 0.02f, true);
        transform.position = agentTargetPoints.transform.GetChild(0).transform.position;
        GetComponent<Animator>().SetBool("lamp", true);
        yield return new WaitForSecondsRealtime(4f);
        lights.SetActive(false);
        GetComponent<Animator>().SetBool("lamp", false);
        yield return new WaitForSecondsRealtime(0.019f);
     //   GetComponent<Animator>().SetBool("Walk", true);
     //   yield return returnTo(4, 180, 0.02F, true);
       GetComponent<Animator>().SetBool("Walk", true);
        transform.rotation = Quaternion.Euler(0, -90, 0);
     //   skateBoard.SetActive(true);
      //  agent.enabled = false;
        targetAgent = agentTargetPoints.transform.GetChild(0).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
       
        walk = true;
        dest = true;
      
        step++;
    }
    IEnumerator boardReverse()
    {
        targets[3] = false;
     //   skateBoard.SetActive(false);
        GetComponent<Animator>().SetBool("Walk", true);
      
       transform.rotation = Quaternion.Euler(0, -90, 0);
     //   yield return returnTo(-4, -30, 02f, true);

        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(1).transform.position)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(1).transform.position, 3 * 0.0333f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = agentTargetPoints.transform.GetChild(1).transform.position;
       GetComponent<Animator>().SetBool("Walk", false);
       
        GetComponent<Animator>().SetBool("board", true);
        yield return new WaitForSecondsRealtime(0.5f);

        tabl.GetComponent<Animator>().SetBool("boardR", true);
        yield return new WaitForSecondsRealtime(4f);
        GetComponent<Animator>().SetBool("board", false);
      //  tabl.GetComponent<Animator>().SetBool("boardR", false);
        yield return new WaitForSecondsRealtime(0.02f);
        
        GetComponent<Animator>().SetBool("Walk", true);
        yield return returnTo(4, 60, 0.02f,true);
        transform.rotation = Quaternion.Euler(0, 60, 0);
        agent.enabled = false;
       
        targetAgent = agentTargetPoints.transform.GetChild(1).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        walk = true;
        dest = true;
        step++;
    }
    IEnumerator cabineOpen()
    {
        opentheDoor = false;
        GetComponent<Animator>().SetBool("Walk", true);
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(2).transform.position)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(2).transform.position, 3 * 0.0333f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        transform.position = agentTargetPoints.transform.GetChild(2).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        //    yield return returnTo(4, 0, 0.02F, true);
        transform.rotation = Quaternion.Euler(0, 0, 0);
       cabineDoor.GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetBool("open", true); 
        yield return new WaitForSeconds(1f);
        cabineDoor.GetComponent<Animator>().SetBool("openCabine", true);
        yield return new WaitForSeconds(3f);
        GetComponent<Animator>().SetBool("open", false);
        GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSeconds(0.5f);
       yield return returnTo(4, -90, 0.02f, true);
        targetAgent =agentTargetPoints.transform.GetChild(3).transform.position;
        agent.enabled = false;
        GetComponent<Animator>().SetBool("Walk", false);
       
      //  SceneManager.LoadScene("MainMenu");
        walk = true;
        dest = true;
        step++; 
    }
    IEnumerator closeCabinet()
    {
        closeTheDoor = false;
        GetComponent<Animator>().SetBool("Walk", false);

        GetComponent<Animator>().SetBool("board", true);
        cabineDoor.GetComponent<Animator>().SetBool("openCabine", false);
        cabineDoor.GetComponent<Animator>().SetBool("closeCabine", true);
        yield return new WaitForSecondsRealtime(2f);
        GetComponent<Animator>().SetBool("board", false);
        yield return new WaitForSecondsRealtime(0.02f);
        cabineDoor.GetComponent<Animator>().SetBool("closeCabine", false);
        GetComponent<Animator>().SetBool("Walk", true);
        yield return returnTo(-4, 180, 0.02f, true);
        GetComponent<Animator>().SetBool("Walk", false);
        targetAgent = agentTargetPoints.transform.GetChild(3).transform.position;
        agent.enabled = false;
     //   walk = true;
        dest = true;
        step++;

    }
    IEnumerator gotoMenu()
    {
        targets[4] = false;
        GetComponent<Animator>().SetBool("Walk", true);
        SceneManager.LoadScene("MainMenu");
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(3).transform.position)) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agent.transform.GetChild(3).transform.position, 3 * 0.0333f);
            yield return new WaitForSeconds(0.02f); 
        }
        transform.position = agentTargetPoints.transform.GetChild(3).transform.position;
       
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetBool("Walk",false);
        
        walk = false;
       // step++;
    }


    IEnumerator returnTo(float speed,int spicy,float frame,bool walked)
    {
        int times = (int)((spicy - transform.rotation.eulerAngles.y * (Mathf.Abs(speed) / speed)) / speed);
        for(int i = 0; i < Mathf.Abs(times); i++)
        {
            transform.Rotate(0, speed, 0);
            yield return new WaitForSecondsRealtime(frame);
        }
        walk = walked;
        dest = walked;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "trashforNewLevel")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
}
