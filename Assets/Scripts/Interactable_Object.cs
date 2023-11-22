using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable_Object : MonoBehaviour
{
    [Header("Common parameter")]
    [SerializeField] private ObjectType type;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private AudioSource doorAudioSource;
    private bool isOpenDoor = false;
    private Animator animator;
    private BoxCollider2D doorBoxCollider;
    [Header("Door panel parameter")] 
    [SerializeField] private GameObject panel;
    [SerializeField] private Interactable_Object door;
    private Button openButton;
    private Button closeButton;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorBoxCollider = GetComponent<BoxCollider2D>();
        doorAudioSource = GetComponent<AudioSource>();
        if(type == ObjectType.DoorPanel)
        {
            var buttons = panel.transform.GetChild(1).gameObject;
            openButton = buttons.transform.GetChild(0).GetComponent<Button>();
            closeButton = buttons.transform.GetChild(1).GetComponent<Button>();

            openButton.onClick.AddListener(door.DoorOpen);
            closeButton.onClick.AddListener(door.DoorCLose);
        }
    }

    void Update()
    {

    }

    public void Interact()
    {
        switch (type)
        {
            case ObjectType.Door:
                if(!isOpenDoor)
                    DoorOpen();
                else if (isOpenDoor)
                    DoorCLose();
                break;
            case ObjectType.DoorPanel:
                var canvasPanel = panel.GetComponent<CanvasGroup>();
                canvasPanel.alpha = 1.0f;
                canvasPanel.interactable = true;
                canvasPanel.blocksRaycasts = true;
                break;
        }
    }

    public void DoorOpen()
    {
        animator.SetTrigger("Open");
        doorAudioSource.PlayOneShot(openSound);
        isOpenDoor = true;
        doorBoxCollider.enabled = false;
    }

    public void DoorCLose()
    {
        animator.SetTrigger("Close");
        doorAudioSource.PlayOneShot(closeSound);
        isOpenDoor = false;
        doorBoxCollider.enabled = true;
    }

    private enum ObjectType
    {
        Door = 0,
        DoorConsole = 1,
        DoorPanel = 2,
        MainDoor = 3,
    }
}
