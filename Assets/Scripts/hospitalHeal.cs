using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class hospitalHeal : MonoBehaviour {
    public Slider slider;
    private float maxHeals = 100; //the max healing the hospital can hold at one time
    private float currentHeals = 100;
    private float healsPerTick = 10; //amount healed per tick
    private float tickSpeed = 1; //ticks per second
    private float rechargeDelay = 5; //in seconds
    private float rechargeRate = 10; //the recharge rate of heals per second that the hospital will gain
    private float timeSinceEmpty = 0;// the time (in seconds) that the hospital has not been occupied by a player
    private List<GameObject> playerObjectList = new List<GameObject>(); // a list of players in the heal
	// Use this for initialization
	void Start () {
        UpdateHealBar();
	}
	
	// Update is called once per frame
	void Update () {
        Recharge();
        DecreasePercent();
	}

    private void Recharge() { 
        if (playerObjectList.Count >= 1) {
            timeSinceEmpty = 0;
        } else {
            timeSinceEmpty += Time.deltaTime;
        }

        if (timeSinceEmpty >= rechargeDelay) {
            currentHeals += rechargeRate * Time.deltaTime;
            UpdateHealBar();
        }
        //avoid reacharging past cap
        if (currentHeals > maxHeals) {
            currentHeals = maxHeals;
        }
    }

    private float timeSinceHeal = 0;
    //transfers healing to the players in the hospital
    private void DecreasePercent() {
        timeSinceHeal += Time.deltaTime;

        if(timeSinceHeal >= tickSpeed && playerObjectList.Count>=1) { //if it is time to tick another heal, then heal the players
            int numPlayers = playerObjectList.Count;
            float healPerPlayer = 0;

            //splits the heals between players
            if(healsPerTick <= currentHeals) {
                healPerPlayer = healsPerTick / numPlayers; //use healsPerTick if heals per tick is less than the remaining
            } else {
                healPerPlayer = currentHeals / numPlayers; //use remaining heals if heals per tick is greater than the remaining
            }
            
            //Heal the player as long as their percentage does not go below 0
            foreach (GameObject player in playerObjectList) {
               if( player.GetComponent<PlayerValues>().percentage - (int)healPerPlayer >= 0) {
                    player.GetComponent<PlayerValues>().percentage -= (int)healPerPlayer;
                } else {
                    player.GetComponent<PlayerValues>().percentage = 0;
                }             
            }

            currentHeals -= healsPerTick;
            UpdateHealBar();
            timeSinceHeal = 0;
        }
        
    }

    private void UpdateHealBar() {
        slider.value = currentHeals / maxHeals;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player") { //makes sure to only add players
            if (!playerObjectList.Contains(obj)) { //if the player isnt in the playerlist
                playerObjectList.Add(obj); //add the player to the list              
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
    }
    private void OnTriggerExit2D(Collider2D collision) {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Player") { //makes sure to only add players
            if (playerObjectList.Contains(obj)) { //if the player is in the playerlist
                playerObjectList.Remove(obj); //remove the player
            }
        }
    }
}
