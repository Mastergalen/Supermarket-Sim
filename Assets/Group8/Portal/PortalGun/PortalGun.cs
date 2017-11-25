using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {

    public GameObject bellyPortal;
    public GameObject projectilePrefab;
    public int projectileSpeed = 10;

	private Valve.VR.InteractionSystem.Hand hand;

	void Start () {
		hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
	}
	
	void Update () {
        if (hand.controller == null) return;

        ControllerMode.Mode currentMode = GetComponent<ControllerMode>().currentMode;

		if (currentMode != ControllerMode.Mode.PortalGun) return;

		if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
            Debug.Log("Shoot portal gun");
			UCL.COMPGV07.Logging.KeyDown();

			ShootPortal();
		}
	}

	void ShootPortal() {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        projectile.GetComponent<PortalProjectile>().bellyPortal = bellyPortal;

        Destroy(projectile, 10.0f);
    }
}
