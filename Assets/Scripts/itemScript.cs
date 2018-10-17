using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start () {
        player1 = GameObject.Find("player1");
        player2 = GameObject.Find("player2");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   /* private void OnTriggerEnter2D(Collider other)
    {
        if(other == player1 || other == player2)
        {
            Destroy(gameObject);
        }
    }*/
}
