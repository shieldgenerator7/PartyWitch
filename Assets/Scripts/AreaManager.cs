using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public string startScene;

    public string currentScene = "";
    public bool jumpNow = false;

    private void Start()
    {
        goToArea(startScene);
    }

    private void Update()
    {
        if (jumpNow)
        {
            jumpNow = false;
            goToArea(startScene);
        }
    }

    public void goToArea(string sceneName)
    {
        Scene prevScene = SceneManager.GetSceneByName(currentScene);
        if (prevScene != null && prevScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        currentScene = sceneName;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Debug.Log("Scene " + sceneName + " should be loading now.");
    }
}
