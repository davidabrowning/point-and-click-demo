using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string message;
    public bool updateDialogue;
    public bool isGameOver;
    public bool advancesStory;
    public float secondsToDelayBeforeAdvancingStory = 5f;
    public bool advancesScene;
    public List<GameObject> objectsThatDisappearOnClick = new List<GameObject>();
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }
    private void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        foreach (GameObject gameObject in objectsThatDisappearOnClick)
            GameManager.Instance.HideObject(gameObject);
        if (updateDialogue)
            GameManager.Instance.ShowDialogue(message);
        if (isGameOver)
            GameManager.Instance.GameOver(message);
        if (advancesStory)
            GameManager.Instance.AdvanceStory(secondsToDelayBeforeAdvancingStory);
        if (advancesScene)
            GameManager.Instance.AdvanceScene();
    }
}
