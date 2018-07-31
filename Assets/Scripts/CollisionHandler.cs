﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

	[Tooltip("defines which gameobject plays the death animation")][SerializeField] GameObject deathFX;
	[Tooltip("defines after how many seconds the level loads")][SerializeField] float reloadSceneDelay = 1f;

	void OnTriggerEnter(Collider collider)
	{
		StartDeathSequence();
	}

	void StartDeathSequence()
	{
		SendMessage("OnPlayerDeath");
		deathFX.SetActive(true);
		Invoke("ReloadLevel", reloadSceneDelay);
	}

	void ReloadLevel()
	{
		int currentScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
	}
}
