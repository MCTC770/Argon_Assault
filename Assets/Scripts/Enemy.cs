using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] ParticleSystem enemyExplosion;
	[SerializeField] AudioSource enemyExplosionSound;
	[SerializeField] Renderer enemyBody;
	[SerializeField] Collider enemyCollider;

	// Use this for initialization
	void Start () {
		enemyExplosion = GetComponent<ParticleSystem>();
		enemyExplosionSound = GetComponent<AudioSource>();
		enemyBody = GetComponent<Renderer>();
		enemyCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other)
	{
		enemyExplosion.Play();
		enemyExplosionSound.Play();
		enemyBody.enabled = false;
		enemyCollider.enabled = false;
		print("Particles collided with enemy " + gameObject.name);
	}
}
