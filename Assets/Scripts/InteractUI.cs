using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractUI : MonoBehaviour
{
    public static InteractUI instance { get; private set; }

    private readonly List<EventTrigger> triggerQueue = new List<EventTrigger>();

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

    private DialoguePlayer dialoguePlayer;
    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("InteractUI is already bound!");
            Destroy(instance.gameObject);
        }
        instance = this;
        gameObject.SetActive(false);
        SceneManager.sceneUnloaded += (s) => triggerQueue.Clear();
        dialoguePlayer = FindObjectOfType<DialoguePlayer>();
        PlayerController.OnPlayerInteract += interactPressed;
    }

    public void interactPressed()
    {
        if (!dialoguePlayer.Playing)
        {
            if (triggerQueue.Count > 0)
            {
                triggerQueue[0].triggerEvent();
            }
        }
    }

    public void registerTrigger(EventTrigger trigger, bool register = true)
    {
        if (register)
        {
            triggerQueue.Add(trigger);
        }
        else
        {
            triggerQueue.Remove(trigger);
        }
        updateInteractUI();
    }

    private void updateInteractUI()
    {
        bool canShow = triggerQueue.Count > 0 && !Suppressed;
        gameObject.SetActive(canShow);
        if (canShow)
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
