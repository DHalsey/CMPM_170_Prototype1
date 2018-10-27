using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {
    private Collider2D hitbox;

    private float timer;
	// Use this for initialization
	void Start () {
        timer = 20;
        hitbox = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            this.hitbox.isTrigger = false;
        }
    }
}
