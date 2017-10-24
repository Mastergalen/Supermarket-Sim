
// Modified by AJS to have fade in and out, and not automatically on start

using UnityEngine;
using System.Collections; // required for Coroutines

/// <summary>
/// Fades the screen from black after a new scene is loaded.
/// </summary>
public class UCLScreenFade : MonoBehaviour
{
	/// <summary>
	/// How long it takes to fade.
	/// </summary>
	public float fadeTime = 2.0f;
	public float holdFadeTime = 10.0f;

	/// <summary>
	/// The initial screen color.
	/// </summary>
	public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);
	
	/// <summary>
	/// The shader to use when rendering the fade.
	/// </summary>
	public Shader fadeShader = null;
	
	private Material fadeMaterial = null;
	private bool isFading = false;
	
	/// <summary>
	/// Initialize.
	/// </summary>
	void Awake()
	{
		// create the fade material
		fadeMaterial = (fadeShader != null) ? new Material(fadeShader) : new Material(Shader.Find("Transparent/Diffuse"));
	}
	
	/// <summary>
	/// Starts the fade in
	/// </summary>
	void OnEnable()
	{
		StartCoroutine(FadeIn());

	}
	
	void OnDisable()
	{
	}
	
	public void OnEnableFadeIn()
	{
		StartCoroutine(FadeIn());
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.I)) 
			OnEnableFadeIn();
		if (Input.GetKeyDown (KeyCode.O)) 
			OnEnableFadeOut();
		
	}

	/// <summary>
	/// Cleans up the fade material
	/// </summary>
	void OnDestroy()
	{
		if (fadeMaterial != null)
		{
			Destroy(fadeMaterial);
		}
	}
	
	/// <summary>
	/// Fades alpha from 1.0 to 0.0
	/// </summary>
	IEnumerator FadeIn()
	{
		float elapsedTime = 0.0f;
		Color color = fadeMaterial.color = fadeColor;
		isFading = true;
		while (elapsedTime < fadeTime)
		{
			yield return new WaitForEndOfFrame();
			elapsedTime += Time.deltaTime;
			color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
			fadeMaterial.color = color;
		}
		isFading = false;
	}

	public void OnEnableFadeOut()
	{
		StartCoroutine(FadeOut());
	}


	IEnumerator FadeOut()
	{
		float elapsedTime = 0.0f;
		Color color = fadeColor;
		isFading = true;
		while (elapsedTime < fadeTime)
		{
			yield return new WaitForEndOfFrame();
			elapsedTime += Time.deltaTime;
			color.a = Mathf.Clamp01(elapsedTime / fadeTime);
			fadeMaterial.color = color;
		}
		while (elapsedTime < fadeTime+holdFadeTime)
		{
			yield return new WaitForEndOfFrame();
			elapsedTime += Time.deltaTime;
		}
		isFading = false;
	}

	void OnPostRender()
	{
		if (isFading)
		{
			fadeMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Color(fadeMaterial.color);
			GL.Begin(GL.QUADS);
			GL.Vertex3(0f, 0f, -12f);
			GL.Vertex3(0f, 1f, -12f);
			GL.Vertex3(1f, 1f, -12f);
			GL.Vertex3(1f, 0f, -12f);
			GL.End();
			GL.PopMatrix();
		}
	}
}
