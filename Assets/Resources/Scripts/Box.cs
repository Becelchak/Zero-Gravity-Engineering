using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Box : Interactable_Object
{
    [SerializeField] private AudioClip openSound;
    private AudioSource doorAudioSource;
    private bool isOpenDoor = false;
    private Animator animator;
    private BoxCollider2D doorBoxCollider;


    private Interactable playerAccess;
    private Health playerHealth;
    private Oxygen playerOxygen;
    private float playerFuelValue;

    [SerializeField] private BoxLoot loot;
    [SerializeField] private Sprite openSprite;
    private SpriteRenderer sprite;
    private GameObject lootUI;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorBoxCollider = GetComponent<BoxCollider2D>();
        doorAudioSource = GetComponent<AudioSource>();
        var player = GameObject.Find("Player");
        playerAccess = player.GetComponent<Interactable>();
        playerHealth = player.GetComponent<Health>();
        playerOxygen = player.GetComponent<Oxygen>();

        lootUI = GameObject.Find("Loot message");
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Interact()
    {
        sprite.sprite = openSprite;
        var canvasLootUI = lootUI.GetComponent<CanvasGroup>();
        var textLootMessage = lootUI.GetComponentInChildren<Text>();

        canvasLootUI.alpha = 1.0f;
        canvasLootUI.blocksRaycasts = true;

        if (loot != BoxLoot.Nothing)
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
    }

    private void SpawnLoot()
    {
        switch (loot)
        {
            case BoxLoot.MedKit:
                var prefab1 = Resources.Load<GameObject>("GameObjects/Prefabs/MedKit");
                var obj1 = Object.Instantiate(prefab1);
                obj1.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.OxygenKit:
                var prefab2 = Resources.Load<GameObject>("GameObjects/Prefabs/OxygenBalloon");
                var obj2 = Object.Instantiate(prefab2);
                obj2.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card1:
                var prefab3 = Resources.Load<GameObject>("GameObjects/Prefabs/Card1");
                var obj3 = Object.Instantiate(prefab3);
                obj3.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card2:
                var prefab4 = Resources.Load<GameObject>("GameObjects/Prefabs/Card2");
                var obj4 = Object.Instantiate(prefab4);
                obj4.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Card3:
                var prefab5 = Resources.Load<GameObject>("GameObjects/Prefabs/Card3");
                var obj5 = Object.Instantiate(prefab5);
                obj5.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
            case BoxLoot.Canister:
                var prefab6 = Resources.Load<GameObject>("GameObjects/Prefabs/Canister");
                var obj6 = Object.Instantiate(prefab6);
                obj6.GameObject().transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                break;
        }
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
        Nootebook = 8,

    }
}
