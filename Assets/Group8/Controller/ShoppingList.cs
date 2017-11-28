
using UnityEngine;
using UCL.COMPGV07;
using System.Collections.Generic;

public class ShoppingList : MonoBehaviour
{
    public GameObject checkout;

    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject[] inventory;
    private GameObject[] shoppingList;
    private int[] items;
    private List<int> outstanding;
    private bool hasSpawned;

    void Start()
    {
        hand = gameObject.GetComponent<Valve.VR.InteractionSystem.Hand>();
        inventory = checkout.GetComponent<Checkout>().experimentManager.Inventory;
    }

    void Update()
    {
        if (hand.controller == null) return;

        // While holding button instead of the if below
        if (!hasSpawned && hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            UCL.COMPGV07.Logging.KeyDown();
            items = checkout.GetComponent<Checkout>().experimentManager.ItemsToCollect;
            outstanding = checkout.GetComponent<Checkout>().experimentManager.itemsOutstanding;
            int numberOfItems = outstanding.Count;
            shoppingList = new GameObject[numberOfItems];

            int i = 0;

            // Go through items to collect
            foreach (int productCode in items)
            {
                // Search through inventory for match
                foreach (GameObject productPrefab in inventory)
                {
                    if (productPrefab.GetComponent<ProductCode>().Code == productCode && outstanding.Contains(productCode))
                    {
                        // Spawn outstanding items in shopping list
                        GameObject product = Instantiate(
                            productPrefab,
                            gameObject.transform.position + new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f),
                            Quaternion.identity
                        );
                        Destroy(product.GetComponent<BoxCollider>());
                        Destroy(product.GetComponent<CapsuleCollider>());
                        Destroy(product.GetComponent<Rigidbody>());

                        // Attach items to controller position
                        product.transform.parent = transform;
                        product.transform.localRotation = Quaternion.Euler(57, 0, 0);
                        product.transform.localPosition = new Vector3((i * 0.05f) - ((numberOfItems * 0.05f) / 2) + 0.025f, 0, 0.065f);
                        product.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);

                        // Store spawned product
                        shoppingList[i] = product;

                        i++;

                    }
                }
            }

            // Disable portal mode GUI while showing list
            transform.Find("ControllerGUI").gameObject.SetActive(false);

            hasSpawned = true;

        }
        // Close shopping list when menu button is let go by destroying items
        else if (hasSpawned = hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            for(int j = 0; j < shoppingList.Length; j++) 
            {
                Destroy(shoppingList[j]);
            }

            // Re-enable controller GUI
            transform.Find("ControllerGUI").gameObject.SetActive(true);
        }

    }
}