using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorPanel : Interactable_Object
{
    [Header("Common parameter")]
    private AudioSource doorAudioSource;
    private BoxCollider2D doorBoxCollider;


    private Interactable playerAccess;
    private Health playerHealth;
    private Oxygen playerOxygen;
    private float playerFuelValue;

    [Header("Door panel parameter")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Door door;
    private Button openButton;
    private Button closeButton;

    void Start()
    {
        doorBoxCollider = GetComponent<BoxCollider2D>();
        doorAudioSource = GetComponent<AudioSource>();
        var player = GameObject.Find("Player");
        playerAccess = player.GetComponent<Interactable>();
        playerHealth = player.GetComponent<Health>();
        playerOxygen = player.GetComponent<Oxygen>();

        var buttons = panel.transform.GetChild(1).gameObject;
        openButton = buttons.transform.GetChild(0).GetComponent<Button>();
        closeButton = buttons.transform.GetChild(1).GetComponent<Button>();

        openButton.onClick.AddListener(door.DoorOpen);
        closeButton.onClick.AddListener(door.DoorCLose);
    }

    public void Interact()
    {
        var canvasPanel = panel.GetComponent<CanvasGroup>();
        canvasPanel.alpha = 1.0f;
        canvasPanel.interactable = true;
        canvasPanel.blocksRaycasts = true;
    }

    public void EndInteract()
    {
        var canvasPanel = panel.GetComponent<CanvasGroup>();
        canvasPanel.alpha = 0.0f;
        canvasPanel.interactable = false;
        canvasPanel.blocksRaycasts = false;
    }
}
