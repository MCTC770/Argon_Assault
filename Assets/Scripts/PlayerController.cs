﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	[Header("Movement")]
	[Tooltip("Defines movement speed in ms^-1")] [SerializeField] float controlSpeed = 20f;
	[Tooltip("Defines x boundaries within the ship can move in m")][SerializeField] float xRange = 4.95f;
	[Tooltip("Defines lower y boundaries within the ship can move in m")] [SerializeField] float yMin = -3.5f;
	[Tooltip("Defines upper y boundaries within the ship can move in m")] [SerializeField] float yMax = 3.6f;

	[Header("Rotations")]
	[Tooltip("Adjusts the x rotation of the spaceship based on y position")][SerializeField] float positionPitchFactor = -5f;
	[Tooltip("Adjust y tilt while moving up or down")][SerializeField] float controlPitchFactor = -15f;
	[Tooltip("Adjust y rotation based on x position")] [SerializeField] float positionYawFactor = 4.5f;
	[Tooltip("Adjust z tilt while moving left or right")] [SerializeField] float controlRollFactor = -30f;

	[Header("Shooting")]
	[SerializeField] GameObject[] guns;
	[SerializeField] ParticleSystem gunParticleLeft;
	[SerializeField] ParticleSystem gunParticleRight;
	[SerializeField] float emissionOverTime = 5f;

	ScoreBoard scoreBoard;

	float xThrow;
	float yThrow;
	bool controlEnabled = true;

	void Awake()
	{
		Application.targetFrameRate = 30;
	}

	private void Start()
	{
		scoreBoard = FindObjectOfType<ScoreBoard>();
	}

	// Update is called once per frame
	void Update ()
	{
		RespondToXAxisInput();
		RespondToYAxisInput();
		RespondToFireInput();
		ProcessRotation();
		AddTimebaseScore();
	}

	void OnPlayerDeath()
	{
		controlEnabled = false;
		foreach (GameObject gun in guns)
		{
			gun.SetActive(false);
		}
	}

	void RespondToXAxisInput()
	{
		if (controlEnabled == true)
		{
			xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		}

		float xOffset = xThrow * controlSpeed * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
	}

	void RespondToYAxisInput()
	{
		if (controlEnabled == true)
		{
			yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		}
		
		float yOffset = yThrow * controlSpeed * Time.deltaTime;

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

		transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
	}

	private void RespondToFireInput()
	{
		var emissionGunLeft = gunParticleLeft.emission;
		var emissionGunRight = gunParticleRight.emission;

		if (CrossPlatformInputManager.GetButton("Fire"))
		{
			emissionGunLeft.rateOverTime = emissionOverTime;
			emissionGunRight.rateOverTime = emissionOverTime;
		}
		else
		{
			emissionGunLeft.rateOverTime = 0f;
			emissionGunRight.rateOverTime = 0f;
		}
	}

	void ProcessRotation()
	{
		float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
		float yaw = transform.localPosition.x * positionYawFactor;
		float roll = xThrow * controlRollFactor;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	void AddTimebaseScore()
	{
		scoreBoard.TimeBasedScore();
	}
}
