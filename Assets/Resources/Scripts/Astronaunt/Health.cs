using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Animator healthBarAnimator;
    [SerializeField] private AudioClip healing;
    [SerializeField] private AudioClip dead;
    [SerializeField] private List<AudioClip> hurtPlaylist;
    private AudioSource playerAudioSource;
    private CanvasGroup deathPanel;
    private bool isDead;

    private Oxygen playerOxygen;
    void Start()
    {
        deathPanel = GameObject.Find("DeathPanel").GetComponent<CanvasGroup>();
        playerAudioSource = GetComponent<AudioSource>();
        playerOxygen = GetComponent<Oxygen>();
    }
    void Update()
    {
        
        switch (health)
        {
            case > 70:
                healthBarAnimator.SetTrigger("Normal");
                break;
            case < 70 and > 30:
                healthBarAnimator.SetTrigger("Middle");
                break;
            case < 30 and > 0:
                healthBarAnimator.SetTrigger("Bad");
                break;
            case <= 0:
                healthBarAnimator.SetTrigger("Death");
                break;
        }

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            GetComponent<Moving>().Freeze();
            GetComponent<Oxygen>().EndGame();
            playerAudioSource.PlayOneShot(dead);

            deathPanel.alpha = 1;
            deathPanel.blocksRaycasts = true;
            deathPanel.interactable = true;
        }

        if(!isDead) return;
        if(!playerAudioSource.isPlaying)
            playerAudioSource.mute = true;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 1 || isDead) return;

        var breath = Resources.Load("Sound/Sounds/Astronaunt/Breathing") as AudioClip;
        if(!playerOxygen.isGasping())
        {
            playerAudioSource.Stop();
            var rnd = new System.Random();
            var number = rnd.Next(0, 2);
            playerAudioSource.PlayOneShot(hurtPlaylist[number]);
        }

        health -= damage;
        Debug.Log($"Take damage! Now health = {health}");
    }

    public int GetHealth()
    {
        return health;
    }

    public bool GetLiveStatus()
    {
        return isDead;
    }

    public void AddHealth(int health)
    {
        this.health += health;
        if (this.health > 100)
            this.health = 100;
        playerAudioSource.PlayOneShot(healing);
    }
}
