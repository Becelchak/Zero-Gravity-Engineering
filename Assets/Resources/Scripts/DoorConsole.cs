using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConsole : Interactable_Object
{
    [SerializeField]
    private CanvasGroup console;

    [SerializeField] 
    private CanvasGroup closer;

    private float timer;

    void Update()
    {
        
    }

    public void Interact()
    {
        console.alpha = 1;
        console.blocksRaycasts = true;
        console.interactable = true;

        closer.alpha = 1;
        closer.blocksRaycasts = true;
        closer.interactable = true;
    }

    public void EndInteract()
    {
        console.alpha = 0;
        console.blocksRaycasts = false;
        console.interactable = false;

        closer.alpha = 0;
        closer.blocksRaycasts = false;
        closer.interactable = false;
    }
}
