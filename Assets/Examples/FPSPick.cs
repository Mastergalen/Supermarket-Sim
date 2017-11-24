using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private GameObject dragging = null;
    private float distance = 0f;
	
	// Update is called once per frame
	void Update () {

        var camera = GetComponentInChildren<Camera>();
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, 10000000, LayerMask.GetMask("Grabbable")))
            {
                dragging = hitinfo.transform.gameObject;
                distance = hitinfo.distance;
            }
        }

        if(Input.GetMouseButton(0))
        {
            if(dragging != null)
            {
                dragging.transform.position = ray.origin + (ray.direction * distance);
            }

            UCL.COMPGV07.Logging.KeyDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = null;
        }
    }
}
