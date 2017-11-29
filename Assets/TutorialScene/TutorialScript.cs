using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;


public class TutorialScript : MonoBehaviour
{
    public GameObject[] teleportAreas = new GameObject[5];
    public GameObject bellyPortal;

    private GameObject portal;
    private GameObject throwable;
    private GameObject food;
    private GameObject cashout;
    private GameObject robot;
    private Player player = null;
    private Animator anim;
    private Vector3 robotTarget = new Vector3(-5.48f, 0, 0);
    private int tutorialPart = 0;
    private CheckoutPortal portalScript;

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    void Start()
    {
        InitTutorial();

        // Enable teleport point 1
        teleportAreas[0].SetActive(true);
        anim = robot.GetComponent<Animator>();
    }

    void Update()
    {

        foreach (Hand hand in player.hands)
        {
            if (hand.controller == null) break;

            //checking for portal gun mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == 2))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    // You have to get the full path every time
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Fire using the trigger! Try to hit the checkout.";
                    ShowButtonHint(triggerButton, "Pull to shoot portal");
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == 2))
            {
                robotTarget = new Vector3(3.3f, 0, 4.4f);
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Let's learn to grab objects! Teleport here!";
            }

            //checking for grab mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == 3))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.y > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Grab objects by holding the trigger. Release by letting go. Try throwing something!";
                    ShowButtonHint(triggerButton, "Hold trigger to grab an object");
                }
            }

            //checking for portal gun has been shot
            if (hand.controller.GetPressDown(triggerButton) && (tutorialPart == 3))
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Look at your belly! Try dropping an object in, and look at the portal you shot on the checkout.";
            }

            //check for collision between a gameobject and belly portal
            portalScript = (CheckoutPortal)bellyPortal.GetComponent(typeof(CheckoutPortal));
            if (portalScript.Checker == true)
            {
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Teleport to the portal, then walk through it to go to the supermarket!";
                // Proceed to supermarket
                ActivatePortal();
            }
            portalScript.Checker = false;
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
            robot.transform.rotation = Quaternion.Slerp(robot.transform.rotation, Quaternion.LookRotation(GameObject.Find("Player").transform.position - robot.transform.position), Time.deltaTime * 3);
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        // Check for correct equipment before enabling tutorials
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider")
        {
            teleportAreas[tutorialPart].SetActive(false);

            if(tutorialPart == 0)
            {
                robotTarget = new Vector3(-7.5f, 0, 4.4f);
                GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Now teleport to the next point!";
            }

            // Portal gun tutorial
            if (tutorialPart == 1)
            {
                cashout.SetActive(true);
                PortalGunTutorial();
            }
            // Throwables tutorial
            if (tutorialPart == 2)
            {
                food.SetActive(true);
                throwable.SetActive(true);
                ThrowableTutorial();
            }
            // Minimap tutorial
            /*
            if (tutorialPart == 3)
            {
                MinimapTutorial();
            }*/
            tutorialPart++;
            SetTargetTeleport();
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
        for (int i = 0; i < teleportAreas.Length; i++)
        {
            if (tutorialPart == i)
            {
                teleportAreas[i].SetActive(true);
                gameObject.GetComponent<BoxCollider>().center = new Vector3(teleportAreas[i].transform.position.x, 1.5f, teleportAreas[i].transform.position.z);
            }
            else
            {
                teleportAreas[i].SetActive(false);
            }
        }
    }

    // TODO Portal gun tutorial
    private void PortalGunTutorial()
    {
        ShowButtonHint(touchpadButton, "Press RIGHT on touchpad to change to Portal Gun Mode");
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Select portal gun by pressing right on the touchpad.";
    }

    // TODO Throwables tutorial
    private void ThrowableTutorial()
    {
        HideButtonHint(touchpadButton);
        HideButtonHint(triggerButton);
        ShowButtonHint(touchpadButton, "Press UP on touchpad to change to Grab Mode");

        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Select grab mode by pressing up on the touchpad.";
        
    }

    // TODO Minimap tutorials
    /*
    private void MinimapTutorial()
    {
        GameObject.Find("RobotModel").transform.Find("BubbleSpeech/Text").GetComponent<Text>().text = "Bring up the map by holding the bottom of the pad.";
    }*/

    // Render portal
    IEnumerator PortalEffect(float time)
    {
        Vector3 originalScale = portal.transform.localScale;
        Vector3 destinationScale = new Vector3(0.14f, 0.14f, 0.5f);
        float currentTime = 0.0f;

        do
        {
            portal.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        teleportAreas[tutorialPart].SetActive(true);
    }

    // Teleport to supermarket via portal
    private void ActivatePortal()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        portal.transform.localScale = new Vector3(0, 0, 0);
        portal.SetActive(true);

        StartCoroutine(PortalEffect(1.5f));
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
