using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public void startGame()
    {
        //Disable all buttons
        foreach (Button b in FindObjectsOfType<Button>())
        {
            b.interactable = false;
        }
        //Load player scene
        SceneManager.LoadSceneAsync("_Player", LoadSceneMode.Additive);
        //Unload the title scene after another scene has loaded
        SceneManager.sceneLoaded += unloadTitleScene;
    }

    public void extras()
    {

    }

    public void showControls()
    {

    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void unloadTitleScene(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= unloadTitleScene;
        SceneManager.UnloadSceneAsync("Title");
    }
}
