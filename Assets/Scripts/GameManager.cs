using Assets.Scripts;
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
    public List<GameObject> scenes = new List<GameObject>();
    private int sceneCounter;
    private int currentSceneProgressCounter;
    public GameObject CurrentScene { get { return scenes[sceneCounter]; } }
    public string CurrentSceneName { get { return scenes[sceneCounter].name; } }
    public GameObject PreviousScene { get { return scenes[sceneCounter - 1]; } }
    public string PreviousSceneName { get { return scenes[sceneCounter - 1].name; } }

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
        // Handle any special cases
        if (CurrentSceneName == "Kitchen" && currentSceneProgressCounter > 0)
            message = "I call the airport and they confirm I am on that flight. I have no idea who bought the ticket for me...";

        // Update dialogue
        DialogueHelper.ShowDialogue(dialoguePanel, dialogueText, message);
    }

    public void HideObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void AdvanceStory(float secondsToDelayBeforeAdvancing)
    {
        // Exit criteria
        if (CurrentSceneName == "Kitchen" && currentSceneProgressCounter == 0)
            return;

        sceneCounter++;
        currentSceneProgressCounter = 0;
        if (PreviousSceneName == "MainMenu")
            titlePanel.SetActive(false);
        StartCoroutine(UpdateScene(PreviousScene, CurrentScene, secondsToDelayBeforeAdvancing));
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
        string initialDialogue = DialogueHelper.GetInitialDialogue(CurrentScene);
        ShowDialogue(initialDialogue);
    }

    public void GameOver(string message)
    {
        gameOverText.text = "Game over";
        StartCoroutine(UpdateScene(scenes[sceneCounter], gameOverPanel, 5f));
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
