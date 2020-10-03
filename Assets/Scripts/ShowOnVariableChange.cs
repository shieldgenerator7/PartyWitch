using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowOnVariableChange : MonoBehaviour
{
    public GameObject showGameObject;
    public TMP_Text txtUpdate;
    public StringBank stringBank;
    public string message = "[x] is a message.";
    public string varStandIn = "[x]";
    public float spinSpeed = 5;
    public GameObject spinObject;
    public AudioSource audioSource;

    public float duration = 3;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange += checkPlay;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime > 0)
        {
            //Spin the object
            Vector3 angles = spinObject.transform.eulerAngles;
            angles.z += spinSpeed;
            spinObject.transform.eulerAngles = angles;
            //Check for stop
            if (Time.time > startTime + duration)
            {
                stop();
            }
        }
    }

    private void checkPlay(string varName, int oldValue, int newValue)
    {
        if (stringBank.Contains(varName))
        {
            play(varName);
        }
    }

    private void play(string varName)
    {
        string bankString = stringBank.getString(varName);
        string newText = bankString + message;
        if (message.Contains(varStandIn))
        {
            newText = message.Replace(varStandIn, bankString);
        }
        txtUpdate.text = newText;
        showGameObject.SetActive(true);
        startTime = Time.time;
        InteractUI.instance.suppress(this);
        audioSource.Play();
    }
    private void stop()
    {
        showGameObject.SetActive(false);
        startTime = -1;
        InteractUI.instance.suppress(this, false);
    }
}
