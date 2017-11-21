using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject objectInHand;
    
	void Start () {
        hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    // Update is called once per frame
	void Update () {
        if (hand.controller == null) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            RaycastHit hitinfo;       
            if (Physics.Raycast(
                gameObject.transform.position,
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

        if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (objectInHand != null)
            {
                objectInHand.transform.position = gameObject.transform.position;
            }
        }

        if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (objectInHand != null)
            {
                ReleaseObject();
            }
        }
	}

    void ReleaseObject()
    {
        Debug.Log("Releasing object");
        Rigidbody rb = objectInHand.GetComponent<Rigidbody>();

        var origin = gameObject.transform.position;
        rb.velocity = hand.controller.velocity;
        rb.angularVelocity = hand.controller.angularVelocity;
        objectInHand = null;
    }
}
