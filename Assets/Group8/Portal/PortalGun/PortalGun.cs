using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {

	private Valve.VR.InteractionSystem.Hand hand;
	private SteamVR_LaserPointer laser;

	// Use this for initialization
	void Start () {
		hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
		laser = GetComponent<SteamVR_LaserPointer>();
	}
	
	// Update is called once per frame
	void Update () {
		ControllerMode.Mode currentMode = GetComponent<ControllerMode> ().currentMode;

		if (currentMode != ControllerMode.Mode.PortalGun) return;

		if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			UCL.COMPGV07.Logging.KeyDown();
			RaycastHit hit;

			if (Physics.Raycast (
				   hand.controller.transform.pos,
				   gameObject.transform.forward,
				   10,
				   out hit
			   )) {
				Debug.Log ("Hit");
				Debug.Log (hit.point);
				Debug.DrawLine (hand.controller.transform.pos, hit.point);

				ShootPortal (hit);
			}
		}
	}

	public void Enable() {
		laser.active = true;
	}

	public void Disable() {
		laser.active = false;
	}

	void ShootPortal(RaycastHit hit) {

	}
}
