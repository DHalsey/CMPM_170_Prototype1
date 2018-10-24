using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentBehavior : MonoBehaviour {

    private int percent;

	void Update ()
    {
        percent = this.transform.parent.GetComponent<PlayerValues>().percentage;
        string temp= percent.ToString(); ;
        GetComponent<TextMesh>().text=temp;

	}
}
