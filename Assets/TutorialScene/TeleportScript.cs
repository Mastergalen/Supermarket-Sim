using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

    private GameObject[] teleportAreas;
    private int teleportCount = 0;
    private GameObject portal;

    // Use this for initialization
    void Start () {
        teleportAreas = GameObject.FindGameObjectsWithTag("Teleport");
        portal = GameObject.Find("Portal_01");
        portal.SetActive(false);
        SetTeleportStates();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if ((collisionInfo.GetComponent<Collider>().name == "HeadCollider") && (teleportCount < teleportAreas.Length - 1))
        {
            teleportCount++;
            SetTeleportStates();
        } else if ((collisionInfo.GetComponent<Collider>().name == "HeadCollider") && (teleportCount == teleportAreas.Length - 1))
        {
            ThrowableTutorial();
        } else
        {
            PortalEffect();
            Destroy(gameObject.GetComponent<BoxCollider>());
            Debug.Log(gameObject.GetComponent<BoxCollider>());
            teleportAreas[teleportCount].SetActive(true);
        }
    }

    private void SetTeleportStates()
    {
        for (int i = 0; i < teleportAreas.Length; i++)
        {
            if (teleportCount == i)
            {
                teleportAreas[i].SetActive(true);
                gameObject.GetComponent<BoxCollider>().center = new Vector3(teleportAreas[i].transform.position.x, 1.5f, teleportAreas[i].transform.position.z);
            } else
            {
                teleportAreas[i].SetActive(false);
            }
            
        }
        
    }

    private void ThrowableTutorial()
    {
        //TODO tutorial for throwables
        teleportCount++;
        SetTeleportStates();
    }

    private void PortalEffect()
    {
        return;
    }
}
