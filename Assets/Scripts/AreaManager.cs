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
    private Rigidbody2D playerRB2D;

    public AudioClip transitionSound;

    private DoorTrigger.Door targetDoor;

    private List<string> loadedSceneNames = new List<string>();

    private void Start()
    {
        playerRB2D = playerObject.GetComponentInChildren<Rigidbody2D>();

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
        //If already in that scene,
        if (sceneName == currentScene)
        {
            //Just teleport
            positionAtDoor(
                SceneManager.GetSceneByName(sceneName),
                LoadSceneMode.Additive
                );
            //Don't do any scene loading
            return;
        }
        freezePlayer();
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
        //Sound effect
        AudioSource.PlayClipAtPoint(transitionSound, playerRB2D.transform.position);
    }

    public void jumpToDoor(string sceneName, DoorTrigger.Door door)
    {
        this.targetDoor = door;
        goToArea(sceneName);
    }

    public void positionAtDoor(Scene s, LoadSceneMode mode)
    {
        //If there's no target door,
        if (targetDoor == null)
        {
            //Unfreeze player
            freezePlayer(false);
            //Do nothing
            return;
        }
        DoorTrigger door = FindObjectsOfType<DoorTrigger>().FirstOrDefault(
            d => d.id == targetDoor.connectId
            );
        if (door)
        {
            playerObject.transform.position = (Vector2)door.transform.position;
            playerRB2D.transform.localPosition = Vector2.zero;
            AudioSource.PlayClipAtPoint(door.doorSound, door.transform.position);
        }
        else
        {
            Debug.LogError(
                "DoorTrigger with Id " + targetDoor.connectId
                + " does not exist in scene " + s.name + "!"
                );
        }
        freezePlayer(false);
    }

    private void freezePlayer(bool freeze = true)
    {
        if (freeze)
        {
            playerRB2D.gravityScale = 0;
            playerRB2D.velocity = Vector2.zero;
        }
        else
        {
            playerRB2D.gravityScale = 1;
        }
    }
}
