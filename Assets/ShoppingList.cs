
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
        items = checkout.GetComponent<Checkout>().experimentManager.ItemsToCollect;

        // Using Start () does not work.
        if (!hasSpawned)
        {
            foreach (int item in items)
            {
                // One of the prodcts in the XML has code 56, but there are only 55 items in Inventory
                int index = item - 1;
                if (index > 54) {
                    index = 54;
                }
                Instantiate(
                    checkout.GetComponent<Checkout>().experimentManager.Inventory[index],
                    gameObject.transform.position + new Vector3(0, i / 10, 0),
                    Quaternion.identity
                );
                // Change quat.id if rotation is messed up
                i++;
            }
            hasSpawned = true;
        }
    }
}