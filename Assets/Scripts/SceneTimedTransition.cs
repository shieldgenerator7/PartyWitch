using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimedTransition : MonoBehaviour
{

	public string SceneToUnload = "Party";
	public string sceneToLoad = "MainMenu";
	public bool playerLoad = true;
	public string playerToLoad = "_PlayerScene02";
	
	public int seconds = 60;
    private float gameTime;
	
    void Awake (){
		// on awake reset timer to 0
        this.gameTime = 0f;
    }

    void FixedUpdate()
    {
		//on update add time 
        this.gameTime += Time.deltaTime;
		
		//on update if greater than seconds var start next scene method 
        if (this.gameTime >= this.seconds)
            {                
                StartCoroutine("SceneSwitch");
            }
    }

	//works by using scene transition script's load unload checking for playerload activated by timer
	IEnumerator SceneSwitch()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
		yield return load;
			
/*  			if (playerLoad){
				AsyncOperation loadplayer = SceneManager.LoadSceneAsync(playerToLoad, LoadSceneMode.Additive);
				yield return loadplayer;
			}  */

        SceneManager.UnloadSceneAsync(SceneToUnload);
    } 
	
/*     void NextScene()
    {
		
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync(SceneToUnload);
    } */
}

