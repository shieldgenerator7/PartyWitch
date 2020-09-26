using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component marks a GameObject as a clone made by the WorldWrapper
/// by shieldgenerator7
/// </summary>
public class WorldWrapperClone : MonoBehaviour
{

    public WorldWrapper worldWrapper;
    public GameObject original;

    private Vector3 offset;

    private Rigidbody2D rb2d;
    private Rigidbody2D rb2dOriginal;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void init()
    {
        offset = transform.position - original.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        rb2dOriginal = original.GetComponent<Rigidbody2D>();
        if (rb2d && rb2dOriginal)
        {
            sync();
        }
        else
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sync();
    }

    public void sync()
    {
        transform.position = original.transform.position + offset;
        transform.rotation = original.transform.rotation;
        transform.localScale = original.transform.localScale;
        rb2d.velocity = rb2dOriginal.velocity;
        rb2d.angularVelocity = rb2dOriginal.angularVelocity;
    }

    public void unclone()
    {
        Destroy(this.gameObject);
    }
}
