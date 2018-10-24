using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {
    public GameObject[] playerArr;
    public List<GameObject> playerList = new List<GameObject>();
    private Vector3 center;
    private Camera cam;
    public float cameraEdgePadding = 2; //the size of the extra space on each side of the character
    public float minZoom = 5;
    public float maxZoom = 50;
	// Use this for initialization
	void Start () {
        cam = this.GetComponent<Camera>();
        playerArr = GameObject.FindGameObjectsWithTag("Player"); //adds all players with the tag "Player" to a list
        foreach (GameObject player in playerArr) {
            playerList.Add(player);
        }
        playerList.Add(GameObject.Find("Reticle"));
        for (int i = 0; i < playerList.Count; i++) {
            //Debug.Log(playerList[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    //Updating the camera in FixedUpdate so it moves at the same speed as the game physics
    private void FixedUpdate() {
        playerList.Clear();
        playerArr = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerArr)
        {
            playerList.Add(player);
        }
        playerList.Add(GameObject.Find("Reticle"));
        cam.transform.position = GetCenterPoint();
        Zoom();
    }

    private void Zoom() {
        Vector3 zoomPad = new Vector3(cameraEdgePadding, cameraEdgePadding, 0);
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() /maxZoom) + cameraEdgePadding;
        //Debug.Log(newZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, 0.02f);
    }

    private Vector3 GetCenterPoint() {
        int numPlayers = playerList.Count;
        center = Vector3.zero;
        foreach (GameObject player in playerList) {
            center += player.transform.position;
        }
        center /= numPlayers;
        center.z += -10;
        return center;
    }

    //Get the greatest distance between any 2 players
    private float GetGreatestDistance() {
        var bounds = new Bounds(playerList[0].transform.position, Vector3.zero); //new bounds starting at 1st player. Starts with size 0
        for (int i = 0; i < playerList.Count; i++) {
            bounds.Encapsulate(playerList[i].transform.position); //includes all players in the bounding box
        }
        float distance = Mathf.Sqrt(Mathf.Pow(bounds.size.x, 2) + Mathf.Pow(bounds.size.y, 2)); //gets the diagonal length of the bounding box (will be the bounds of the camera)
        return distance; 
    }
}
