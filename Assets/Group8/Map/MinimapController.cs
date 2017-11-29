using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

    public GameObject MapMarkerPrefab;

    private HashSet<int> scannedProductCodes = new HashSet<int>();
    private float markerSpawnHeight = 8.15f;
    private int mapOnlyLayerId = 12; // This layer won't be rendered to the VRCamera
    private GameObject Notifications;
    private Dictionary<GameObject, GameObject> productMarkerDict = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        if(MapMarkerPrefab == null)
        {
            Debug.LogError("Map marker prefab not set");
        }

        Notifications = GameObject.FindGameObjectWithTag("HUD");
    }

    void Update()
    {
        UpdateMarkerPositions();
    }

    public void AddProductCode(int productCode)
    {
        if (!scannedProductCodes.Contains(productCode))
        {
            Debug.Log("Adding " + productCode);

            List<GameObject> results = FindInScene(productCode);

            Debug.Log(results);
            Debug.Log("Found " + results.Count + " game objects");

            foreach(GameObject obj in results)
            {
                CreateMapMarker(obj);
            }

            Notifications.GetComponent<Notifications>().DisplayMessage("Added scanned items to minimap");
        }

        scannedProductCodes.Add(productCode);
    }

    private List<GameObject> FindInScene(int productCode)
    {
        ProductCode[] productCodes = FindObjectsOfType<ProductCode>();

        List<GameObject> results = new List<GameObject>();

        foreach(ProductCode p in productCodes)
        {
            if(p.Code == productCode)
            {
                results.Add(p.gameObject);
            }
        }

        return results;
    }

    private void CreateMapMarker(GameObject product)
    {
        GameObject marker = Instantiate(MapMarkerPrefab, product.transform);
        productMarkerDict.Add(product, marker);
        marker.layer = mapOnlyLayerId;
    }

    /**
     * Position marker above roof
     */
    private void UpdateMarkerPositions()
    {
        foreach (KeyValuePair<GameObject, GameObject> entry in productMarkerDict)
        {
            GameObject product = entry.Key;
            GameObject marker = entry.Value;

            marker.transform.position = new Vector3(
                product.transform.position.x,
                markerSpawnHeight,
                product.transform.position.z
            );
        }
    }
}
