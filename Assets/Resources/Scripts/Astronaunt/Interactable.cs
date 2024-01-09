using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject InteractImage;
    private CanvasGroup imageCanvasGroup;
    private CanvasGroup canvasLootUI;
    private float timerLootMessage = 3f;

    private Interactable_Object interactable;

    public bool isAccessFirstBay;
    [SerializeField] 
    private bool haveCardFirstBay; 
    public bool isAccessSecondBay;
    [SerializeField] 
    private bool haveCardSecondBay;
    public bool isAccessThirdBay;
    [SerializeField] 
    private bool haveCardThirdBay;
    [SerializeField] 
    private bool haveCanister;
    private float canisterValue = 0f;
    [SerializeField] 
    private AudioClip playerAccess;

    [SerializeField] 
    private bool haveFuse;

    private int countFuse = 0;

    private AudioSource playerAudioSource;
    private int questItemsCount = 0;
    public bool firstItemHave;
    public bool secondItemHave;
    public bool thirdItemHave;
    [SerializeField] private AudioClip questtakeAudioClip;

    private bool isStartComplited;
    void Start()
    {
        imageCanvasGroup = InteractImage.GetComponent<CanvasGroup>();
        canvasLootUI = GameObject.Find("Loot message").GetComponentInChildren<CanvasGroup>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && interactable != null)
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


    public void AddFuse()
    {
        countFuse++;
        haveFuse = true;
    }

    public void RemoveFuse()
    {
        countFuse--;
        if(countFuse <1)
            haveFuse = false;
    }

    public bool GetFuse()
    {
        return haveFuse;
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
        switch (number)
        {
            case 1:
                if (haveCardFirstBay)
                {
                    isAccessFirstBay = true;
                    playerAudioSource.PlayOneShot(playerAccess);
                }
                break;
            case 2:
                if (haveCardSecondBay)
                {
                    isAccessSecondBay = true;
                    playerAudioSource.PlayOneShot(playerAccess);
                }
                break;
            case 3:
                if (haveCardThirdBay)
                {
                    isAccessThirdBay = true;
                    playerAudioSource.PlayOneShot(playerAccess);
                }
                break;
        }

    }

    public void AddCanister()
    {
        haveCanister = true;
        canisterValue = 1f;
    }

    public void RemoveCanister()
    {
        haveCanister = false;
        canisterValue = 0f;
    }

    public float GetCanisterValue()
    {
        return canisterValue;
    }

    public void AddQuestItem(string nameItem)
    {
        questItemsCount++;
        canvasLootUI.alpha = 1f;
        var text = GameObject.Find("Loot message").transform.GetChild(0).GetComponent<Text>();
        text.text = $"Вы собрали важный компонент - {nameItem}! Всего собрано {questItemsCount} / 3";
        playerAudioSource.PlayOneShot(questtakeAudioClip);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MusicZone")
        {
            var number = other.name.Split(' ')[1];
            var musicManager = transform.gameObject.GetComponentInParent<MusicManager>();

            switch (number)
            {
                case "1":
                    musicManager.SetParameterCounter(0,2);
                    break;
                case "2":
                    musicManager.SetParameterCounter(3, 4);
                    break;
                case "3":
                    musicManager.SetParameterCounter(5, 6);
                    break;
            }
        }

        if(other.tag == "StartZone")
            isStartComplited = true;

        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 1.0f;
        imageCanvasGroup.blocksRaycasts = true;
        imageCanvasGroup.interactable = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"{interactable}");
        if ((interactable == other.GetComponent<Interactable_Object>() || other.tag != "Object") && interactable == null) return;
        else if (interactable == null)
        {
            interactable = other.GetComponent<Interactable_Object>();
        }
        if(other.tag != "ObjectTileMap")
        {
            imageCanvasGroup.alpha = 1.0f;
            imageCanvasGroup.blocksRaycasts = true;
            imageCanvasGroup.interactable = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (interactable == null) return;
        Debug.Log("END");
        interactable.EndInteract();
        RemoveInterectable();
        imageCanvasGroup.alpha = 0.0f;
        imageCanvasGroup.blocksRaycasts = false;
        imageCanvasGroup.interactable = false;
    }

    public void RemoveInterectable()
    {
        interactable = null;
    }

    public bool GetStartStatus()
    {
        return isStartComplited;
    }

    public int GetCountQuestItems()
    {
        return questItemsCount;
    }

    public bool GetFirstItemStatus()
    {
        return firstItemHave;
    }

    public bool GetSecondItemStatus()
    {
        return secondItemHave;
    }

    public bool GetThirdItemStatus()
    {
        return thirdItemHave;
    }
}
