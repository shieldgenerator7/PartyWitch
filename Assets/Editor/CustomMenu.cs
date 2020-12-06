using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

public class CustomMenu
{
    //Find Default Layer Objects
    //2018-04-13: copied from http://wiki.unity3d.com/index.php?title=FindMissingScripts
    static int go_count = 0, components_count = 0, sr_count = 0;
    [MenuItem("Tools/Editor/Find Default Sorting Layer Imposters")]
    private static void FindDefaultSortingLayerImposters()
    {
        go_count = 0;
        components_count = 0;
        sr_count = 0;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                foreach (GameObject go in s.GetRootGameObjects())
                {
                    FindSortingLayerInGO(go);
                }
            }
        }
        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} default sorting layer imposters", go_count, components_count, sr_count));
    }
    private static void FindSortingLayerInGO(GameObject g)
    {
        go_count++;
        SpriteRenderer[] srs = g.GetComponents<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++)
        {
            components_count++;
            if (srs[i].sortingLayerName == "Default")
            {
                bool hasSolidCollider = g.GetComponents<Collider2D>().ToList()
                    .Any(coll => !coll.isTrigger);
                if (!hasSolidCollider) {
                    sr_count++;
                    string s = g.name;
                    Transform t = g.transform;
                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }
                    Debug.Log(s + " is in the Default Sorting Layer but does not have a solid collider!", g);
                }
            }
        }
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            FindSortingLayerInGO(childT.gameObject);
        }
    }
}
