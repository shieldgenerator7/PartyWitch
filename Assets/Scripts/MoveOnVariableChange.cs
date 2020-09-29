using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnVariableChange : MonoBehaviour
{
    public string variableName;
    public float targetThreshold = 0.1f;
    public List<Vector2> positions;

    private Vector2 targetPos;

    // Start is called before the first frame update
    private void Start()
    {
        int index = FindObjectOfType<DialogueManager>().progressManager.get(variableName);
        index = Mathf.Clamp(index, 0, positions.Count - 1);
        targetPos = positions[index];
    }
    void OnEnable()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= checkVariable;
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange += checkVariable;
    }

    private void OnDisable()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= checkVariable;
    }

    private void OnDestroy()
    {
        FindObjectOfType<DialogueManager>().progressManager
            .onVariableChange -= checkVariable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) > targetThreshold)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime);
        }
    }

    void checkVariable(string varName, int oldValue, int newValue)
    {
        if (varName == this.variableName)
        {
            newValue = Mathf.Clamp(newValue, 0, positions.Count - 1);
            targetPos = positions[newValue];
        }
    }
}
