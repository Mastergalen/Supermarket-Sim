using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFollower : MonoBehaviour {

    public GameObject vrCamera;
    public float heightOffset = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var newPosition = vrCamera.transform.position;

        newPosition.y -= heightOffset;
        gameObject.transform.position = newPosition;

	}
}
