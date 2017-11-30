using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PortalGun : MonoBehaviour {

    public GameObject bellyPortal;
    public GameObject projectilePrefab;
	public GameObject PortalArea = null;
    public int projectileSpeed = 10;

	private Valve.VR.InteractionSystem.Hand hand;
	private GameObject Notifications;
	private AudioSource audioSource;

	void Start() {
		hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
		Notifications = GameObject.FindGameObjectWithTag("HUD");
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update()
	{
        if (hand.controller == null) return;

        ControllerMode.Mode currentMode = GetComponent<ControllerMode>().currentMode;

		if (currentMode != ControllerMode.Mode.PortalGun) return;

		if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();

			if (PortalArea != null)
			{
				Bounds portalAreaBounds = PortalArea.GetComponent<Renderer> ().bounds;

				if (!portalAreaBounds.Contains(transform.position))
				{
					Debug.Log ("Not in portal area");
					Notifications.GetComponent<Notifications>().DisplayMessage("To place your portal, go to the starting area");
					return;
				}
			}

			ShootPortal();
		}
	}

	void ShootPortal() {
		Debug.Log("Shoot portal gun");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        projectile.GetComponent<PortalProjectile>().bellyPortal = bellyPortal;
		audioSource.Play();

        Destroy(projectile, 10.0f);
    }
}
