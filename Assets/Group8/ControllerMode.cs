using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMode : MonoBehaviour {

	public enum Mode { Grab, PortalGun };
	public Mode currentMode;

	private Valve.VR.InteractionSystem.Hand hand;

	void Start () {
		hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
		currentMode = Mode.Grab;
	}

	void Update() {
		if (hand.controller.GetTouchDown ()) {
			Vector2 touchpad = hand.controller.GetAxis();
			PortalGun portalGun = GetComponent<PortalGun>();

			if (touchpad.y > 0.7f)
			{
				Debug.Log ("Grab Mode");
				currentMode = Mode.Grab;

				portalGun.Disable();
			}

			if (touchpad.x > 0.7f)
			{
				Debug.Log ("Portal Gun");
				currentMode = Mode.PortalGun;

				portalGun.Enable();
			}
		}
	}
}
