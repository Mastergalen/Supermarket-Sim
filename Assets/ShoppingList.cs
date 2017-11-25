
using UnityEngine;
using UCL.COMPGV07;
using System.Collections.Generic;

public class ShoppingList : MonoBehaviour
{
    public GameObject checkout;
    private int[] items;
    private bool hasSpawned;
    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject[] shoppingList;

    void Start()
    {
        hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    void Update()
    {
        if (hand.controller == null) return;

        // While holding button instead of the if below
        if (!hasSpawned && hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            UCL.COMPGV07.Logging.KeyDown();
            items = checkout.GetComponent<Checkout>().experimentManager.ItemsToCollect;
            int numberOfItems = items.Length;
            shoppingList = new GameObject[numberOfItems];

            int i = 0;

            foreach (int productCode in items) {
                int index = productCode - 2;

                GameObject product = Instantiate(
                    checkout.GetComponent<Checkout>().experimentManager.Inventory[index],
                    gameObject.transform.position + new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f),
                    Quaternion.identity
                );
                Destroy(product.GetComponent<BoxCollider>());
                Destroy(product.GetComponent<CapsuleCollider>());
                Destroy(product.GetComponent<Rigidbody>());

                product.transform.parent = transform;
                product.transform.localRotation = Quaternion.Euler(57, 0, 0);
                product.transform.localPosition = new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f);
                product.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);

                shoppingList[i] = product;

                i++;
            }

            // Disable portal mode GUI while showing list
            transform.Find("ControllerGUI").gameObject.SetActive(false);

            // destroy objects
            hasSpawned = true;
        } else if (hasSpawned = hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            for(int j = 0; j < shoppingList.Length; j++) 
            {
                Destroy(shoppingList[j]);
            }

            // Re-enable controller GUI
            transform.Find("ControllerGUI").gameObject.SetActive(true);
        }
    }
}