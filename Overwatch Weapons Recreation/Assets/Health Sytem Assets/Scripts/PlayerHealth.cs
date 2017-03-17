using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    //Public Settings
    public int totalHealth;
    public EnemyDeathSoundManager sound;

    //InGame Objects
    GameObject healthbar; //UI Component of the healthbar

    //Player Data
    float currentHealth;
    AudioSource deathSound;


	void Start () {
        currentHealth = totalHealth;
        healthbar = transform.GetChild(0).transform.GetChild(1).gameObject;

	}
	

    //Decrease this player's current health by hitVal
    public void decreaseHealth(float hitVal)
    {
        if (currentHealth > 0)
        {
            //Subtract hitVal amount of health
            currentHealth -= hitVal;

            //Calculate the ratio of health for the healthbar
            float healthRatio = (float)currentHealth / (float)totalHealth;
            
            //Set the proper position of the healthbar
            healthbar.transform.localScale = new Vector3(healthRatio, 1, 1);
            Vector3 healthbarPos = healthbar.transform.position;
            healthbarPos.x = healthbar.transform.parent.transform.position.x + healthRatio-1;
            healthbar.transform.position = healthbarPos;
        }
        else
        {
            sound.Play();
            Destroy(gameObject);
        }

    }
}
