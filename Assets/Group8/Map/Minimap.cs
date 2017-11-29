using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Minimap : MonoBehaviour {

    public GameObject MinimapPrefab;
    public GameObject VRCamera;
    public float distance = -1.0f;
    
    private Hand hand;
    private GameObject minimapInstance;

    void Start () {
        hand = GetComponent<Hand>();
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
        }
    }

    public void CreateMinimap()
    {
        Debug.Log("Creating minimap");

        Destroy(minimapInstance); // Ensure previous instance was destroyed

        Quaternion rot = Quaternion.LookRotation(VRCamera.transform.forward);
        rot *= Quaternion.Euler(20, 0, 0);

        minimapInstance = Instantiate(
            MinimapPrefab,
            VRCamera.transform.position + VRCamera.transform.forward * distance,
            rot
        );
    }
}
