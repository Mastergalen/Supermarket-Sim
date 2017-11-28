using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFollower : MonoBehaviour {

    public GameObject vrCamera;
    public float heightOffset = 0.5f;
	
	void Update () {
        Vector3 newPosition = vrCamera.transform.position;

        newPosition.y -= heightOffset;
        gameObject.transform.position = newPosition;
	}
}
