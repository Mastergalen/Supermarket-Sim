
using UnityEngine;
using UCL.COMPGV07;
using System.Collections.Generic;

public class ShoppingList : MonoBehaviour
{

    public GameObject checkout;
    private int[] items;
    private int i = 0;
    private bool hasSpawned;

    void Start()
    {
    }

    void Update()
    {
        // Using Start () does not work.
        if (!hasSpawned)
        {
            items = checkout.GetComponent<Checkout>().experimentManager.ItemsToCollect;
            GameObject[] shoppingList = new GameObject[items.Length];
            FixedJoint[] joints = new FixedJoint[items.Length];
            foreach (int item in items)
            {
                // One of the prodcts in the XML has code 56, but there are only 55 items in Inventory
                int index = item - 1;
                if (index > 54) {
                    index = 54;
                }
                Debug.Log("Spawning product");
                GameObject product = Instantiate(checkout.GetComponent<Checkout>().experimentManager.Inventory[index], gameObject.transform.position + new Vector3(0, (i * 0.1f), 0), Quaternion.identity);
                Debug.Log(product);

                product.transform.parent = gameObject.transform;
                // Change quat.id if rotation is messed up
                joints[i] = gameObject.AddComponent<FixedJoint>();
                joints[i].breakForce = 20000;
                joints[i].breakTorque = 20000;
                joints[i].connectedBody = product.GetComponent<Rigidbody>();

                shoppingList[i] = product;
                i++;
            }
            hasSpawned = true;
        }
    }
}