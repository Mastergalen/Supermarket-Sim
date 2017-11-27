using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TutorialScript : MonoBehaviour
{

    public GameObject[] teleportAreas = new GameObject[5];
    private int tutorialPart = 0;

    private GameObject portal;
    private GameObject throwable;
    private GameObject food;
    private GameObject cashout;
    private GameObject robot;
    private Player player = null;
    private Vector3 robotTarget = new Vector3(-5.48f, 0, 0);

    private EVRButtonId touchpadButton = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.InteractionSystem.Hand hand;

    // Use this for initialization
    void Start()
    {
        InitTutorial();
        
        //enable teleport point 1
        teleportAreas[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float step = 0.001f * Time.deltaTime;
        robot.transform.position = Vector3.MoveTowards(robot.transform.position, robotTarget, step);

        foreach (Hand hand in player.hands)
        {
            if (hand.controller == null) return;

            //checking for portal gun mode change
            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == 2))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.x > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    ShowButtonHint(triggerButton, "Pull to shoot portal");
                }
            }

            if (hand.controller.GetPressDown(touchpadButton) && (tutorialPart == 3))
            {
                Vector2 touchpad = hand.controller.GetAxis();
                if (touchpad.y > 0.7f)
                {
                    HideButtonHint(touchpadButton);
                    ShowButtonHint(triggerButton, "Hold trigger to grab an object");
                }
            }
        }

    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider") //enable throwables tutorial
        {
            teleportAreas[tutorialPart].SetActive(false);
            if (tutorialPart == 1) //portal gun tutorial
            {
                cashout.SetActive(true);
                PortalGunTutorial();
            }
            if (tutorialPart == 2) //throwable tutorial
            {
                food.SetActive(true);
                throwable.SetActive(true);
                ThrowableTutorial();
            }
            tutorialPart++;
            SetTargetTeleport();
        }
    }

    //disables all teleport points and hides objects that are required for further tutorial parts
    private void InitTutorial()
    {
        player = Player.instance;

        foreach (GameObject area in teleportAreas)
        {
            area.SetActive(false);
        }
        portal = GameObject.Find("Portal_01");
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

    private void SetTargetTeleport()
    {
        for (int i = 0; i < teleportAreas.Length; i++)
        {
            if (tutorialPart == i)
            {
                teleportAreas[i].SetActive(true);
                gameObject.GetComponent<BoxCollider>().center = new Vector3(teleportAreas[i].transform.position.x, 1.5f, teleportAreas[i].transform.position.z);
                robot.transform.position = new Vector3((teleportAreas[i].transform.position.x - 1.5f), 0, teleportAreas[i].transform.position.z);
                robot.transform.eulerAngles = new Vector3(0, 180, 0);
                //Vector3 robotTarget = new Vector3((teleportAreas[i].transform.position.x - 1.5f), 0, teleportAreas[i].transform.position.z);
                //MoveRobot(robotTarget);
                robotTarget = new Vector3((teleportAreas[i].transform.position.x - 1.5f), 0, teleportAreas[i].transform.position.z);
            }
            else
            {
                teleportAreas[i].SetActive(false);
            }
        }
    }

    private void PortalGunTutorial()
    {
        ShowButtonHint(touchpadButton, "Press RIGHT on touchpad to change to Portal Gun Mode");
           //TODO portal gun tutorial
    }

    private void ThrowableTutorial()
    {
        HideButtonHint(touchpadButton);
        HideButtonHint(triggerButton);
        ShowButtonHint(touchpadButton, "Press UP on touchpad to change to Grab Mode");

        //TODO tutorial for throwables

        ActivatePortal(); //throwable tutorial part done - can go to supermarket now! :)
    }

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
