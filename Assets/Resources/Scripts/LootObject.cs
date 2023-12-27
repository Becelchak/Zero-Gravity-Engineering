using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootObject : Interactable_Object
{
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private AudioSource doorAudioSource;
    private bool isOpenDoor = false;
    private Animator animator;
    private BoxCollider2D doorBoxCollider;

    private Interactable playerAccess;
    private Health playerHealth;
    private Oxygen playerOxygen;
    private float playerFuelValue;

    [SerializeField] private BoxLoot loot;
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

                var canvasLootUI1 = lootUI.GetComponent<CanvasGroup>();
                var textLootMessage1 = lootUI.GetComponentInChildren<Text>();
                canvasLootUI1.alpha = 1.0f;
                canvasLootUI1.blocksRaycasts = true;
                textLootMessage1.text = $"Вы нашли {loot}!";

                doorAudioSource.PlayOneShot(openSound);

                Destroy(gameObject, 0.6f);
                break;
            case BoxLoot.FlashDrive or BoxLoot.Nootebook:
                playerAccess.AddQuestItem(name);
                Destroy(gameObject);
                break;
            case BoxLoot.Fuse:
                playerAccess.AddFuse();
                Destroy(gameObject);
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
        Fuse = 9,

    }
}
