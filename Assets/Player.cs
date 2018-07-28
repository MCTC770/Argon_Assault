using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {

	[Tooltip("Defines movement speed in ms^-1")] [SerializeField] float xSpeed = 4f;
	[Tooltip("Defines x boundaries within the ship can move in m")][SerializeField] float xRange = 4f;
	[Tooltip("Defines lower y boundaries within the ship can move in m")] [SerializeField] float yMin = 4f;
	[Tooltip("Defines upper y boundaries within the ship can move in m")] [SerializeField] float yMax = 4f;

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
		RespondToXAxisInput();
		RespondToYAxisInput();
	}

	private void RespondToXAxisInput()
	{
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffset = xThrow * xSpeed * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
	}

	private void RespondToYAxisInput()
	{
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float yOffset = yThrow * xSpeed * Time.deltaTime;

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

		transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
	}
}
