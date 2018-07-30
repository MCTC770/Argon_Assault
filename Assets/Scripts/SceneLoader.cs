using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

	int currentScene;
	int maxScene;
	[SerializeField] float loadLevelDelay = 3;

	// Use this for initialization
	void Start()
	{
		Invoke("LoadFirstLevel", loadLevelDelay);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void LoadFirstLevel()
	{
		currentScene = SceneManager.GetActiveScene().buildIndex;
		maxScene = SceneManager.sceneCountInBuildSettings - 1;

		SceneManager.LoadScene(currentScene + 1);
		if (currentScene > maxScene)
		{
			currentScene = maxScene;
		}
	}
}