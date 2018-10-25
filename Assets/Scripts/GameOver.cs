using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public void Quit () {
        Debug.Log("Restart");
        Restart();
    }

    void Restart()
    {
        SceneManager.LoadScene("SceneMain");
    }
}
