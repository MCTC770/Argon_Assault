using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {

	[Tooltip("Defines movement speed in ms^-1")] [SerializeField] float speed = 20f;
	[Tooltip("Defines x boundaries within the ship can move in m")][SerializeField] float xRange = 4.95f;
	[Tooltip("Defines lower y boundaries within the ship can move in m")] [SerializeField] float yMin = -3.5f;
	[Tooltip("Defines upper y boundaries within the ship can move in m")] [SerializeField] float yMax = 3.6f;
	[Tooltip("Adjusts the x rotation of the spaceship based on y position")][SerializeField] float positionPitchFactor = -5f;
	[Tooltip("Adjust y tilt while moving up or down")][SerializeField] float controlPitchFactor = -15f;

	[SerializeField] float positionYawFactor = 4.5f;
	[SerializeField] float controlRollFactor = -30f;

	float xThrow;
	float yThrow;

	void Awake()
	{
		Application.targetFrameRate = 30;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		RespondToXAxisInput();
		RespondToYAxisInput();
		ProcessRotation();
	}

	private void RespondToXAxisInput()
	{
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffset = xThrow * speed * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
	}

	private void RespondToYAxisInput()
	{
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float yOffset = yThrow * speed * Time.deltaTime;

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

		transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
	}

	private void ProcessRotation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
		float yaw = transform.localPosition.x * positionYawFactor;
		float roll = xThrow * controlRollFactor;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
