using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class FPSPick : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private GameObject dragging = null;
    private float distance = 0f;

    void Start()
    {        
    }
	
	// Update is called once per frame
	void Update () {
        GameObject leftHand = GameObject.Find("Hand1");
        GameObject rightHand = GameObject.Find("Hand2");
        //Rigidbody gameObjectsRigidBody = leftHand.AddComponent<Rigidbody>();

        if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger))
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(
                leftHand.transform.position,
                leftHand.transform.forward,
                out hitinfo,
                0.05f,
                LayerMask.GetMask("Grabbable")
               ))
            {
                dragging = hitinfo.transform.gameObject;
                distance = hitinfo.distance;
                Debug.LogFormat("Colliding %f", distance);
            }

            /*
            if (Physics.Raycast(ray, out hitinfo, 10000000, LayerMask.GetMask("Grabbable")))
            {
                dragging = hitinfo.transform.gameObject;
                distance = hitinfo.distance;
            }
             * */
        }

        if (ViveInput.GetPress(HandRole.LeftHand, ControllerButton.Trigger))
        {
            if (dragging != null)
            {
                dragging.transform.position = leftHand.transform.position;
            }
        }

        if (ViveInput.GetPressUp(HandRole.LeftHand, ControllerButton.Trigger))
        {
            if (dragging != null)
            {
                ThrowObject(leftHand);
            }
            
            dragging = null;
        }

         if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger))
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(
                rightHand.transform.position,
                rightHand.transform.forward,
                out hitinfo,
                0.05f,
                LayerMask.GetMask("Grabbable")
               ))
            {
                dragging = hitinfo.transform.gameObject;
                distance = hitinfo.distance;
                Debug.LogFormat("Colliding %f", distance);
            }

            /*
            if (Physics.Raycast(ray, out hitinfo, 10000000, LayerMask.GetMask("Grabbable")))
            {
                dragging = hitinfo.transform.gameObject;
                distance = hitinfo.distance;
            }
             * */
        }

        if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger))
        {
            if (dragging != null)
            {
                dragging.transform.position = rightHand.transform.position;
            }
        }

        if (ViveInput.GetPressUp(HandRole.RightHand, ControllerButton.Trigger))
        {
            if (dragging != null)
            {
                ThrowObject(rightHand);
            }
            
            dragging = null;
        }
    }



    void ThrowObject(GameObject hand)
    {
        Rigidbody rb = dragging.GetComponent<Rigidbody>();

        var origin = hand.transform.position;
        var controllerRb = hand.GetComponent<Rigidbody>();
        rb.velocity = controllerRb.velocity;
        rb.angularVelocity = controllerRb.angularVelocity;
    }
}
