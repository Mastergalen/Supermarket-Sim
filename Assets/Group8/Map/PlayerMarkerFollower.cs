using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarkerFollower : MonoBehaviour {

    public GameObject VRCamera;
    public float markerHeight = 8.30f;
	
	void Start () {
		if(VRCamera == null)
        {
            Debug.LogError("VRCamera not set");
        }
	}
	
	void Update () {
        transform.position = new Vector3(
            VRCamera.transform.position.x,
            markerHeight,
            VRCamera.transform.position.z
        );

        // Keep it flat
        Vector3 playerOrientation = VRCamera.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(90, 0, -playerOrientation.y + 90);
    }
}
