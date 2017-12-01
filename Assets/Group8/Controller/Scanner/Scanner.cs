using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]

public class Scanner : MonoBehaviour {

    public GameObject ScannerSprite;
    public GameObject Controller;
    public GameObject Player;
	public GameObject ScanningArea;

    private Valve.VR.InteractionSystem.Hand hand;
    private ControllerMode.Mode currentMode;
    private bool isScanning = false;
	private Bounds scanningAreaBounds;
	private GameObject Notifications;
    private MinimapController minimapController;
    
    void Start () {
        hand = Controller.GetComponent<Valve.VR.InteractionSystem.Hand>();
		Notifications = GameObject.FindGameObjectWithTag("HUD");
        minimapController = Player.GetComponent<MinimapController>();


        if (ScanningArea == null)
		{
			Debug.LogError("Scanning area not set");
		}

		if (Notifications == null)
		{
			Debug.LogError("Player HUD not set");
		}

        if (minimapController == null)
        {
            Debug.LogError("Minimap controller not set");
        }

        scanningAreaBounds = ScanningArea.GetComponent<Renderer>().bounds;
    }
	
	void Update () {
        if (hand.controller == null) return;
        currentMode = Controller.GetComponent<ControllerMode>().currentMode;
        if (currentMode != ControllerMode.Mode.Scanner) return;

        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
			if (!scanningAreaBounds.Contains(Controller.transform.position))
			{
				Debug.Log ("Not in scanning area");
				Notifications.GetComponent<Notifications>().DisplayMessage("Can't scan outside starting area");
				return;
			}

			Debug.Log("Scanning");
            isScanning = true;
            ScannerSprite.SetActive(true);
        }

        if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
			UCL.COMPGV07.Group8.CustomLogger.LogKeyDown();
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

        ProductCode pc = other.gameObject.GetComponent<ProductCode>();

        if (pc == null) return;

        int productCode = pc.Code;

        minimapController.AddProductCode(productCode);
    }
}
