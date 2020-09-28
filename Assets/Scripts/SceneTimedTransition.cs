using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimedTransition : MonoBehaviour
{

	public string SceneToUnload = "Title";
	public string sceneToLoad = "Party";
	public bool playerLoad = true;
	public string playerToLoad = "_Player";
	
	public int seconds = 60;
    private float gameTime;
	
    void Awake (){
		// on awake reset timer to 0
        this.gameTime = Time.time;
    }

    void Update()
	{
		//on update if greater than seconds var
		if (Time.time >= gameTime + seconds)
        {
			//start next scene method
			SceneSwitch();
			Destroy(this);
		}
    }

	//works by using scene transition script's load unload checking for playerload activated by timer
	void SceneSwitch()
	{
		SceneManager.UnloadSceneAsync(SceneToUnload);

		SceneManager.LoadSceneAsync(sceneToLoad);

		if (playerLoad)
		{
			SceneManager.LoadSceneAsync(playerToLoad, LoadSceneMode.Additive);
		}		
    } 
}

