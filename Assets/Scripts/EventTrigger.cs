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
        coll2d = GetComponents<Collider2D>().FirstOrDefault(c2d => c2d.isTrigger == true);
        if (!coll2d)
        {
            Debug.LogError("" + this.GetType().Name + " requires a Collider2D with isTrigger set to true. "
                + "This one on GameObject " + gameObject.name + " has none.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.OnPlayerInteract += triggerEvent;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.OnPlayerInteract -= triggerEvent;
        }
    }

    protected abstract void triggerEvent();
}
