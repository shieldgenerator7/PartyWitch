using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class EventTrigger : MonoBehaviour
{
    private Collider2D coll2d;

    // Start is called before the first frame update
    void Start()
    {
        coll2d = GetComponents<Collider2D>().First(c2d => c2d.isTrigger == true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerEvent();
        }
    }

    protected abstract void triggerEvent();
}
