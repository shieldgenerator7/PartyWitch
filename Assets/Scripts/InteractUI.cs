using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public static InteractUI instance { get; private set; }

    private readonly List<EventTrigger> triggerQueue = new List<EventTrigger>();


    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("InteractUI is already bound!");
            Destroy(instance.gameObject);
        }
        instance = this;
        gameObject.SetActive(false);
    }

    public void grabInteractUI(EventTrigger trigger)
    {
        triggerQueue.Add(trigger);
        updateInteractUI();
    }

    public void letgoInteractUI(EventTrigger trigger)
    {
        triggerQueue.Remove(trigger);
        updateInteractUI();
    }

    private void updateInteractUI()
    {
        gameObject.SetActive(triggerQueue.Count > 0);
        if (triggerQueue.Count > 0)
        {
            SpriteRenderer triggerSR = findSpriteRenderer(triggerQueue[0]);
            if (triggerSR)
            {
                transform.position = new Vector2(
                    triggerQueue[0].transform.position.x,
                    triggerSR.bounds.max.y);
            }
        }
    }

    private SpriteRenderer findSpriteRenderer(EventTrigger trigger)
    {
        SpriteRenderer triggerSR = trigger.GetComponent<SpriteRenderer>();
        if (triggerSR)
        {
            return triggerSR;
        }
        triggerSR = trigger.GetComponentInChildren<SpriteRenderer>();
        if (triggerSR)
        {
            return triggerSR;
        }
        triggerSR = trigger.GetComponentInParent<SpriteRenderer>();
        if (triggerSR)
        {
            return triggerSR;
        }
        return null;
    }

}
