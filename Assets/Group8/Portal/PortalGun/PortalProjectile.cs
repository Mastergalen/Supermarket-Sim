using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalProjectile : MonoBehaviour {

    public GameObject bellyPortal;

    private CheckoutPortal checkoutPortal;
    private float portalOffset = 0.3f;

	// Use this for initialization
	void Start () {
        checkoutPortal = bellyPortal.GetComponent<CheckoutPortal>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Add a slight offset to the portal position, so it's not embedded in ground
        Vector3 pos = contact.point + contact.normal * portalOffset;

        checkoutPortal.CreatePortal(contact.point, contact.normal);

        Destroy(gameObject);
    }
}
