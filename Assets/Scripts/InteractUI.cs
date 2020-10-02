using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractUI : MonoBehaviour
{
    public static InteractUI instance { get; private set; }

    private readonly List<EventTrigger> triggers = new List<EventTrigger>();

    private bool _suppress = false;
    public bool Suppressed
    {
        get => _suppress;
        set
        {
            _suppress = value;
            updateInteractUI();
        }
    }

    private EventTrigger currentTrigger;

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("InteractUI is already bound!");
            Destroy(instance.gameObject);
        }
        instance = this;
        gameObject.SetActive(false);
        SceneManager.sceneUnloaded += (s) => triggers.Clear();
        PlayerController.OnPlayerInteract += interactPressed;
    }

    public void interactPressed()
    {
        if (!Suppressed)
        {
            currentTrigger?.processTrigger();
        }
    }

    public void registerTrigger(EventTrigger trigger, bool register = true)
    {
        if (register)
        {
            triggers.Add(trigger);
        }
        else
        {
            triggers.Remove(trigger);
        }
        updateInteractUI();
    }

    private void updateInteractUI()
    {
        currentTrigger = null;
        bool canShow = triggers.Count > 0 && !Suppressed;
        gameObject.SetActive(canShow);
        if (canShow)
        {
            selectTrigger();
            SpriteRenderer triggerSR = findSpriteRenderer(currentTrigger);
            if (triggerSR)
            {
                transform.position = new Vector2(
                    currentTrigger.transform.position.x,
                    triggerSR.bounds.max.y);
            }
        }
    }

    private void selectTrigger()
    {
        if (triggers.Count > 0)
        {
            Vector2 playerPos = FindObjectOfType<PlayerController>().transform.position;
            currentTrigger = triggers.OrderBy(
                t => Vector2.Distance(
                    playerPos,
                    t.GetComponent<Collider2D>().bounds.center
                    )
                ).FirstOrDefault();
        }
        else
        {
            currentTrigger = null;
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
