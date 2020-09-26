using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tells the WorldWrapper that something has left or entered its trigger
/// by shieldgenerator7
/// </summary>
public class WorldWrapperEdge : MonoBehaviour
{
    public WorldWrapper controller;

    private BoxCollider2D bc2d;

    private void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.objectEntered(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.objectExited(collision.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.objectMoved(collision.gameObject, this);
    }

    public bool isPastEdge(Vector3 point, Vector3 middle)
        => Mathf.Sign(point.x - transform.position.x)
        != Mathf.Sign(middle.x - transform.position.x);
}
