using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

namespace Assets.Scripts
{
    internal static class DialogueHelper
    {
        public static void ShowDialogue(GameObject dialoguePanel, TMP_Text dialogueText, string message)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = message;
        }
        public static string GetInitialDialogue(GameObject currentScene)
        {
            string initialDialogue = "";
            switch (currentScene.name)
            {
                case "Bedroom":
                    initialDialogue = "I wake up sore and tired. I have no memory of last night. I'm alone in bed and thirsty. Eyes barely open, I fumble around looking for something to drink...";
                    break;
                case "Bathroom":
                    initialDialogue = "I make my way to the bathroom.";
                    break;
                case "Kitchen":
                    initialDialogue = "I'm not hungry but out of habit I wander into the kitchen after getting dressed.";
                    break;
                case "Airport":
                    initialDialogue = "I arrive at LaGuardia.";
                    break;
                case "Flight":
                    initialDialogue = "Hmm, meal service...";
                    break;
                case "Brother":
                    initialDialogue = "We land and I'm greeted by my little brother. He needs help carrying furniture and I guess my plane ticket was cheaper than hiring professional movers...";
                    break;
                default:
                    initialDialogue = "I'm not sure what to say...";
                    break;
            }
            return initialDialogue;
        }
    }
}
