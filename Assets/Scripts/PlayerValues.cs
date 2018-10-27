using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour {

    public int percentage;
    public float infectTimer;
    public bool hasBox;

    void Update()
    {
        infectTimer -= Time.deltaTime;
        if (infectTimer <= 0)
        {
            if (percentage + 1 < 100) { percentage++; } else { percentage = 100; }
            infectTimer = 1;
        }
    }

}
