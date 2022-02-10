using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class forMiniGame : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("MiniGame");

    }
    public void exitToMenu()
    {
        SceneManager.LoadScene("Animat");
    }
}
