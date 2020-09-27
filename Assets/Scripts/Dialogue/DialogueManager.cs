using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Dialogue
{
    /// <summary>
    /// Determines which dialogue happens when you want to trigger a dialogue
    /// </summary>
    [RequireComponent(typeof(DialoguePlayer))]
    class DialogueManager : MonoBehaviour
    {
        private DialogueData dialogueData;
        [SerializeField]
        private DialoguePlayer dialoguePlayer;

        private ProgressManager progressManager;

        private void Awake()
        {
            string jsonString = Resources.Load<TextAsset>("dialogues").text;
            dialogueData = JsonUtility.FromJson<DialogueData>(jsonString);
            dialogueData.dialogues.ForEach(d => d.inflate());
            dialoguePlayer.onDialogueEnded += takeActions;
            progressManager = new ProgressManager();
        }

        public void playDialogue(string title = null)
        {
            DialoguePath path = null;
            if (title == null || title == "")
            {
                //2020-09-24: TODO: make it search for characters
                //path = dialogueData.selectSuitableDialoguePath();

                //can't do anything (for now)
                throw new NullReferenceException("Title must be non-null and must not be the empty string! title: " + title);
            }
            else
            {
                path = dialogueData.getDialoguePath(title);
            }
            playDialogue(path);
        }

        public void playDialogue(List<string> characters)
        {
            DialoguePath path = dialogueData.getDialoguePaths(characters)
                .FirstOrDefault(dp => conditionsMet(dp));
            playDialogue(path);
        }

        public void playDialogue(DialoguePath path)
        {
            dialoguePlayer.playDialogue(path);
        }

        private bool conditionsMet(DialoguePath path)
        {
            return path.conditions.All(c => conditionMet(c));
        }

        private bool conditionMet(Condition c)
        {
            int value = progressManager.get(c.variableName);
            switch (c.testType)
            {
                case Condition.TestType.EQUAL: return value == c.testValue;
                case Condition.TestType.NOT_EQUAL: return value != c.testValue;
                case Condition.TestType.GREATER_THAN: return value > c.testValue;
                case Condition.TestType.GREATER_THAN_EQUAL: return value >= c.testValue;
                case Condition.TestType.LESS_THAN: return value < c.testValue;
                case Condition.TestType.LESS_THAN_EQUAL: return value <= c.testValue;
                default: throw new ArgumentException("condition testType is not valid: " + c.testType);
            }
        }

        private void takeActions(DialoguePath path)
        {
            path.actions.ForEach(a => takeAction(a));
        }

        private void takeAction(Action a)
        {
            switch (a.actionType)
            {
                case Action.ActionType.SET: progressManager.set(a.variableName, a.actionValue); break;
                case Action.ActionType.ADD: progressManager.add(a.variableName, a.actionValue); break;
                case Action.ActionType.SUBTRACT: progressManager.add(a.variableName, -a.actionValue); break;
                case Action.ActionType.MULTIPLY: progressManager.multiply(a.variableName, a.actionValue); break;
                case Action.ActionType.DIVIDE: progressManager.multiply(a.variableName, 1/a.actionValue); break;
                default: throw new ArgumentException("Action testType is not valid: " + a.actionType);
            }
        }
    }
}
