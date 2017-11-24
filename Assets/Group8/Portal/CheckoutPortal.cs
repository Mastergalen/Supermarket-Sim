using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutPortal : MonoBehaviour {
    public GameObject targetPortal;
    private float releaseCooldown = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider c)
    {
        if (!c.GetComponent<Rigidbody>()) return;

        int layer = c.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Grabbable")) return;

        FixedJointConnection connection = c.gameObject.GetComponent<FixedJointConnection>();

        // Check if player is dropping the item in
        if (connection)
        {
            if((Time.time - connection.timeReleased) < releaseCooldown)
            {
                Teleport(c.gameObject);
            }
        }
        
    }

    private void Teleport(GameObject collidingObject)
    {
        Rigidbody rb = collidingObject.GetComponent<Rigidbody>();

        collidingObject.transform.position = targetPortal.transform.position;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
