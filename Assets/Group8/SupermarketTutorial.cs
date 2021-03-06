﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Rigidbody))]

public class SupermarketTutorial : MonoBehaviour {
    [Serializable]
    public class Clips
    {
        public AudioClip GoodJob;
        public AudioClip ChangeToPortalGunMode;
        public AudioClip ShootPortal;
        public AudioClip ShowShoppingList;
        public AudioClip NowLetsScan;
        public AudioClip SelectScanner;
        public AudioClip HoldToScan;
        public AudioClip OpenMap;
        public AudioClip GreatTheItems;
        public AudioClip YourTask;
        public AudioClip ByeBye;
    }

    public Clips clips;
    public GameObject portalTargetSign;
    public GameObject checkoutSign;

    private Player player = null;
    private TutorialStep tutorialPart = TutorialStep.GoodJob;

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId shoppingListButton = EVRButtonId.k_EButton_ApplicationMenu;
	private EVRButtonId gripButton = EVRButtonId.k_EButton_Grip;

    private Text textComponent;
    private AudioSource audioSource;

    private enum TutorialStep {GoodJob, PortalGunMode, ShootPortal, ShoppingList, ScanMode, ScanItems, MinimapMode, Minimap, YourTask, TaskStart}

    // Use this for initialization
    void Start () {
        if(checkoutSign == null)
        {
            Debug.LogError("Checkout sign not set");
        }

        if (portalTargetSign == null)
        {
            Debug.LogError("Portal target sign not set");
        }

        player = Player.instance;
		audioSource = GetComponent<AudioSource>();

        Teleport.instance.CancelTeleportHint();
        textComponent = GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>();

        textComponent.text = "Good job!";
        RobotSpeak(clips.GoodJob);

        tutorialPart = TutorialStep.PortalGunMode;
        Invoke("PortalGunMode", 2);	
    }

    // Update is called once per frame
    void Update () {
        foreach (Hand hand in player.hands)
        {
            if (hand.controller == null) break;

            ControllerMode.Mode currentMode = hand.GetComponent<ControllerMode>().currentMode;

            //checking for portal gun mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == TutorialStep.PortalGunMode))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x > 0.7f)
                {                    
					HideButtonHint(touchpadButton);
                    textComponent.text = "Now shoot the portal above the checkout. Try to make sure it is directly above it.";
                    tutorialPart = TutorialStep.ShootPortal;
					ShowButtonHint(triggerButton, "Press the trigger to shoot portal");
                    RobotSpeak(clips.ShootPortal);
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == TutorialStep.ShootPortal) && (currentMode == ControllerMode.Mode.PortalGun))
            {
				HideButtonHint(triggerButton);
                textComponent.text = "Once you are happy with the portal, hold the menu button to see your shopping list.";
                tutorialPart = TutorialStep.ShoppingList;
                RobotSpeak(clips.ShowShoppingList);
                ShowButtonHint(shoppingListButton, "Show Shopping List");
            }

            //checking for shopping list button press
            if (hand.controller.GetPressDown(shoppingListButton) && (tutorialPart == TutorialStep.ShoppingList))
            {
                portalTargetSign.SetActive(false);
                HideButtonHint(shoppingListButton);
                textComponent.text = "Now let's scan some items.";
                ShowButtonHint(touchpadButton, "Press LEFT on touchpad to change to Scan Mode");
                Invoke("ScannerTutorial", 2);
            }

            //checking for scanner mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == TutorialStep.ScanMode))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x < -0.7f)
                {
                    HideButtonHint(touchpadButton);
                    textComponent.text = "Hold trigger to scan. Scan the items in front of me :)";
                    tutorialPart = TutorialStep.ScanItems;
                    RobotSpeak(clips.HoldToScan);
                    ShowButtonHint(triggerButton, "Hold to scan items.");
                }
            }

            //checking for after scan button press
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == TutorialStep.ScanItems) && (currentMode == ControllerMode.Mode.Scanner))
            {
                textComponent.text = "Open the map by holding down on the touchpad.";
                tutorialPart = TutorialStep.MinimapMode;
                RobotSpeak(clips.OpenMap);
                HideButtonHint(triggerButton);
                ShowButtonHint(touchpadButton, "Hold DOWN on touchpad to change to show Map");
            }

            //checking for map mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == TutorialStep.MinimapMode))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.y < -0.7f)
                {
                    HideButtonHint(touchpadButton);
                    textComponent.text = "Great! The items you have scanned are blue dots. The green dot is you.";
                    tutorialPart = TutorialStep.Minimap;
                    RobotSpeak(clips.GreatTheItems);
                    Invoke("StartTaskText", 10);
                }
            }

			//checking for teleport event - turn off button hint after they teleport once
			if (hand.controller.GetPressDown(gripButton) && (tutorialPart == TutorialStep.TaskStart))
			{
				HideButtonHint(gripButton);
			}
        }
    }

    void PortalGunMode()
    {
        textComponent.text = "Change to portal gun mode!";
        tutorialPart = TutorialStep.PortalGunMode;
		ShowButtonHint(touchpadButton, "Press RIGHT on touchpad to change to Portal Gun Mode");
        RobotSpeak(clips.ChangeToPortalGunMode);
    }

    void ScannerTutorial()
    {
        textComponent.text = "Select scanner by pressing left on the touchpad!";
        tutorialPart = TutorialStep.ScanMode;
        RobotSpeak(clips.SelectScanner);
    }

    void StartTaskText()
    {
        textComponent.text = "Your task: Collect these items and put them on the checkout. Use the map to find them. Tip: Put the items in your belly portal!";
        tutorialPart = TutorialStep.YourTask;

        checkoutSign.SetActive(true);

        RobotSpeak(clips.YourTask);
        Invoke("RobotFlyAway", 13);
    }

    void RobotFlyAway()
    {
        textComponent.text = "Bye Bye :)";
        RobotSpeak(clips.ByeBye);

		tutorialPart = TutorialStep.TaskStart;
		ShowButtonHint (gripButton, "Teleport");

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Flying", true);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0.75f, 0);

        Invoke("SelfDestruct", 10);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void ShowButtonHint(EVRButtonId button, string buttonText)
    {
        foreach (Hand h in player.hands)
        {
            ControllerButtonHints.ShowTextHint(h, button, buttonText);
        }
    }

    private void HideButtonHint(EVRButtonId button)
    {
        foreach (Hand h in player.hands)
        {
            ControllerButtonHints.HideTextHint(h, button);
        }
    }


    private void RobotSpeak(AudioClip clip)
    {
        audioSource.clip = clip;

        if (audioSource.clip == null)
        {
            Debug.LogError("Audio clip is null");
        }

        audioSource.Play();
    }
}
