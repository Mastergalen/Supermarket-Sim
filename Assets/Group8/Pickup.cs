using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }
    private GameObject objectInHand;

	// Use this for initialization
	void Start () {
        if (this.GetComponent<SteamVR_TrackedObject>() == null) {
            gameObject.AddComponent<SteamVR_TrackedObject>();
        }
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        Debug.Log("tracked start");
        Debug.Log(trackedObj);
	}

    void Awake() {
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
        //Debug.Log("tracked");
        //Debug.Log(trackedObj);
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.GetHairTriggerDown())
        {
            RaycastHit hitinfo;       
            if (Physics.Raycast(
                controller.transform.pos,
                gameObject.transform.forward,
                out hitinfo,
                0.1f,
                LayerMask.GetMask("Grabbable")
               ))
            {
                objectInHand = hitinfo.transform.gameObject;
                var distance = hitinfo.distance;
                Debug.LogFormat("Colliding %f", distance);
            }
        }

        if (controller.GetHairTrigger())
        {
            if (objectInHand != null)
            {
                objectInHand.transform.position = gameObject.transform.position;
            }
        }

        if (controller.GetHairTriggerUp())
        {
            if (objectInHand != null)
            {
                ReleaseObject();
            }
        }
	}

    void ReleaseObject()
    {
        Rigidbody rb = objectInHand.GetComponent<Rigidbody>();

        var origin = controller.transform.pos;
        rb.velocity = controller.velocity;
        rb.angularVelocity = controller.angularVelocity;
        objectInHand = null;
    }
}
