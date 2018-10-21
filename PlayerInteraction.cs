using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Canvas messageCanvas;
    void Start()
    {
        messageCanvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player1")
        {
            Debug.Log(collision.name);
            TurnOnMessage();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player2")
        {
            TurnOffMessage();
        }
    }

    private void TurnOnMessage()
    {
        messageCanvas.enabled = true;
    }
    private void TurnOffMessage()
    {
        messageCanvas.enabled = false;
    }
}