using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	[SerializeField] int scorePerSecond = 1;

	float timer;
	int score;
	Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		scoreText.text = score.ToString();
	}
	
	public void ScoreHit(int scorePerHit)
	{
		score += scorePerHit;
		scoreText.text = score.ToString();
	}

	public void TimeBasedScore()
	{
		timer += Time.deltaTime;
		if (timer > 1.0f)
		{
			score += scorePerSecond;
			timer -= 1.0f;
		}
		scoreText.text = score.ToString();
	}
}
