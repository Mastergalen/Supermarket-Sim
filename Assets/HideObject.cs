using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {

    private GameObject portalTeleport;
    private GameObject teleport1;
    private GameObject teleport2;
    private GameObject throwableTeleport;

    // Use this for initialization
    void Start () {
        portalTeleport = GameObject.Find("PortalTeleportArea");
        teleport1 = GameObject.Find("TeleportPoint1");
        teleport2 = GameObject.Find("TeleportPoint2");
        throwableTeleport = GameObject.Find("ThrowableTeleportPoint");
        portalTeleport.SetActive(false);
        teleport2.SetActive(false);
        throwableTeleport.SetActive(false);
        teleport1.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
