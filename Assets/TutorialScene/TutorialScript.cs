using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;


[Serializable]
public class Clips
{
    public AudioClip NowTeleportTo;
    public AudioClip SelectPortalGun;
    public AudioClip SelectGrabMode;
    public AudioClip LetsLearn;
    public AudioClip GrabObjects;
    public AudioClip LookAtBelly;
    public AudioClip TeleportToPortal;
    public AudioClip FireAway;
}

public class TutorialScript : MonoBehaviour
{
    public GameObject[] teleportAreas = new GameObject[5];
    public GameObject bellyPortal;
    public Clips clips;

    private AudioSource audioSource;
    private GameObject portal;
    private GameObject throwable;
    private GameObject food;
    private GameObject cashout;
    private GameObject robot;
    private Player player = null;
    private Animator anim;
    private Vector3 robotTarget = new Vector3(-5.48f, 0, 0);
    private TutorialPart tutorialPart = TutorialPart.Welcome;
    private CheckoutPortal portalScript;

    private enum TutorialPart { Welcome, Teleport, PortalGunMode, PortalGunShoot, TeleportToGrab, GrabMode, Grab, Belly, TeleportSupermarket, FireAway };

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    private Dictionary<TutorialPart, GameObject> teleportAreaMap;

    void Start()
    {
        InitTutorial();

        // Enable teleport point 1
        teleportAreas[0].SetActive(true);
        anim = robot.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        teleportAreaMap = new Dictionary<TutorialPart, GameObject>()
        {
            { TutorialPart.Welcome, teleportAreas[0]},
            { TutorialPart.Teleport, teleportAreas[1]},
            { TutorialPart.TeleportToGrab, teleportAreas[2]},
            { TutorialPart.TeleportSupermarket, teleportAreas[3]},
        };
    }

    void Update()
    {

        foreach (Hand hand in player.hands)
        {
            if (hand.controller == null) break;

            //checking for portal gun mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == TutorialPart.PortalGunMode))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    // You have to get the full path every time
                    //audioSource.Play();
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Fire using the trigger! Try to hit the checkout.";
                    RobotSpeak(clips.FireAway);
                    ShowButtonHint(triggerButton, "Pull to shoot portal");

                    tutorialPart = TutorialPart.PortalGunShoot;
                    SetTargetTeleport();
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == TutorialPart.PortalGunShoot))
            {
                robotTarget = new Vector3(3.3f, 0, 4.4f);
                teleportAreas[2].SetActive(true);
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Let's learn to grab objects! Teleport here!";
                RobotSpeak(clips.LetsLearn);

                tutorialPart = TutorialPart.TeleportToGrab;
                SetTargetTeleport();
            }

            //checking for grab mode change
            if (hand.controller.GetPressDown(touchpadButton) && tutorialPart== TutorialPart.GrabMode)
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.y > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Grab objects by holding the trigger. Release by letting go. Try throwing something!";
                    RobotSpeak(clips.GrabObjects);
                    ShowButtonHint(triggerButton, "Hold trigger to grab an object");

                    tutorialPart = TutorialPart.Grab;
                    SetTargetTeleport();
                }
            }

            // Belly portal
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == TutorialPart.Grab))
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Look at your belly! Try dropping an object in, and look at the portal you shot on the checkout.";
                RobotSpeak(clips.LookAtBelly);

                tutorialPart = TutorialPart.Belly;
                SetTargetTeleport();
            }

            //check for collision between a gameobject and belly portal
            portalScript = (CheckoutPortal)bellyPortal.GetComponent(typeof(CheckoutPortal));
            if (tutorialPart == TutorialPart.Belly && portalScript.CheckObjectTeleported == true)
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Teleport to the portal, then walk through it to go to the supermarket!";
                RobotSpeak(clips.TeleportToPortal);
                // Proceed to supermarket
                ActivatePortal();
            }
            portalScript.CheckObjectTeleported = false;
        }


        // Robot walk
        if (robot.transform.position != robotTarget)
        {
            // 0 for still, 1 for walk, 2 for run, 3 for jump
            anim.SetInteger("Speed", 1);
            robot.transform.rotation = Quaternion.Slerp(robot.transform.rotation, Quaternion.LookRotation(robotTarget - robot.transform.position), Time.deltaTime * 3);
            robot.transform.position = Vector3.MoveTowards(robot.transform.position, robotTarget, 0.05f);
        }
        // Robot stop and face player
        else
        {
            anim.SetInteger("Speed", 0);
            robot.transform.rotation = Quaternion.Slerp(robot.transform.rotation, Quaternion.LookRotation(Player.instance.transform.position - robot.transform.position), Time.deltaTime * 3);
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        // Check for correct equipment before enabling tutorials
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider")
        {
            if(tutorialPart == TutorialPart.Welcome)
            {
                teleportAreas[0].SetActive(false);
                robotTarget = new Vector3(-7.5f, 0, 4.4f);
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Now teleport to the next point!";
                RobotSpeak(clips.NowTeleportTo);
                teleportAreas[1].SetActive(true);
                tutorialPart = TutorialPart.Teleport;
            }
            // Portal gun tutorial
            else if (tutorialPart == TutorialPart.Teleport)
            {
                teleportAreas[1].SetActive(false);
                cashout.SetActive(true);
                PortalGunTutorial();
            }
            // Throwables tutorial
            else if (tutorialPart == TutorialPart.TeleportToGrab)
            {
                teleportAreas[2].SetActive(false);
                food.SetActive(true);
                throwable.SetActive(true);
                ThrowableTutorial();
            }

            SetTargetTeleport();
            teleportAreaMap[TutorialPart.TeleportSupermarket].SetActive(false);
        }
    }

    // Start tutorial: disables teleport points and hides objects for other parts
    private void InitTutorial()
    {
        player = Player.instance;

        foreach (GameObject area in teleportAreas)
        {
            area.SetActive(false);
        }

        portal = GameObject.Find("Portal");
        portal.SetActive(false);

        throwable = GameObject.Find("Throwables");
        throwable.SetActive(false);

        food = GameObject.Find("Food");
        food.SetActive(false);

        cashout = GameObject.Find("Cashout");
        cashout.SetActive(false);

        robot = GameObject.Find("RobotModel");
        robot.transform.position = new Vector3(-5.48f, 0, 0);
        robot.transform.eulerAngles = new Vector3(0, 134, 0);
    }

    // Set fixed telport areas for tutorial
    private void SetTargetTeleport()
    {
        GameObject teleportArea;
        teleportAreaMap.TryGetValue(tutorialPart, out teleportArea);

        if (teleportArea == null)
        {
            Debug.LogWarning("Teleport area was null for " + tutorialPart);
        } else
        {
            gameObject.GetComponent<BoxCollider>().center = new Vector3(teleportArea.transform.position.x, 1.5f, teleportArea.transform.position.z);
        }
    }

    private void PortalGunTutorial()
    {
        ShowButtonHint(touchpadButton, "Press RIGHT on touchpad to change to Portal Gun Mode");
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Select portal gun by pressing right on the touchpad.";
        RobotSpeak(clips.SelectPortalGun);
        tutorialPart = TutorialPart.PortalGunMode;
        SetTargetTeleport();
    }

    private void ThrowableTutorial()
    {
        HideButtonHint(touchpadButton);
        HideButtonHint(triggerButton);
        ShowButtonHint(touchpadButton, "Press UP on touchpad to change to Grab Mode");

        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Select grab mode by pressing up on the touchpad.";
        RobotSpeak(clips.SelectGrabMode);
        tutorialPart = TutorialPart.GrabMode;
        SetTargetTeleport();
    }

    // Render portal
    IEnumerator PortalEffect(float time)
    {
        Vector3 originalScale = portal.transform.localScale;
        Vector3 destinationScale = new Vector3(2f, 2f, 1f);
        float currentTime = 0.0f;

        do
        {
            portal.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        teleportAreaMap[TutorialPart.TeleportSupermarket].SetActive(true);
    }

    // Teleport to supermarket via portal
    private void ActivatePortal()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        portal.transform.localScale = new Vector3(0, 0, 0);
        portal.SetActive(true);

        StartCoroutine(PortalEffect(1.5f));

        tutorialPart = TutorialPart.TeleportSupermarket;
        SetTargetTeleport();
        teleportAreaMap[TutorialPart.TeleportSupermarket].SetActive(true);
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
