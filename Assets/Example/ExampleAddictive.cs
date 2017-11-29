using UnityEngine;
using System.Collections;

public class ExampleAddictive : MonoBehaviour {

	UnityEngine.UI.Slider slider;
	
	void Start () 
	{
		slider = GetComponent<UnityEngine.UI.Slider>();
		slider.gameObject.SetActive(false);
	}

	public void LoadAsync()
	{
		// Setup the callbacks to display and hide the status bar
		DPLoadScreen.Instance.OnStartLoadEventAddictive += () => slider.gameObject.SetActive(true);
		DPLoadScreen.Instance.OnEndLoadEventAddictive  += () => slider.gameObject.SetActive(false);

		// loads the scene
		DPLoadScreen.Instance.LoadLevelAddictive("AddictiveScene");
	}
		
	// Update is called once per frame
	void Update () 
	{
		slider.value = DPLoadScreen.Instance.Progress;
	}
}
