using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : Interactable_Object
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

    private float openTimer = 5f;

    [Header("Door access parameter")]
    [SerializeField] private bool isNeedFirstBayCard;
    [SerializeField] private bool isNeedSecondBayCard;
    [SerializeField] private bool isNeedThirdBayCard;
    private bool isPowerOn = true;
    [SerializeField] private bool isMainDoor = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorBoxCollider = GetComponent<BoxCollider2D>();
        doorAudioSource = GetComponent<AudioSource>();
        var player = GameObject.Find("Player");
        playerAccess = player.GetComponent<Interactable>();
        playerHealth = player.GetComponent<Health>();
        playerOxygen = player.GetComponent<Oxygen>();
    }

    void Update()
    {
        if (isOpenDoor && !isMainDoor)
        {
            openTimer -= Time.deltaTime;
            if(openTimer <= 0 )
            {
                DoorCLose();
                openTimer = 5f;
            }
        }
    }

    public void Interact()
    {
        if (isMainDoor) return;

        if (!isOpenDoor)
            DoorOpen();
        else if (isOpenDoor)
            DoorCLose();
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
        if (isNeedSecondBayCard)
            return playerAccess.isAccessSecondBay;
        return isNeedThirdBayCard && playerAccess.isAccessThirdBay;
    }

}
