using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{

#if UNITY_EDITOR
    [Tooltip("Turns off auto-load first scene, Unity Editor only.")]
    public bool debugMode = false;
#endif

    public string startScene;

    private string currentScene = "";

    public GameObject playerObject;

    private DoorTrigger.Door targetDoor;

    private List<string> loadedSceneNames = new List<string>();

    private void Start()
    {
#if UNITY_EDITOR
        if (!debugMode)
        {
#endif
            goToArea(startScene);
#if UNITY_EDITOR
        }
#endif
        SceneManager.sceneLoaded += positionAtDoor;
        SceneManager.sceneLoaded += (s, m) => loadedSceneNames.Add(s.name);
        SceneManager.sceneUnloaded += s => loadedSceneNames.Remove(s.name);
    }

    private void goToArea(string sceneName)
    {
        if (sceneName == currentScene)
        {
            positionAtDoor(
                SceneManager.GetSceneByName(sceneName),
                LoadSceneMode.Additive
                );
            return;
        }
        playerObject.GetComponentInChildren<Rigidbody2D>().gravityScale = 0;
        //Unload prev scene
        if (loadedSceneNames.Contains(currentScene))
        {
            SceneManager.UnloadSceneAsync(currentScene);
        }
        //Load next scene
        if (!loadedSceneNames.Contains(sceneName) && currentScene != sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Debug.Log("Scene " + sceneName + " should be loading now.");
        }
        //
        currentScene = sceneName;
    }

    public void jumpToDoor(string sceneName, DoorTrigger.Door door)
    {
        this.targetDoor = door;
        goToArea(sceneName);
    }

    public void positionAtDoor(Scene s, LoadSceneMode mode)
    {
        int connectId = (targetDoor != null) ? targetDoor.connectId : -1;
        DoorTrigger door = FindObjectsOfType<DoorTrigger>().FirstOrDefault(
            d => d.id == connectId
            );
        if (door)
        {
            playerObject.transform.position = (Vector2)door.transform.position;
        }
        else
        {
            Debug.LogError(
                "DoorTrigger with Id " + connectId
                + " does not exist in scene " + s.name + "!"
                );
        }
        Rigidbody2D rb2d = playerObject.GetComponentInChildren<Rigidbody2D>();
        rb2d.gravityScale = 1;
        rb2d.transform.localPosition = Vector2.zero;
    }
}
