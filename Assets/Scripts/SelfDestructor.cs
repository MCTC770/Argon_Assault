using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour {

	[Tooltip ("time after which death FX gets destroyed after initialization")][SerializeField] float deathFXDestroyDelay = 1f;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, deathFXDestroyDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
