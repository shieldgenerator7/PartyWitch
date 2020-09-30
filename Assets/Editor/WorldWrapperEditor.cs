using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(WorldWrapper))]
public class WorldWrapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WorldWrapper worldWrapper = (WorldWrapper)target;
        GUI.enabled = !EditorApplication.isPlaying;
        if (GUILayout.Button("Setup"))
        {
            //Destroy current children
            //2020-06-02: copied from https://stackoverflow.com/a/60391826/2336212
            while (worldWrapper.transform.childCount > 0)
            {
                DestroyImmediate(worldWrapper.transform.GetChild(0).gameObject);
            }
            //Setup template
            GameObject template = GameObject.Instantiate(worldWrapper.gameObject);
            DestroyImmediate(template.GetComponent<WorldWrapper>());
            BoxCollider2D bc2d = template.AddComponent<BoxCollider2D>();
            bc2d.isTrigger = true;
            bc2d.size = new Vector2(worldWrapper.edgeWidth, worldWrapper.worldHeight);
            bc2d.offset = new Vector2(0, worldWrapper.worldHeight/2);
            WorldWrapperEdge wwe = template.AddComponent<WorldWrapperEdge>();
            wwe.controller = worldWrapper;
            //Setup children
            GameObject left = GameObject.Instantiate(template);
            left.transform.parent = worldWrapper.transform;
            left.transform.position = worldWrapper.transform.position
                + Vector3.left * (worldWrapper.worldWidth / 2);
            left.name = "Left WorldWrapperEdge";
            GameObject right = GameObject.Instantiate(template);
            right.transform.parent = worldWrapper.transform;
            right.transform.position = worldWrapper.transform.position
                + Vector3.right * (worldWrapper.worldWidth / 2);
            right.name = "Right WorldWrapperEdge";
            //Delete template
            DestroyImmediate(template);
        }
    }
}
