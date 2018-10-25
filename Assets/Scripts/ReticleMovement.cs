using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour {

    // Base speed for the reticle. Multiplied by calculated values based on player percents.
    public float BaseMoveSpeed = 1f;
    public float timer = 0;

    // Stores certain player values.
    private GameObject p1obj;
    private GameObject p2obj;

    // List of players to be tracked
    private List<GameObject> playerObjectList = new List<GameObject>();

    // Reticle Values
    private float forceModVertical;
    private float forceModHorizontal;
    private int HighestPercent;
    private Rigidbody2D rb;
    private Transform tf;

    // Use this for initialization
    void Start () {
        // Get player objects
        p1obj = GameObject.Find("player1");
        if (p1obj != null) { playerObjectList.Add(p1obj); }
        p2obj = GameObject.Find("player2");
        if (p2obj != null) { playerObjectList.Add(p2obj); }
        // Get self components
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        timer = 30;
    }
	
	// Update is called once per frame
	void Update () {
        forceModVertical = 0;
        forceModHorizontal = 0;
        HighestPercent = 0;
        foreach (GameObject player in playerObjectList)
        {
            forceModVertical += (Mathf.Sign(tf.position.y - player.transform.position.y) * player.GetComponent<PlayerValues>().percentage);
            forceModHorizontal += (Mathf.Sign(tf.position.x - player.transform.position.x) * player.GetComponent<PlayerValues>().percentage);
            // Update highest percentage
            if (player.GetComponent<PlayerValues>().percentage > HighestPercent) { HighestPercent = player.GetComponent<PlayerValues>().percentage; }
        }
        BaseMoveSpeed = 1f + (HighestPercent / 5f);
        forceModVertical = BaseMoveSpeed * forceModVertical;
        forceModHorizontal = BaseMoveSpeed * forceModHorizontal;
        rb.AddForce(Vector2.left * forceModHorizontal * Time.deltaTime);
        rb.AddForce(Vector2.down * forceModVertical * Time.deltaTime);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0 || playerObjectList.Count == 1)
        {
            FindObjectOfType<GameManager>().EndGame();
            //timer = 30;
        }
    }

    // Timer
    private void OnGUI() {
        GUI.Label(new Rect(1000, 10, 100, 20), "" + timer);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (timer <= 0.1) {
            if (obj.tag == "Player"){
                playerObjectList.Remove(obj);
                Destroy(obj);
            }
        }
    }
}
