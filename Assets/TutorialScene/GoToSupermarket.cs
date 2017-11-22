using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSupermarket : MonoBehaviour
{

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider")
        {
            //TODO: Switch to supermarket scene
            SceneManager.LoadScene("Supermarket_01");
        }
    }
}
