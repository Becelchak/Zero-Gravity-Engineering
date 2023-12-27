using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLogic : MonoBehaviour
{
    [SerializeField] private int damage;
    private float timer = 1;
    private bool canDamage = true;
    [SerializeField] private AudioClip spark;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(spark);
        }
        if(!canDamage)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                canDamage = true;
                timer = 1;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && canDamage)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            canDamage = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && canDamage)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            canDamage = false;
        }
    }
}
