using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]

public class Notifications : MonoBehaviour {

    public Sprite successIcon;
    public Sprite failIcon;

    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void DisplayMessage(string label)
    {
        canvas.enabled = true;

        transform.Find("Notification").gameObject.GetComponent<Text>().text = label;

        Image icon = transform.Find("Icon").GetComponent<Image>();
        icon.enabled = true;

        switch (label)
        {
            case "Correct!":
                icon.sprite = successIcon;
                break;
            case "No target portal set":
            case "Incorrect":
                icon.sprite = failIcon;
                break;
            default:
                icon.enabled = false;
                break;
        }

        StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(3);

        canvas.enabled = false;
    }
}
