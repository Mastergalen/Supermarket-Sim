using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSupermarket : MonoBehaviour {

    private void OnTriggerEnter(Collider collisionInfo)
    {
        // TODO: Check it's only "HeadCollider"
        Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.GetComponent<Collider>().name);

        //TODO: Switch to supermarket scene
    }
}
