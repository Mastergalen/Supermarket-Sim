using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutPortal : MonoBehaviour {
    public GameObject targetPortal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider c)
    {
        if (!c.GetComponent<Rigidbody>()) return;

        int layer = c.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Grabbable")) return;

        Debug.Log("Start Portal collision");

        // Check if it belongs to a FixedJoint
        Debug.Log("Has fixed joint?");
        Debug.Log(c.gameObject.GetComponent<FixedJoint>());
        Teleport(c.gameObject);
    }

    private void Teleport(GameObject collidingObject)
    {
        Rigidbody rb = collidingObject.GetComponent<Rigidbody>();

        collidingObject.transform.position = targetPortal.transform.position;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
