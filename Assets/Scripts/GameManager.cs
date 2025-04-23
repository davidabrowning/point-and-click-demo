using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject titlePanel;
    public TMP_Text titleText;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public GameObject clickableObjects;
    public List<GameObject> scenes = new List<GameObject>();
    private int sceneCounter;
    private int currentSceneProgressCounter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        scenes[0].SetActive(true);
        titlePanel.SetActive(true);
    }

    public void ShowDialogue(string message)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;

        // Handle any special cases
        string currentSceneName = scenes[sceneCounter].name;
        if (currentSceneName == "Kitchen" && currentSceneProgressCounter > 0)
            dialogueText.text = "I call the airport and they confirm I am on that flight. I have no idea who bought the ticket for me...";
    }

    public void HideObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void AdvanceStory()
    {
        // Exit criteria
        string currentSceneName = scenes[sceneCounter].name;
        if (currentSceneName == "Kitchen" && currentSceneProgressCounter == 0)
            return;

        sceneCounter++;
        currentSceneProgressCounter = 0;
        GameObject previousScene = scenes[sceneCounter - 1];
        GameObject nextScene = scenes[sceneCounter];
        float sceneEndDelay;
        if (previousScene.name == "MainMenu")
        {
            sceneEndDelay = 0f;
            titlePanel.SetActive(false);
        }
        else
        {
            sceneEndDelay = 5f;
        }
        StartCoroutine(UpdateScene(previousScene, nextScene, sceneEndDelay));
    }

    public void AdvanceScene()
    {
        currentSceneProgressCounter++;
    }

    private IEnumerator UpdateScene(GameObject previousScene, GameObject nextScene, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        previousScene.SetActive(false);
        nextScene.SetActive(true);

        PopulateInitialDialogue();
    }

    private void PopulateInitialDialogue()
    {
        string currentSceneName = scenes[sceneCounter].name;
        string initialDialogue = "";
        switch (currentSceneName)
        {
            case "Bedroom":
                initialDialogue = "I wake up sore and tired. I have no memory of last night. I'm alone in bed and thirsty. Eyes barely open, I fumble around looking for something to drink...";
                break;
            case "Bathroom":
                initialDialogue = "I make my way to the bathroom to rinse off whatever happened yesterday.";
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
        ShowDialogue(initialDialogue);
    }

    public void GameOver(string message)
    {
        clickableObjects.SetActive(false);
        gameOverText.text = "Game over";
        StartCoroutine(UpdateScene(scenes[sceneCounter], gameOverPanel, 5f));
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        clickableObjects.SetActive(true);
    }

}
