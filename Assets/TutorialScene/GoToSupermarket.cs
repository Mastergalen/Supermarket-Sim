using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToSupermarket : MonoBehaviour
{

    public GameObject LoadingScene;
    public Image LoadingBar;

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.GetComponent<Collider>().name == "HeadCollider")
        {
            SteamVR_LoadLevel.Begin("Supermarket_01");
        }
    }

    public void LoadLevel(string LevelName)
    {

        StartCoroutine(LevelCoroutine(LevelName));
    }

    IEnumerator LevelCoroutine(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
