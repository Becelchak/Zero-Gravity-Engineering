using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleElectric : Interactable_Object
{
    [SerializeField] 
    private CanvasGroup consoleCanvas;

    private Button slot1;
    private Button slot2;

    private bool slot1Full;
    private bool slot2Full;

    [SerializeField] 
    private Door controledDoor;

    [SerializeField]
    private Interactable player;
    void Start()
    {
        slot1 = GameObject.Find("Slot 1").GetComponent<Button>();
        slot2 = GameObject.Find("Slot 2").GetComponent<Button>();
    }

    void Update()
    {
        if (!player.GetFuse())
        {
            slot1.enabled = false;
            slot2.enabled = false;
        }

        if (slot1Full && slot2Full && !controledDoor.isOpenDoor)
        {
            controledDoor.DoorOpen();
        }
    }

    public void Interact()
    {
        consoleCanvas.alpha = 1;
        consoleCanvas.interactable = true;
        consoleCanvas.blocksRaycasts = true;

        if (player.GetFuse())
        {
            slot1.enabled = true;
            slot2.enabled = true;
        }
    }

    public void EndInteract()
    {
        consoleCanvas.alpha = 0;
        consoleCanvas.interactable = false;
        consoleCanvas.blocksRaycasts = false;
    }

    public void FullSlot(int numberSlot)
    {
        switch (numberSlot)
        {
            case 1:
                slot1Full = true;
                break;
            case 2:
                slot2Full = true;
                break;
        }
    }
}
