
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

            foreach (int item in items) {
                int index = item - 2;

                GameObject product = Instantiate(
                    checkout.GetComponent<Checkout>().experimentManager.Inventory[index],
                    gameObject.transform.position + new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f),
                    Quaternion.Euler(57, 0, 0)
                    );
                Destroy(product.GetComponent<BoxCollider>());
                Destroy(product.GetComponent<CapsuleCollider>());
                Destroy(product.GetComponent<Rigidbody>());
                product.transform.parent = transform;
                product.transform.localPosition = new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f);
                product.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);

                shoppingList[i] = product;

                i++;
            }

            // destroy objects
            hasSpawned = true;
        }
    }
}