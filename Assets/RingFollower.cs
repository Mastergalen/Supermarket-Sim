using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFollower : MonoBehaviour {

    public GameObject vrCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var newPosition = vrCamera.transform.position;

        newPosition.y -= 0.3f;
        gameObject.transform.position = newPosition;

	}
}
