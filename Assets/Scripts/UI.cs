using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private CanvasGroup doorPanelCanvasGroup;
    void Start()
    {
        doorPanelCanvasGroup = GameObject.Find("Door panel").GetComponent<CanvasGroup>();
    }

    void Update()
    {

    }

    public void StartMainGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void CloseDoorPanel()
    {
        doorPanelCanvasGroup.alpha = 0f;
        doorPanelCanvasGroup.blocksRaycasts = true;
        doorPanelCanvasGroup.interactable = true;
    }
}
