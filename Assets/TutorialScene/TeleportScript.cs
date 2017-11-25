using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

    public GameObject[] teleportAreas = new GameObject[4];
    private int tutorialPart = 0;

    private GameObject portal;
    private GameObject throwable;
    private GameObject food;

    // Use this for initialization
    void Start () {
        InitTutorial();
        //enable teleport point 1
        teleportAreas[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider") //enable throwables tutorial
        {
            if (tutorialPart == 2)
            {
                teleportAreas[2].SetActive(false);
                food.SetActive(true);
                throwable.SetActive(true);
                ThrowableTutorial();
            }
            tutorialPart++;
            SetTargetTeleport();
        }
    }

    //disables all teleport points and hides objects that are required for further tutorial parts
    private void InitTutorial()
    {
        foreach (GameObject area in teleportAreas)
        {
            area.SetActive(false);
        }
        portal = GameObject.Find("Portal_01");
        portal.SetActive(false);

        throwable = GameObject.Find("Throwables");
        throwable.SetActive(false);

        food = GameObject.Find("Food");
        food.SetActive(false);
    }

    private void SetTargetTeleport()
    {
        for (int i = 0; i < teleportAreas.Length; i++)
        {
            if (tutorialPart == i)
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
        ActivatePortal();
    }

    IEnumerator PortalEffect(float time)
    {
        Vector3 originalScale = portal.transform.localScale;
        Vector3 destinationScale = new Vector3(0.14f, 0.14f, 0.5f);

        float currentTime = 0.0f;

        do
        {
            portal.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        teleportAreas[tutorialPart].SetActive(true);
    }

    private void ActivatePortal()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        portal.transform.localScale = new Vector3(0, 0, 0);
        portal.SetActive(true);

        StartCoroutine(PortalEffect(1.5f));        
    }
}
