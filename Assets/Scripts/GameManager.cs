using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameOver = false;
    private GameObject p1;
    private GameObject p2;
    private float timer;
    private int nextItem;
    public Transform medkit;
    public Transform box;

    private void Start()
    {
        p1 = GameObject.Find("player1");
        p2 = GameObject.Find("player2");
        timer = Random.Range(0, 5);
        nextItem = Random.Range(0, 2);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (nextItem == 0)
            {
                Instantiate(medkit, new Vector3(Random.Range(-45,40), Random.Range(-45, 35), 0), Quaternion.identity);
            }
            if (nextItem == 1)
            {
                Instantiate(box, new Vector3(Random.Range(-45, 40), Random.Range(-45, 35), 0), Quaternion.identity);
            }
            timer = Random.Range(2, 3);
            nextItem = Random.Range(0, 2);
        }
    }

    public void EndGame () 
    {
        if (gameOver == false) 
        {
            Debug.Log("End of Game");
            if (p1 != null) 
            {
                SceneManager.LoadScene("EndScene1");
            } else 
            {
                SceneManager.LoadScene("EndScene2");
            }
            gameOver = true;
        }
    }   

}
