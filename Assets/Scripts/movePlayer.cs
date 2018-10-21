using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {
    public int player;
    private Rigidbody2D rb; //the rigidbody of the player
    private SpriteRenderer sr; //the sprite renderer of the player
    private bool keyLeft, keyRight, keyUp, keyDown;
    private float movementSpeed = 4000.0f;
    public int player1score, player2score;
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
        } else if (player == 2) { // if this is player 2, get player 2 controls
            keyLeft = Input.GetKey(KeyCode.LeftArrow);
            keyRight = Input.GetKey(KeyCode.RightArrow);
            keyUp = Input.GetKey(KeyCode.UpArrow);
            keyDown = Input.GetKey(KeyCode.DownArrow);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (player == 1)
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<itemScript>().timer = Random.Range(10, 20);
                player1score++;
                p1.GetComponent<PlayerValues>().percentage -= 10;
                p2.GetComponent<PlayerValues>().percentage += 10;
            } else if (player == 2) {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<itemScript>().timer = Random.Range(10, 20);
                player2score++;
                p1.GetComponent<PlayerValues>().percentage += 10;
                p2.GetComponent<PlayerValues>().percentage -= 10;
            }
        }
    }

    private void OnGUI() {
        if (player == 1)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Score: " + player1score);
        }
        else if (player == 2)
        {
            GUI.Label(new Rect(100, 10, 100, 20), "Score: " + player2score);
        }
    }
}
