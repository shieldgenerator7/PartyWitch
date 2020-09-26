using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the world continuous left and right.
/// It teleports things that leave the area to the other side.
/// Place it in the center of the world and tell it the world width and it does the rest.
/// by shieldgenerator7
/// </summary>
public class WorldWrapper : MonoBehaviour
{
    public float worldWidth = 200;
    public float worldHeight = 10000;
    public float edgeWidth = 5;

    private Dictionary<GameObject, WorldWrapperClone> cloneMap = new Dictionary<GameObject, WorldWrapperClone>();

    public void objectEntered(GameObject go)
    {
        //If the object is not a clone,
        if (!go.GetComponent<WorldWrapperClone>())
        {
            //And it's not already cloned,
            if (!cloneMap.ContainsKey(go))
            {
                //Clone it
                clone(go);
            }
        }
    }

    public void objectExited(GameObject go)
    {
        //If the object is not a clone,
        if (!go.GetComponent<WorldWrapperClone>())
        {
            //Delete the clone
            if (cloneMap.ContainsKey(go))
            {
                cloneMap[go].unclone();
                cloneMap.Remove(go);
            }
        }
    }
    public void objectMoved(GameObject go, WorldWrapperEdge worldEdge)
    {
        //If the object is not a clone,
        if (!go.GetComponent<WorldWrapperClone>())
        {
            //And it's position is in the new worldEdge,
            if (worldEdge.isPastEdge(go.transform.position, transform.position))
            {
                //Teleport original to other side
                Vector3 oldPos = go.transform.position;
                go.transform.position = oppositeSide(go.transform.position);
                if (cloneMap.ContainsKey(go))
                {
                    cloneMap[go].transform.position = oldPos;
                    cloneMap[go].init();
                }
            }
        }
    }

    private void clone(GameObject go)
    {
        GameObject clone = Instantiate(go);
        WorldWrapperClone wwc = clone.AddComponent<WorldWrapperClone>();
        wwc.worldWrapper = this;
        wwc.original = go;
        clone.transform.position = oppositeSide(go.transform.position);
        clone.transform.parent = this.transform;
        wwc.init();
        cloneMap.Add(go, wwc);
    }

    private Vector3 oppositeSide(Vector3 v)
        => v +
        (Vector3.right * worldWidth
        * Mathf.Sign(transform.position.x - v.x)
        );
}
