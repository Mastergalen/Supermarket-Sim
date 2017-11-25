﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class ControllerMode : MonoBehaviour {

	public enum Mode { Grab, PortalGun };
	public Mode currentMode = Mode.Grab;

	private Valve.VR.InteractionSystem.Hand hand;
    private Teleport teleport;


    void Start () {
		hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
        teleport = GameObject.Find("Teleporting").GetComponent<Teleport>();
    }

	void Update() {
        if (hand.controller == null) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			Vector2 touchpad = hand.controller.GetAxis();
            PortalGun portalGun = GetComponent<PortalGun>();

			if (touchpad.y > 0.7f)
			{
                UCL.COMPGV07.Logging.KeyDown();
                Debug.Log ("Grab Mode");
				currentMode = Mode.Grab;
                SetGUI("Grab");
			}

			if (touchpad.x > 0.7f)
			{
                UCL.COMPGV07.Logging.KeyDown();
                Debug.Log ("Portal Gun");
				currentMode = Mode.PortalGun;
                SetGUI("Portal");
            }
		}
	}

    void SetGUI(string label)
    {
        GameObject labelObject = transform.Find("ControllerGUI").Find("ModeLabel").gameObject;

        labelObject.GetComponent<Text>().text = label;
    }
}