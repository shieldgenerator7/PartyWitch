using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image logo;
    public List<Button> mainMenuButtons; 
    public Image controlScreen;

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

    public void showMainScreen(bool show = true)
    {
        logo.gameObject.SetActive(show);
        mainMenuButtons.ForEach(b => b.interactable = show);
    }

    public void showControls(bool show = true)
    {
        controlScreen.gameObject.SetActive(show);
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
