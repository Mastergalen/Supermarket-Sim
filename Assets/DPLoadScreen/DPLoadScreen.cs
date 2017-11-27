using UnityEngine;
using System.Collections;
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
using UnityEngine.SceneManagement;
#endif

public class DPLoadScreen : MonoBehaviour {

	// Singleton
	public static DPLoadScreen Instance
	{
		get
		{
			if( instance == null )
			{
				GameObject go = new GameObject( "_DPLoadScreen" );
				go.hideFlags = HideFlags.HideInHierarchy;
				instance = go.AddComponent<DPLoadScreen>();
				GameObject.DontDestroyOnLoad( go );
			}
			return instance;
		}
	}

	private static DPLoadScreen instance;

	/// <summary>
	/// Loads the level with a custom loading screen.
	/// </summary>
	/// <param name="levelName">Level name to load.</param>
	/// <param name="manualSceneActivation">If set to <c>true</c>, when the loading is complete, you must call ActivateScene() to continue.</param>
	/// <param name="customLoadScene">Custom Loading Scene name. (optional)</param>
	public void LoadLevel( string levelName, bool manualSceneActivation = false, string customLoadScene = "LoadScreen")
	{
		StopAllCoroutines();
		StartCoroutine( doLoadLevel( levelName, customLoadScene, manualSceneActivation) );
	}

	//
	public void LoadLevel( string levelName, string customLoadScene)
	{
		LoadLevel(levelName, false, customLoadScene);
	}
	
	/// <summary>
	/// Return the percentagem of the loading. The value is between 0-100.
	/// </summary>
	public int Progress = 0;
	private bool _activeScene = false;

	/// <summary>
	/// Activates the scene when you are
	/// </summary>
	public void ActivateScene()
	{
		_activeScene = true;
	}

	public delegate void LoadEvent();
	/// <summary>
	/// Occurs when on scene finished the loading and start waiting to a call for ActivateScene() to continue.
	/// </summary>
	public event LoadEvent OnStartWaitingEventToActivateScene;

	IEnumerator doLoadLevel( string name, string customLoadScene, bool manualActivation)
	{	
		// Reset
		Progress = 0;
		OnStartWaitingEventToActivateScene = null;
		_activeScene = true;
		
		// Load the loading scene

		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		Application.LoadLevel(customLoadScene);
		#else
		SceneManager.LoadScene(customLoadScene);
		#endif

		yield return null;
		yield return null;
		
		// Load the scene async
		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		AsyncOperation async = Application.LoadLevelAsync(name);
		#else	
		AsyncOperation async = SceneManager.LoadSceneAsync(name);
		#endif

		if(manualActivation) 
		{
			async.allowSceneActivation = false;
			_activeScene = false;
		}

		while(!async.isDone) 
		{
			if(manualActivation)
			{
				if(async.progress < 0.9f) 
				{
					Progress = Mathf.RoundToInt(Mathf.Clamp((async.progress/0.9f) * 100, 0, 100));
				}
				else 
				{
					// the first load phase was completed!
					Progress = 100;
					if(!async.allowSceneActivation) 
					{
						//  notify the end of first loading phase
						if(OnStartWaitingEventToActivateScene != null) OnStartWaitingEventToActivateScene();
						// Attention!! You must call DPLoadScreen.instance.ActivateScene(); to continue
						while(!_activeScene) { yield return null; }
						async.allowSceneActivation = true;
					}
				}
			}
			else
			{
				Progress = Mathf.RoundToInt(async.progress * 100);
			}
			yield return null;
		}
		OnStartWaitingEventToActivateScene = null;
	}


	/// <summary>
	/// Loads the level addictively. but do not use the loading scene. You must create your GUI, images, etc to display the progress. Also you must use the two callbacks to display and hide these elements.
	/// </summary>
	/// <param name="name">Level Name.</param>
	public void LoadLevelAddictive(string name)
	{
		StopAllCoroutines();
		StartCoroutine(doLoadLevelAddictive(name));
	}

	/// <summary>
	/// Occurs when a scene started to load addictively. Use this do display your progress GUI, images, etc. Register this callback before call LoadLevelAddictive.
	/// </summary>
	public event LoadEvent OnStartLoadEventAddictive;

	/// <summary>
	/// Occurs when a scene finishes the loading. Use this to hide your progress GUI, images, etc. Register this callback before call LoadLevelAddictive.
	/// </summary>
	public event LoadEvent OnEndLoadEventAddictive;


	IEnumerator doLoadLevelAddictive( string name)
	{	
		// Reset the percentage
		Progress = 0;
		if(OnStartLoadEventAddictive != null) OnStartLoadEventAddictive();
		yield return null;
		
		// Load the scene async

		#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		AsyncOperation async = Application.LoadLevelAdditiveAsync(name);
		#else	
		AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
		#endif

		while(!async.isDone) {
			// updates the percentage
			Progress = Mathf.RoundToInt(async.progress * 100);
			yield return null;
		}
		// Reset the events
		if(OnEndLoadEventAddictive != null) OnEndLoadEventAddictive();
		OnStartLoadEventAddictive = null;
		OnEndLoadEventAddictive = null;
	}
}
