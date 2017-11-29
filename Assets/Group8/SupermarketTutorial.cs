using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class SupermarketTutorial : MonoBehaviour {

    private Player player = null;
    private string tutorialPart;

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId shoppingListButton = EVRButtonId.k_EButton_ApplicationMenu;

    // Use this for initialization
    void Start () {
        player = Player.instance;

        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Good job!";
        Invoke("StartTutorialText", 2);
    }

    // Update is called once per frame
    void Update () {
        foreach (Hand hand in player.hands)
        {
            if (hand.controller == null) break;

            //checking for portal gun mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == "portalGun"))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x > 0.7f)
                {
                    // You have to get the full path every time
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Now shoot the portal above the checkout. Try to make sure it is directly above it.";
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == "portalGun"))
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Once you are happy with the portal, hold the menu button to see your shopping list.";
                tutorialPart = "shoppingList";
                ShowButtonHint(shoppingListButton, "Show Shopping List");
            }

            //checking for shopping list button press
            if (hand.controller.GetPressDown(shoppingListButton) && (tutorialPart == "shoppingList"))
            {
                HideButtonHint(shoppingListButton);
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Now let's scan some items.";
                ShowButtonHint(touchpadButton, "Press LEFT on touchpad to change to Scan Mode");
                Invoke("ScannerTutorial", 2);
            }

            //checking for scanner mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == "scanner"))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x < -0.7f)
                {
                    // You have to get the full path every time
                    HideButtonHint(touchpadButton);
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Hold trigger to scan. Scan the items in front of me :)";
                    ShowButtonHint(triggerButton, "Hold to scan items.");
                }
            }

            //checking for after scan button press
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == "scanner"))
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Open the map by holding down on the touchpad.";
                tutorialPart = "map";
                HideButtonHint(triggerButton);
                ShowButtonHint(touchpadButton, "Hold DOWN on touchpad to change to show Map");
            }

            //checking for map mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == "map"))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.y < -0.7f)
                {
                    // You have to get the full path every time
                    HideButtonHint(touchpadButton);
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Great! The items you have scanned are blue dots. The green dot is you.";
                    Invoke("StartTaskText", 3);
                }
            }
        }
    }

    void StartTutorialText()
    {
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Change to portal gun mode!";
        tutorialPart = "portalGun";
    }

    void ScannerTutorial()
    {
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Select scanner by pressing left on the touchpad!";
        tutorialPart = "scanner";
    }

    void StartTaskText()
    {
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Your task: collect 2 of each of these items. Use the map to find them. Put them in your belly and they will go to the checkout! Good luck!";
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
}
