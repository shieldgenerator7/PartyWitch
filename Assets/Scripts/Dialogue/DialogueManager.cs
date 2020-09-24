using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dialogue
{
    /// <summary>
    /// Determines which dialogue happens when you want to trigger a dialogue
    /// </summary>
    class DialogueManager : MonoBehaviour
    {
        private DialogueData dialogueData;
        [SerializeField]
        private DialoguePlayer dialoguePlayer;

        private void Awake()
        {
            string jsonString = Resources.Load<TextAsset>("dialogues").text;
            Debug.Log(jsonString);
            dialogueData = JsonUtility.FromJson<DialogueData>(jsonString);
            dialogueData.dialogues.ForEach(d => d.inflate());
            Debug.Log("dialogueData: " + dialogueData);
            Debug.Log("dialogueData.chars: ");
            dialogueData.dialogues[2].Characters.ForEach(
            chr => Debug.Log("  >>  " + chr)
            );
            int i = 0;
            //testUnityJSON();
        }

        private void testUnityJSON()
        {
            dialogueData = new DialogueData();
            List<DialoguePath> dialogues = dialogueData.dialogues;
            DialoguePath d = new DialoguePath();
            d.quotes.Add(new Quote("Jubilee", "Hello there!"));
            d.quotes.Add(new Quote("Grim", "Hi, I guess."));
            dialogues.Add(d);
            dialogues.Add(new DialoguePath());
            dialogues.Add(new DialoguePath());
            //dialogueData.prepareForJSON();
            string jsonString = JsonUtility.ToJson(dialogueData, true);
            Debug.Log("JSON: " + jsonString);
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
