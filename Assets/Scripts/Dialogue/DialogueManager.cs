using System;
using System.Collections.Generic;
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

        private void Awake()
        {
            string jsonString = Resources.Load<TextAsset>("dialogues").text;
            dialogueData = JsonUtility.FromJson<DialogueData>(jsonString);
            dialogueData.dialogues.ForEach(d => d.inflate());
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
            DialoguePath path = dialogueData.getDialoguePath(characters);
            playDialogue(path);
        }

        public void playDialogue(DialoguePath path)
        {
            dialoguePlayer.playDialogue(path);
        }
    }
}
