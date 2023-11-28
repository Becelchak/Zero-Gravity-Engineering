using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject InteractImage;
    private CanvasGroup imageCanvasGroup;
    private CanvasGroup canvasLootUI;
    private float timerLootMessage = 3f;
    private Interactable_Object interactable;
    public bool isAccessFirstBay;
    [SerializeField] private bool haveCardFirstBay; 
    public bool isAccessSecondBay;
    [SerializeField] private bool haveCardSecondBay;
    public bool isAccessThirdBay;
    [SerializeField] private bool haveCardThirdBay;
    [SerializeField] private bool haveCanister;
    void Start()
    {
        imageCanvasGroup = InteractImage.GetComponent<CanvasGroup>();
        canvasLootUI = GameObject.Find("Loot message").GetComponentInChildren<CanvasGroup>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && interactable != null)
        {
            interactable.Interact();
        }

        if (timerLootMessage <= 0)
        {
            canvasLootUI.alpha = 0;
            timerLootMessage = 3f;
        }
        if (canvasLootUI.alpha == 1.0f)
            timerLootMessage -= Time.deltaTime;
    }

    public void AddCard(int number)
    {
        switch (number)
        {
            case 1:
                haveCardFirstBay = true;
                break;
            case 2:
                haveCardSecondBay = true;
                break;
            case 3:
                haveCardThirdBay = true;
                break;
        }
        
    }

    public void AddAccess(int number)
    {
        Debug.Log($"{haveCardFirstBay}");
        switch (number)
        {
            case 1:
                if (haveCardFirstBay)
                    isAccessFirstBay = true;
                break;
            case 2:
                if (haveCardSecondBay)
                    isAccessSecondBay = true;
                break;
            case 3:
                if (haveCardThirdBay)
                    isAccessThirdBay = true;
                break;
        }

    }

    public void AddCanister()
    {
        haveCanister = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 1.0f;
        imageCanvasGroup.blocksRaycasts = true;
        imageCanvasGroup.interactable = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 1.0f;
        imageCanvasGroup.blocksRaycasts = true;
        imageCanvasGroup.interactable = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 0.0f;
        imageCanvasGroup.blocksRaycasts = false;
        imageCanvasGroup.interactable = false;
    }
}
