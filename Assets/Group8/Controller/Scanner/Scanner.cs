using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]

public class Scanner : MonoBehaviour {

    public GameObject ScannerSprite;
    public GameObject Controller;
    public GameObject Player;

    private Valve.VR.InteractionSystem.Hand hand;
    private ControllerMode.Mode currentMode;
    private bool isScanning = false;
    
    void Start () {
        hand = Controller.GetComponent<Valve.VR.InteractionSystem.Hand>();
    }
	
	void Update () {
        if (hand.controller == null) return;
        currentMode = Controller.GetComponent<ControllerMode>().currentMode;
        if (currentMode != ControllerMode.Mode.Scanner) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Scanning");
            isScanning = true;
            ScannerSprite.SetActive(true);
        }

        if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            UCL.COMPGV07.Logging.KeyDown();
        }


        if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            isScanning = false;
            ScannerSprite.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentMode != ControllerMode.Mode.Scanner) return;
        if (!isScanning) return;

        int layer = other.gameObject.layer;

        if (layer != LayerMask.NameToLayer("Grabbable")) return;

        int productCode = other.gameObject.GetComponent<ProductCode>().Code;

        Player.GetComponent<MinimapController>().AddProductCode(productCode);
    }
}
