using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSupermarket : MonoBehaviour {

    void OnCollisionEnter(Collision collisionInfo)
    {
        print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");

    }
}
