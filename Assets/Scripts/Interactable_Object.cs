using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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


    private Interactable playerAccess;
    private Health playerHealth;
    private Oxygen playerOxygen;

    [Header("Door panel parameter")] 
    [SerializeField] private GameObject panel;
    [SerializeField] private Interactable_Object door;
    private Button openButton;
    private Button closeButton;

    [Header("Door access parameter")] 
    [SerializeField] private bool isNeedFirstBayCard;
    [SerializeField] private bool isNeedSecondBayCard;
    [SerializeField] private bool isNeedThirdBayCard;

    [Header("Box parameter")] 
    [SerializeField] private BoxLoot loot;
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer sprite;
    private GameObject lootUI;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorBoxCollider = GetComponent<BoxCollider2D>();
        doorAudioSource = GetComponent<AudioSource>();
        playerAccess = GameObject.Find("Player").GetComponent<Interactable>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerOxygen = GameObject.Find("Player").GetComponent<Oxygen>();

        lootUI = GameObject.Find("Loot message");
        if (type == ObjectType.DoorPanel)
        {
            var buttons = panel.transform.GetChild(1).gameObject;
            openButton = buttons.transform.GetChild(0).GetComponent<Button>();
            closeButton = buttons.transform.GetChild(1).GetComponent<Button>();

            openButton.onClick.AddListener(door.DoorOpen);
            closeButton.onClick.AddListener(door.DoorCLose);
        }
        sprite = GetComponent<SpriteRenderer>();
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
            case ObjectType.Box:
                sprite.sprite = openSprite;
                var canvasLootUI = lootUI.GetComponent<CanvasGroup>();
                var textLootMessage = lootUI.GetComponentInChildren<Text>();

                canvasLootUI.alpha = 1.0f;
                canvasLootUI.blocksRaycasts = true;

                if(loot != BoxLoot.Nothing)
                {
                    textLootMessage.text = $"Вы нашли {loot}!";
                    doorAudioSource.PlayOneShot(openSound);
                    SpawnLoot();
                }
                else
                {
                    textLootMessage.text = $"Пусто!"; 
                    doorAudioSource.PlayOneShot(openSound);
                }


                Destroy(doorBoxCollider);
                Destroy(this);
                break;
            default:
                switch (loot)
                {
                    case BoxLoot.MedKit:
                        playerHealth.AddHealth(25);
                        Destroy(gameObject);
                        break;
                    case BoxLoot.OxygenKit:
                        playerOxygen.AddOxygen(15);
                        Destroy(doorBoxCollider);
                        Destroy(this);
                        break;
                    case BoxLoot.Card1:
                        playerAccess.AddCard(1);
                        Destroy(gameObject);
                        break;
                    case BoxLoot.Card2:
                        playerAccess.AddCard(2);
                        Destroy(gameObject);
                        break;
                    case BoxLoot.Card3:
                        playerAccess.AddCard(3);
                        Destroy(gameObject);
                        break;
                    case BoxLoot.Canister:
                        playerAccess.AddCanister();
                        Destroy(gameObject);
                        break;
                    case BoxLoot.FlashDrive:
                        break;
                }
                break;
        }
    }

    public void DoorOpen()
    {
        if (isNeedFirstBayCard || isNeedSecondBayCard || isNeedThirdBayCard)
            if (CheckCardAccess())
            {
                animator.SetTrigger("Open");
                doorAudioSource.PlayOneShot(openSound);
                isOpenDoor = true;
                doorBoxCollider.enabled = false;
            }
            else
                return;
        animator.SetTrigger("Open");
        doorAudioSource.PlayOneShot(openSound);
        isOpenDoor = true;
        doorBoxCollider.enabled = false;
    }

    public void DoorCLose()
    {
        if (isNeedFirstBayCard || isNeedSecondBayCard || isNeedThirdBayCard)
            if (CheckCardAccess())
            {
                animator.SetTrigger("Close");
                doorAudioSource.PlayOneShot(closeSound);
                isOpenDoor = false;
                doorBoxCollider.enabled = true;
            }
            else
                return;
        animator.SetTrigger("Close");
        doorAudioSource.PlayOneShot(closeSound);
        isOpenDoor = false;
        doorBoxCollider.enabled = true;
    }

    private bool CheckCardAccess()
    {
        if (isNeedFirstBayCard)
            return playerAccess.isAccessFirstBay;
        else if (isNeedSecondBayCard)
            return playerAccess.isAccessSecondBay;
        return isNeedThirdBayCard && playerAccess.isAccessThirdBay;
    }

    private void SpawnLoot()
    {
        switch (loot)
        {
            case BoxLoot.MedKit:
                var prefab1 = AssetDatabase.LoadAssetAtPath("Assets/GameObjects/Prefabs/MedKit.prefab", typeof(GameObject));
                var obj1 = Object.Instantiate(prefab1);
                obj1.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.OxygenKit:
                var prefab2 = AssetDatabase.LoadAssetAtPath("Assets/GameObjects/Prefabs/OxygenBalloon.prefab", typeof(GameObject));
                var obj2 = Object.Instantiate(prefab2);
                obj2.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card1:
                var prefab3 = AssetDatabase.LoadAssetAtPath("Assets/GameObjects/Prefabs/Card1.prefab", typeof(GameObject));
                var obj3 = Object.Instantiate(prefab3);
                obj3.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card2:
                var prefab4 = AssetDatabase.LoadAssetAtPath("Assets/GameObjects/Prefabs/Card2.prefab", typeof(GameObject));
                var obj4 = Object.Instantiate(prefab4);
                obj4.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card3:
                var prefab5 = AssetDatabase.LoadAssetAtPath("Assets/GameObjects/Prefabs/Card3.prefab", typeof(GameObject));
                var obj5 = Object.Instantiate(prefab5);
                obj5.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
        }
    }

    private enum ObjectType
    {
        Door = 0,
        DoorConsole = 1,
        DoorPanel = 2,
        MainDoor = 3,
        Box = 4,
        Generator = 5,
        Other = 6,
    }

    private enum BoxLoot
    {
        Nothing = 0,
        Card1 = 1, 
        Card2 = 2, 
        Card3 = 3,
        MedKit = 4,
        OxygenKit = 5,
        FlashDrive = 6,
        Canister = 7,

    }
}
