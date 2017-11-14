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

	void FixedUpdate () {
        //controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

    void Start()
    {
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
        //Debug.Log(trackedObj);
    }
	
	// Update is called once per frame
	void Update () {
        var leftHand = GameObject.Find("Hand1");
        var rightHand = GameObject.Find("Hand2");

        var camera = GetComponentInChildren<Camera>();

        Debug.DrawRay(rightHand.transform.position, rightHand.transform.forward, Color.green);
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
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
            Debug.DrawLine(rightHand.transform.position, (rightHand.transform.forward * 100) + rightHand.transform.position, Color.red);
            if (dragging != null)
            {
                dragging.transform.position = rightHand.transform.position;
            }
        }

        if (!ViveInput.GetPress(HandRole.RightHand, ControllerButton.Trigger))
        {
            dragging = null;
        }
    }
}
