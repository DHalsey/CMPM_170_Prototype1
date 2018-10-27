using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

   /* private void OnTriggerEnter2D(Collider other)
    {
        if(other == player1 || other == player2)
        {
            Destroy(gameObject);
        }
    }*/
}
