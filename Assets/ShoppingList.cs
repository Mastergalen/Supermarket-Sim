
using UnityEngine;
using UCL.COMPGV07;
using System.Collections.Generic;

public class ShoppingList : MonoBehaviour
{

    public GameObject checkout;
    private int[] items;
    private int i = 0;
    private bool hasSpawned;

    void Start() {
    }

    void Update()
    {
        // While holding button instead of the if below
        if (!hasSpawned) {

            items = checkout.GetComponent<Checkout>().experimentManager.ItemsToCollect;
            int numberOfItems = items.Length;
            GameObject[] shoppingList = new GameObject[numberOfItems];
            FixedJoint[] joints = new FixedJoint[numberOfItems];

            foreach (int item in items) {

                int index = item - 2;

                GameObject product = Instantiate(
                    checkout.GetComponent<Checkout>().experimentManager.Inventory[index],
                    gameObject.transform.position + new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f),
                    Quaternion.Euler(58, 0, 0)
                    );

                product.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);

                product.transform.parent = gameObject.transform;
                
                joints[i] = gameObject.AddComponent<FixedJoint>();
                joints[i].breakForce = 20000;
                joints[i].breakTorque = 20000;
                joints[i].connectedBody = product.GetComponent<Rigidbody>();

                shoppingList[i] = product;

                i++;
            }

            // destroy objects
            hasSpawned = true;
        }
    }
}