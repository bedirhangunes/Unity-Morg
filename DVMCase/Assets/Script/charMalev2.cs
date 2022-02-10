using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using UnityEngine.Animations.Rigging;

public class charMalev2 : MonoBehaviour
{
    Animator animators;
    bool walk = false;
    bool dest = false;
    bool started = false;
    bool [] targts= new bool[6];
    int step = 0,speed=400,speed2=3;
    GameObject agentTargetPoints;
    GameObject basketb;
    GameObject basketb2;
    GameObject basketb3;
    GameObject bone;
    GameObject bone2;
    GameObject lefthandTransform;
    GameObject metallink;
    GameObject rightHandTransform;
    GameObject scalp;
    GameObject scalp2;
    GameObject scalp3;
    GameObject scalp4;
   
    GameObject scissors;
    GameObject scissors2;
    GameObject scissors3;
    GameObject scissors4;
  
    GameObject targetPoints;
    NavMeshAgent agentMesh;
  //  RigBuilder rigbuilder;
    Vector3 firstposition, targetAgent,secondposition,reposition;

    void Start()
    {
        agentMesh = GetComponent<NavMeshAgent>();
        animators = GetComponent<Animator>();
        //rigbuilder = GetComponent<RigBuilder>();
        //GetComponent<RigBuilder>().enabled = true;

        firstposition = new Vector3(1f, 0f, -3.34f);
        secondposition = new Vector3(6.1f, -0.536f,-0.045f);
        reposition = new Vector3(6.1f, 0f, -0.045f);
        transform.position = firstposition;
        targetPoints = GameObject.Find("agentPoints").gameObject;
        agentTargetPoints = GameObject.Find("agentPoints").gameObject;
      
        lefthandTransform = GameObject.Find("LeftHandTransform").gameObject;
        rightHandTransform = GameObject.Find("RightHandTransform").gameObject;
        basketb = GameObject.Find("Basket").gameObject;
       metallink = GameObject.Find("metal").gameObject;
        bone = GameObject.Find("bonesaw").gameObject;
        scalp = GameObject.Find("scalp").gameObject;
        scalp2 = GameObject.Find("scalp1").gameObject;
        scissors = GameObject.Find("scissors").gameObject;
        scissors2 = GameObject.Find("scissors1").gameObject;
        scalp3 = rightHandTransform.transform.GetChild(0).gameObject;
        scissors3 = rightHandTransform.transform.GetChild(1).gameObject;
        bone2 = rightHandTransform.transform.GetChild(2).gameObject;
        scissors4 = rightHandTransform.transform.GetChild(3).gameObject;
        scalp4 = rightHandTransform.transform.GetChild(4).gameObject;
        basketb2 = lefthandTransform.transform.GetChild(0).gameObject;
        basketb3 = metallink.transform.GetChild(0).gameObject;
        targetAgent = targetPoints.transform.GetChild(4).transform.position;

      
       // bone2.SetActive(false);
       // scalp3.SetActive(false);
       // scalp4.SetActive(false);
       // basketb.SetActive(true);
       //// lefthandTransform.transform.GetChild(0).gameObject.SetActive(false);
       // scissors3.SetActive(false);
       // scissors4.SetActive(false);
        OnEnable();


    }

    private void OnEnable()
    {
        step = 0;
        started = true;
        walk = false;
        dest = false;
       
        for (int i = 0; i < targts.Length; i++)
        {
            targts[i] = true;
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
            agentMesh.enabled = true;
            if (dest)
            {
                agentMesh.SetDestination(targetAgent);
                dest = false;
            }
            //rigbuilder.enabled = false;
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Animator>().applyRootMotion = true;
        }
        else
        {
            agentMesh.enabled = false;
            //rigbuilder.enabled = true;
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().applyRootMotion = false;
            if (step == 0 && targts[0])
            {
                StartCoroutine(starting());
            }
            if (step == 1 && targts[1])
            {
                StartCoroutine(toatTable());
            }
            if (step == 2 && targts[2])
            {
                StartCoroutine(takeScissorsonCabine());
            }
            if (step == 3&& targts[3])
            {
                StartCoroutine(erdStipDown());
            }
            if (step == 4 && targts[4])
            {
                StartCoroutine(finishAndTurnStartPoint());
            }
        }
    }
    IEnumerator starting()
    {
        targts[0] = false;
      
        yield return returnTo(4, 0, 0.02f, true);
        GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSecondsRealtime(0.05f);
        step++;
    }
    IEnumerator toatTable()
    {
        targts[1] = false;
        GetComponent<Animator>().SetBool("Walk", true);
        transform.rotation = Quaternion.Euler(0, 0, 0);
      
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(0).transform.position)) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(0).transform.position, 3 * 0.0333f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        transform.position = agentTargetPoints.transform.GetChild(0).transform.position;
     
        transform.rotation = Quaternion.Euler(0,90f, 0);
        GetComponent<Animator>().SetBool("Walk", false);
        GetComponent<Animator>().SetBool("target1", true);
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(4).gameObject.SetActive(false);
        lefthandTransform.transform.GetChild(0).gameObject.SetActive(true);
        scalp.SetActive(false);
        rightHandTransform.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        rightHandTransform.transform.GetChild(0).gameObject.SetActive(false);
        basketb2.SetActive(true);
        yield return new WaitForSeconds(0.8f);  
        GetComponent<Animator>().SetBool("target1", false);
        transform.GetChild(4).gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        lefthandTransform.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Walk", true);
        //yield return returnTo(4, 0, 0.02f, true);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        agentMesh.enabled = false;
        targetAgent = agentTargetPoints.transform.GetChild(0).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        walk = true;
        dest = true;
        step++;
    }
    IEnumerator takeScissorsonCabine()
    {
        targts[2] = false;
        GetComponent<Animator>().SetBool("Walk", true);
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(2).transform.position)) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(2).transform.position, 3 * 0.0333f);
            yield return new WaitForSeconds(0.02f);
        }
        transform.position = agentTargetPoints.transform.GetChild(2).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        GetComponent<Animator>().SetBool("target3", true);
        transform.GetChild(4).gameObject.SetActive(false);
        lefthandTransform.transform.GetChild(0).gameObject.SetActive(true);
       
        yield return new WaitForSeconds(1f);
        scissors2.SetActive(false);
        rightHandTransform.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.9f);
        rightHandTransform.transform.GetChild(1).gameObject.SetActive(false);
       // yield return new WaitForSeconds(1.1f);
       
        yield return new WaitForSeconds(1.2f);
        GetComponent<Animator>().SetBool("target3", false);
        lefthandTransform.transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        yield return returnTo(4, 90, 0.02f, true);
       // transform.rotation = Quaternion.Euler(0, 90, 0);
        GetComponent<Animator>().SetBool("Walk", true);
        agentMesh.enabled = false;
        targetAgent = agentTargetPoints.transform.GetChild(2).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        walk = true;
        dest = true;
        step++;
       
    }
    IEnumerator erdStipDown()
    {
        targts[3] = false;
        GetComponent<Animator>().SetBool("Walk", true);
        while (Mathf.Abs(Vector3.Distance(transform.position, agentTargetPoints.transform.GetChild(1).transform.position)) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position, agentTargetPoints.transform.GetChild(1).transform.position, 3 * 0.0333f);
            yield return new WaitForSeconds(0.03f);
        }
        GetComponent<Animator>().SetBool("Walk", false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.03f);
        GetComponent<Animator>().SetBool("target2", true);
      //  yield return new WaitForSeconds(0.8f);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.position = secondposition;
        lefthandTransform.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        scalp2.SetActive(false);
        // yield return new WaitForSeconds(0.6f);
        rightHandTransform.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        rightHandTransform.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        scissors.SetActive(false);
        rightHandTransform.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        rightHandTransform.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);
       
        rightHandTransform.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.09f);
        bone.SetActive(false);
        yield return new WaitForSeconds(0.99f);
       
        rightHandTransform.transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetBool("target2", false);
        transform.position = reposition;
        lefthandTransform.transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        yield return returnTo(4, 180, 0.02f, true);
        GetComponent<Animator>().SetBool("Walk", true);
        agentMesh.enabled = false;
            targetAgent=agentTargetPoints.transform.GetChild(1).transform.position;
        GetComponent<Animator>().SetBool("Walk", false);
        walk = true;
        dest = true;
        step++;
    }
    IEnumerator finishAndTurnStartPoint()
    {
        targts[4] = false;
        GetComponent<Animator>().SetBool("Walk", true);
        while (Mathf.Abs(Vector2.Distance(transform.position, agentTargetPoints.transform.GetChild(4).transform.position)) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position,agentTargetPoints.transform.GetChild(4).transform.position, 3 * 0.0333f);
            yield return new WaitForSeconds(0.02f);
        }
     //   yield return returnTo(4, -90, 0.02f, true);
        transform.position = agentTargetPoints.transform.GetChild(4).transform.position;
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(4).gameObject.SetActive(false);
       basketb3.SetActive(true);
        yield return new WaitForSeconds(0.8f);
       // yield return returnTo(4, 90, 0.02f, true);
        GetComponent<Animator>().SetBool("Walk", false);
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetBool("sit", true);
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
}
