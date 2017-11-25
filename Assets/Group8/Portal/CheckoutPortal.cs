using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutPortal : MonoBehaviour {
    public GameObject portalPrefab;
    public GameObject targetPortal;    

    public float portalOutSpeed = 3.0f;
    public float spawnOffset = 0.4f;

    private float releaseCooldown = 0.5f; // Within this many seconds, the dropped item will be teleportable
    private Vector3 targetPortalForward;

    public void CreatePortal(Vector3 position, Vector3 forward)
    {
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, forward);
        rot *= Quaternion.Euler(90, 0, 0);

        // Destroy old portal if there was one
        Destroy(targetPortal);

        targetPortal = Instantiate(portalPrefab, position, rot);
        targetPortalForward = forward;
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
        if(!targetPortal)
        {
            // TODO: Trigger help tooltip, remind them to place target portal first
            Debug.Log("No target portal set");
            return;
        }

        Debug.Log("Teleporting item");

        Rigidbody rb = collidingObject.GetComponent<Rigidbody>();

        collidingObject.transform.position = targetPortal.transform.position + (targetPortalForward * spawnOffset);

        rb.velocity = targetPortalForward * portalOutSpeed;
        rb.angularVelocity = Vector3.zero;
    }
}
