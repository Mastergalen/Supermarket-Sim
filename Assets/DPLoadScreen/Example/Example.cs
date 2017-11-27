using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	
	public void Loadlevel()
	{
		// Replace Application.LoadLevel("BigScene"); with
		DPLoadScreen.Instance.LoadLevel("BigScene");
	}

	public void Loadlevel2()
	{
		// Alternative method showing to wait a key press to continue and using a second load scene
		DPLoadScreen.Instance.LoadLevel("BigScene", true, "LoadScreenPressToContinue");
	}

}
