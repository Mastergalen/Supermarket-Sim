using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {

    public HashSet<int> scannedProductCodes = new HashSet<int>();

    public void AddProductCode(int productCode)
    {
        if (!scannedProductCodes.Contains(productCode))
        {
            Debug.Log("Adding " + productCode);

            List<GameObject> results = FindInScene(productCode);

            Debug.Log(results);
            Debug.Log("Found " + results.Count + " game objects");
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
}
