using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour {

    // Base speed for the reticle. Multiplied by calculated values based on player percents.
    public float BaseMoveSpeed = 0.5f;

    // Stores certain player values.
    private int p1percent;
    private int p2percent;
    private GameObject p1obj;
    private GameObject p2obj;
    private PlayerValues p1vals;
    private PlayerValues p2vals;
    private Vector3 p1loc;
    private Vector3 p2loc;

    // Reticle Values
    private float forceModVertical;
    private float forceModHorizontal;
    private Rigidbody2D rb;
    private Transform tf;

    // Use this for initialization
    void Start () {
        // Get player objects
        p1obj = GameObject.Find("player1");
        p2obj = GameObject.Find("player2");
        // Get their value scripts
        p1vals = p1obj.GetComponent<PlayerValues>();
        p2vals = p2obj.GetComponent<PlayerValues>();
        // Create initial references to each player's percentage and location, probably avoids a null error somewhere
        p1percent = p1vals.percentage;
        p2percent = p2vals.percentage;
        p1loc = p1obj.transform.position;
        p2loc = p2obj.transform.position;
        // Get self components
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        forceModVertical = 0;
        forceModHorizontal = 0;
		p1percent = p1vals.percentage;
        p2percent = p2vals.percentage;
        p1loc = p1obj.transform.position;
        p2loc = p2obj.transform.position;
        forceModVertical = BaseMoveSpeed * ((Mathf.Sign(tf.position.y - p1loc.y) * p1percent) + (Mathf.Sign(tf.position.y - p2loc.y) * p2percent));
        forceModHorizontal = BaseMoveSpeed * ((Mathf.Sign(tf.position.x - p2loc.x) * p1percent) + (Mathf.Sign(tf.position.x - p2loc.x) * p2percent));
        rb.AddForce(Vector2.left * forceModHorizontal * Time.deltaTime);
        rb.AddForce(Vector2.down * forceModVertical * Time.deltaTime);
    }
}
