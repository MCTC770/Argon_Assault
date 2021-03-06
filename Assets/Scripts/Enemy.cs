﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Tooltip("gameobject with the enemy death sequence")][SerializeField] GameObject deathFX;
	[Tooltip("all instatiated enemy explosions will get childed to this transform")][SerializeField] Transform spawnAtRuntime;

	[SerializeField] int scorePerHit = 100;
	[SerializeField] int hits = 3;

	ScoreBoard scoreBoard;
	Collider enemyCollider;

	// Use this for initialization
	void Start ()
	{
		AddBoxCollider();
		scoreBoard = FindObjectOfType<ScoreBoard>();
	}

	private void AddBoxCollider()
	{
		enemyCollider = gameObject.AddComponent<BoxCollider>();
		enemyCollider.isTrigger = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		if (hits <= 0)
		{
			KillEnemy();
		}
	}

	private void ProcessHit()
	{
		scoreBoard.ScoreHit(scorePerHit);
		hits--;
	}

	private void KillEnemy()
	{
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = spawnAtRuntime;
		Destroy(gameObject);
	}
}
