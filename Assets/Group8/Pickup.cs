using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject objectInHand;
    private GameObject collidingObject;
    private FixedJoint joint;

	void Start () {
        hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    // Update is called once per frame
	void Update () {
        if (hand.controller == null) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(collidingObject)
            {
                Grab(collidingObject);
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

    private void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    private void SetCollidingObject(Collider c)
    {
        if(collidingObject || !c.GetComponent<Rigidbody>())
        {
            return;
        }

        int layer = c.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Grabbable")) return;

        collidingObject = c.gameObject;
    }

    void Grab(GameObject objectToGrab) {
        Debug.Log("Grabbing Object");
        objectInHand = objectToGrab;

        joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = 20000;
        joint.breakTorque = 20000;

        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        FixedJointConnection anchor = objectInHand.AddComponent<FixedJointConnection>();
        anchor.joint = joint;
    }

    void ReleaseObject()
    {
        Debug.Log("Releasing object");
        Rigidbody rbObject = objectInHand.GetComponent<Rigidbody>();
        Rigidbody rbController = gameObject.GetComponent<Rigidbody>();

        rbObject.velocity = hand.controller.velocity;
        rbObject.angularVelocity = hand.controller.angularVelocity;


        if (joint != null)
        {
            FixedJointConnection anchor = objectInHand.GetComponent<FixedJointConnection>();
            anchor.joint = null;
            anchor.timeReleased = Time.time;
            joint.connectedBody = null;
            Destroy(joint);
        }
        objectInHand = null;
    }
}
