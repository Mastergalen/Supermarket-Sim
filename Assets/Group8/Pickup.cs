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

        if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (objectInHand != null)
            {
                ReleaseObject();
            }
        }
	}

    void Grab(RaycastHit hitInfo) {
        Debug.Log("Grabbing Object");
        objectInHand = hitInfo.transform.gameObject;

        joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = 20000;
        joint.breakTorque = 20000;

        joint.connectedBody = hitInfo.transform.gameObject.GetComponent<Rigidbody>();        
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
            joint.connectedBody = null;
            Destroy(joint);
        }
        objectInHand = null;
        //FIXME: Velocity wrong way round
    }
}
