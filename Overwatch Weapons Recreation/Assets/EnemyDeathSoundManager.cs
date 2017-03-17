using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSoundManager : MonoBehaviour {

    AudioSource deathSound;
    AudioSource[] sources;

	// Use this for initialization
	void Start () {
        sources = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        deathSound = sources[Random.Range(0, 4)];
        deathSound.Play();
    }
}
