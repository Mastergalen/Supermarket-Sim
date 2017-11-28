using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Minimap))]

public class ControllerMode : MonoBehaviour {

	public enum Mode { Grab, PortalGun, Scanner };
	public Mode currentMode = Mode.Grab;

    private Hand hand;
    private Teleport teleport;


    void Start () {
		hand = gameObject.GetComponent<Hand>();
        teleport = GameObject.Find("Teleporting").GetComponent<Teleport>();
    }

	void Update() {
        if (hand.controller == null) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
			Vector2 touchpad = hand.controller.GetAxis();

			if (touchpad.y > 0.7f)
			{
				UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
                Debug.Log ("Grab Mode");
				currentMode = Mode.Grab;
                SetGUI("Grab");
			}

            if (touchpad.y < -0.7f)
            {
				UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
                Debug.Log("Show Map");

                GetComponent<Minimap>().CreateMinimap();
            }

            if (touchpad.x > 0.7f)
			{
				UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
                Debug.Log ("Portal Gun");
				currentMode = Mode.PortalGun;
                SetGUI("Portal");
            }

            if (touchpad.x < -0.7f)
            {
				UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
                Debug.Log("Scanner Mode");
                currentMode = Mode.Scanner;
                SetGUI("Scanner");
            }
        }
	}

    private void SetGUI(string label)
    {
        GameObject labelObject = transform.Find("ControllerGUI").Find("ModeLabel").gameObject;

        labelObject.GetComponent<Text>().text = label;
    }
}
