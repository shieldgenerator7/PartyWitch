using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public string SceneToUnload = "Party";
	public string SceneToLoad = "Rural";
	
 	private void OnTriggerEnter2D(Collider2D other)
    {		
		 if (other.tag == "Player")
        {
			Debug.Log("SceneTransitionTriggered");
			StartCoroutine("SceneSwitch");
        } 
    } 
	
     IEnumerator SceneSwitch()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
        yield return load;
        SceneManager.UnloadSceneAsync(SceneToUnload);
    } 
}
