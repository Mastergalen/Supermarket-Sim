using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject objectInHand;
    private FixedJoint joint;
    
	void Start () {
        hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    // Update is called once per frame
	void Update () {
        if (hand.controller == null) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            RaycastHit hitInfo;       
            if (Physics.Raycast(
                gameObject.transform.position,
                gameObject.transform.forward,
                out hitInfo,
                0.1f,
                LayerMask.GetMask("Grabbable")
               ))
            {
                Grab(hitInfo);
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

    void Grab(RaycastHit hitInfo) {
        objectInHand = hitInfo.transform.gameObject;
        var distance = hitInfo.distance;
        Debug.LogFormat("Colliding %f", distance);

        joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = 20000;
        joint.breakTorque = 20000;

        joint.connectedBody = hitInfo.transform.gameObject.GetComponent<Rigidbody>();        
    }

    void ReleaseObject()
    {
        Debug.Log("Releasing object");
        Rigidbody rb = objectInHand.GetComponent<Rigidbody>();

        var origin = gameObject.transform.position;
        rb.velocity = hand.controller.velocity;
        rb.angularVelocity = hand.controller.angularVelocity;
        
        objectInHand = null;
        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);
            joint = null;
        }
        //FIXME: Velocity wrong way round
    }
}
