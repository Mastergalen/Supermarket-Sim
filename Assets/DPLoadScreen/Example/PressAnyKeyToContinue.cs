using UnityEngine;
using System.Collections;

public class PressAnyKeyToContinue : MonoBehaviour 
{
	UnityEngine.UI.Text text;

	bool sceneIsReady = false;
	void Start () 
	{
		text = GetComponent<UnityEngine.UI.Text>();
		text.enabled = false;

		// Enable the text when the load is completed!
		DPLoadScreen.Instance.OnStartWaitingEventToActivateScene += delegate {
			text.enabled = true;
			sceneIsReady = true;
		};
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sceneIsReady && Input.anyKeyDown)
		{
			// when we got any get down and the text is enabled (ie. the event OnWaitingEventToActivateScene is fired), then we can finally activate the scene!
			DPLoadScreen.Instance.ActivateScene();
		}
	}
}
