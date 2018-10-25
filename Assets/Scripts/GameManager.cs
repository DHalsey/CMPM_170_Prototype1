using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameOver = false;
    private GameObject p1 = GameObject.Find("player1");
    private GameObject p2 = GameObject.Find("player2");

    public void EndGame () 
    {
        if (gameOver == false) 
        {
            Debug.Log("End of Game");
            if (p1 == null && p2 != null) 
            {
                SceneManager.LoadScene("EndScene2");
            }
            else 
            {
                SceneManager.LoadScene("EndScene1");
            }
        }
    }

}
