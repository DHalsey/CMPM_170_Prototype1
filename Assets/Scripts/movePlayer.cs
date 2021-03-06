﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {
    public int player;
    public Transform wall;
    private Rigidbody2D rb; //the rigidbody of the player
    private SpriteRenderer sr; //the sprite renderer of the player
    private bool keyLeft, keyRight, keyUp, keyDown, keyItem;
    private float movementSpeed = 4000.0f;
    private GameObject p1, p2;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        p1 = GameObject.Find("player1");
        p2 = GameObject.Find("player2");
    }
	
	// Update is called once per frame
	void Update () {
        GetInputs(); //gets the player inputs
        MovePlayer();
        //sr.sortingOrder = -(int)(transform.position.y*5); //This line SHOULD fix draw order issues but doesnt. IDK why - Dustin
	}

    //gets the inputs of the player.
    //This is used so we can support the same script to work with different player inpuits
    void GetInputs() {     
        if (player == 1) { // if this is player 1, get player 1 controls
            keyLeft = Input.GetKey(KeyCode.A);
            keyRight = Input.GetKey(KeyCode.D);
            keyUp = Input.GetKey(KeyCode.W);
            keyDown = Input.GetKey(KeyCode.S);
            keyItem = Input.GetKey(KeyCode.Space);
        } else if (player == 2) { // if this is player 2, get player 2 controls
            keyLeft = Input.GetKey(KeyCode.LeftArrow);
            keyRight = Input.GetKey(KeyCode.RightArrow);
            keyUp = Input.GetKey(KeyCode.UpArrow);
            keyDown = Input.GetKey(KeyCode.DownArrow);
            keyItem = Input.GetKey(KeyCode.Keypad0);
        }
    }

    //moves the player based on inputted keys
    //Note that you should ALWAYS move a player by using it's rigidbody, NOT it's position.
    //if you move the player's position instead, physics go whack
    void MovePlayer() {
        //speeds are multiplied by delta time to guarantee same movement speed regardless of fps
        if (keyLeft == true) {
            rb.AddForce(Vector2.left * movementSpeed * Time.deltaTime);
        }
        if (keyRight == true) {
            rb.AddForce(Vector2.right*movementSpeed * Time.deltaTime);
        }
        if (keyUp == true) {
            rb.AddForce(Vector2.up*movementSpeed * Time.deltaTime);
        }
        if (keyDown == true) {
            rb.AddForce(Vector2.down*movementSpeed * Time.deltaTime);
        }
        if (keyItem == true && GetComponent<PlayerValues>().hasBox == true)
        {
            Instantiate(wall, this.gameObject.transform.position, Quaternion.identity);
            GetComponent<PlayerValues>().hasBox = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameObject obj = other.gameObject;
            if (player == 1)
            {
                if (obj.name == "Medkit(Clone)")
                {
                    if (p1.GetComponent<PlayerValues>().percentage - 10 > 0) { p1.GetComponent<PlayerValues>().percentage -= 10; } else { p1.GetComponent<PlayerValues>().percentage = 0; }
                }
                else if (obj.name == "Box(Clone)")
                {
                    if (p1.GetComponent<PlayerValues>().hasBox == false) { p1.GetComponent<PlayerValues>().hasBox = true; }
                }
                Destroy(obj);
            } else if (player == 2) {
                if (obj.name == "Medkit(Clone)")
                {
                    if (p2.GetComponent<PlayerValues>().percentage - 10 > 0) { p2.GetComponent<PlayerValues>().percentage -= 10; } else { p2.GetComponent<PlayerValues>().percentage = 0; }
                }
                else if (obj.name == "Box(Clone)")
                {
                    if (p2.GetComponent<PlayerValues>().hasBox == false) { p2.GetComponent<PlayerValues>().hasBox = true; }
                }
                Destroy(obj);
            }
        }
    }

    private void OnGUI() {
        if (player == 1)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Percentage: " + p1.GetComponent<PlayerValues>().percentage);
        }
        else if (player == 2)
        {
            GUI.Label(new Rect(100, 10, 100, 20), "Percentage: " + p2.GetComponent<PlayerValues>().percentage);
        }
    }
}
