using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class SupermarketTutorial : MonoBehaviour {

    public AudioClip[] audioRobot = new AudioClip[8];

    private Player player = null;
    private TutorialStep tutorialPart;

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId shoppingListButton = EVRButtonId.k_EButton_ApplicationMenu;
    private Text textComponent;
    private AudioSource audioSource;

    private enum TutorialStep {GoodJob, PortalGunMode, ShootPortal, ShoppingList, ScanMode, ScanItems, MinimapMode, Minimap}

    private Dictionary<TutorialStep, AudioClip> audioMap;

    // Use this for initialization
    void Start () {
        player = Player.instance;
        textComponent = GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>();

        tutorialPart = TutorialStep.GoodJob;
        textComponent.text = "Good job!";
        RobotSpeak(TutorialStep.GoodJob);

        tutorialPart = TutorialStep.PortalGunMode;
        Invoke("StartTutorialText", 2);
        audioSource = GetComponent<AudioSource>();

        audioMap = new Dictionary<TutorialStep, AudioClip>()
        {
            {TutorialStep.GoodJob, audioRobot[0]},
            {TutorialStep.PortalGunMode, audioRobot[1]},
            {TutorialStep.ShootPortal, audioRobot[2]},
            {TutorialStep.ShoppingList, audioRobot[3]},
            {TutorialStep.ScanMode, audioRobot[4]},
            {TutorialStep.ScanItems, audioRobot[5]},
            {TutorialStep.MinimapMode, audioRobot[6]},
            {TutorialStep.Minimap, audioRobot[7]}

        };

        
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
                    
                    textComponent.text = "Now shoot the portal above the checkout. Try to make sure it is directly above it.";                    
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == "portalGun"))
            {
                textComponent.text = "Once you are happy with the portal, hold the menu button to see your shopping list.";
                tutorialPart = "shoppingList";
                ShowButtonHint(shoppingListButton, "Show Shopping List");
            }

            //checking for shopping list button press
            if (hand.controller.GetPressDown(shoppingListButton) && (tutorialPart == "shoppingList"))
            {
                HideButtonHint(shoppingListButton);
                textComponent.text = "Now let's scan some items.";
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
                    textComponent.text = "Hold trigger to scan. Scan the items in front of me :)";
                    ShowButtonHint(triggerButton, "Hold to scan items.");
                }
            }

            //checking for after scan button press
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == "scanner"))
            {
                textComponent.text = "Open the map by holding down on the touchpad.";
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
                    textComponent.text = "Great! The items you have scanned are blue dots. The green dot is you.";
                    Invoke("StartTaskText", 3);
                }
            }
        }
    }

    void StartTutorialText()
    {
        textComponent.text = "Change to portal gun mode!";
        tutorialPart = "portalGun";
    }

    void ScannerTutorial()
    {
        textComponent.text = "Select scanner by pressing left on the touchpad!";
        tutorialPart = "scanner";
    }

    void StartTaskText()
    {
        textComponent.text = "Your task: collect 2 of each of these items. Use the map to find them. Put them in your belly and they will go to the checkout! Good luck!";
        tutorialPart = "robotFly";

        Invoke("RobotFlyAway", 15);
    }

    void RobotFlyAway()
    {
        textComponent.text = "Bye Bye :)";

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


    private void RobotSpeak(TutorialStep audioStep)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioMap[audioStep];

            if (audioSource.clip == null)
            {
                Debug.LogError("Audio clip is null");
            }

            audioSource.Play();
        }
    }
}
