using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

    public string nextTeleportName;
    private GameObject nextTeleport;

    public string currentTeleportName;
    private GameObject currentTeleport;

    private GameObject portalTeleport;
    private GameObject teleport2;
    private GameObject throwableTeleport;

    // Use this for initialization
    void Start () {
        portalTeleport = GameObject.Find("PortalTeleportArea");
        teleport2 = GameObject.Find("TeleportPoint2");
        throwableTeleport = GameObject.Find("ThrowableTeleportPoint");

        portalTeleport.SetActive(false);
        teleport2.SetActive(false);
        throwableTeleport.SetActive(false);

        currentTeleport = GameObject.Find(currentTeleportName);
        currentTeleport.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider")
        {
            Debug.Log("alskdjlaksjdlaskjd");
            nextTeleport = GameObject.Find(nextTeleportName);
            currentTeleport = GameObject.Find(currentTeleportName);
            nextTeleport.SetActive(true);
            currentTeleport.SetActive(false);
        }
    }
}
