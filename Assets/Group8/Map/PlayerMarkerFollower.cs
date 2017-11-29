using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarkerFollower : MonoBehaviour {

    public GameObject Player;
    public float markerHeight = 8.30f;
	
	void Start () {
		if(Player == null)
        {
            Debug.LogError("Player not set");
        }
	}
	
	void Update () {
        transform.position = new Vector3(
            Player.transform.position.x,
            markerHeight,
            Player.transform.position.z
        );
    }
}
