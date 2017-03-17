using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //Public Settings
    public GameObject enemyPrefab;
    public GameObject destination;
    public EnemyDeathSoundManager sound;
    public float spawnTime;
    public float spawnDelay;

    //Local Data
    float timer;


	void Start () {
        timer = -spawnDelay;	
	}
	

	void Update () {
        timer += Time.deltaTime;

        if(timer > spawnTime)
        {
            timer = 0;
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyLocomotion>().destination = destination.transform;
            enemy.GetComponent<PlayerHealth>().sound = sound;
        }
	}
}
