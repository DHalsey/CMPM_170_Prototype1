using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentBehavior : MonoBehaviour {

    private int percent;
    // Use this for initialization
    void Start ()
    {
        
        //percent = PlayerValues.GetPercent();
    }
	
	// Update is called once per frame
	void Update ()
    {
        percent = this.transform.parent.GetComponent<PlayerValues>().percentage;
        string temp= percent.ToString(); ;
        GetComponent<TextMesh>().text=temp;

	}
}
