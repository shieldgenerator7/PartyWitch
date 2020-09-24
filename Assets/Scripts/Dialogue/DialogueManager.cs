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
    class DialogueManager: MonoBehaviour
    {
        public DialogueData dialogueData;

        private void Awake()
        {
            string jsonString = Resources.Load<TextAsset>("dialogues").text;
            Debug.Log(jsonString);
            dialogueData = JsonUtility.FromJson<DialogueData>(jsonString);
            dialogueData.dialogues.ForEach(d => d.inflate());
            Debug.Log("dialogueData: " + dialogueData);
            int i = 0;
            //testUnityJSON();
        }

        private void testUnityJSON()
        {
            dialogueData = new DialogueData();
            List<DialoguePath> dialogues =  dialogueData.dialogues;
            DialoguePath d = new DialoguePath();
            d.quotes.Add(new Quote("Jubilee", "Hello there!"));
            d.quotes.Add(new Quote("Grim", "Hi, I guess."));
            dialogues.Add(d);
            dialogues.Add(new DialoguePath());
            dialogues.Add(new DialoguePath());
            //dialogueData.prepareForJSON();
            string jsonString = JsonUtility.ToJson(dialogueData, true);
            Debug.Log("JSON: "+jsonString);
        }
    }
}
