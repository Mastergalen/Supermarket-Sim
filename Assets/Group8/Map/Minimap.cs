using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Minimap : MonoBehaviour {

    public GameObject MinimapPrefab;
    public GameObject VRCamera;
    public GameObject MapCamera;
    public float distance = -1.0f;
    
    private Hand hand;
    private GameObject minimapInstance;
    private Camera camera;

    void Start () {
        hand = GetComponent<Hand>();

        if (MapCamera == null)
        {
            Debug.LogError("MapCamera not set");
        }

        camera = MapCamera.GetComponent<Camera>();
    }
	
	void Update () {
        if (hand.controller == null) return;

        if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
			UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
        }

        if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Destroy(minimapInstance);
            camera.enabled = false;
        }
    }

    public void CreateMinimap()
    {
        Debug.Log("Creating minimap");

        Destroy(minimapInstance); // Ensure previous instance was destroyed

        camera.enabled = true;

        Quaternion rot = Quaternion.LookRotation(VRCamera.transform.forward);
        rot *= Quaternion.Euler(20, 0, 0);

        minimapInstance = Instantiate(
            MinimapPrefab,
            VRCamera.transform.position + VRCamera.transform.forward * distance,
            rot
        );
    }
}
