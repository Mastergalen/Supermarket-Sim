using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

    public GameObject MapMarkerPrefab;

    private HashSet<int> scannedProductCodes = new HashSet<int>();
    private float markerSpawnHeight = 8.15f;
    private int mapOnlyLayerId = 12; // This layer won't be rendered to the VRCamera

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
                AddMapMarker(obj);
            }
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

    private void AddMapMarker(GameObject product)
    {
        GameObject marker = Instantiate(MapMarkerPrefab, product.transform);
        marker.layer = mapOnlyLayerId;

        // Position marker above the roof
        marker.transform.position = new Vector3(product.transform.position.x, markerSpawnHeight, product.transform.position.z);
    }
}
