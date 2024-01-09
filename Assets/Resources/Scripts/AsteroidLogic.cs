using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidLogic : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private bool isExplode;
    [SerializeField] private bool canExplode;
    [SerializeField] private AudioClip audioClipStuck;
    private Rigidbody2D body;
    private Health playerHealth;
    private Animator asteroidAnim;
    private int counterChildren;
    public float timer = 0;
    public bool isChild;

    void Start()
    {
        playerHealth = GameObject.Find("Player-body").GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        asteroidAnim = GetComponent<Animator>();
        body.AddForce(transform.right * speed, ForceMode2D.Impulse);

        if (name.Contains("large"))
        {
            var rnd = new System.Random();
            if(Random.Range(0,2) == 1)
                canExplode = true;
        }
        else if (name.Contains("medium"))
        {
            var rnd = new System.Random();
            if (rnd.Next(0, 4) == 2)
                canExplode = true;
        }
    }
    void Update()
    {
        if (timer > -1)
        {
            Explode();
        }
        if(isExplode)
        {
            if (name.Contains("huge"))
            {
                if (counterChildren < 10)
                    SpawnAsteroides();
            }

            //Debug.Log($"{name} exploded!");
            Destroy(gameObject, 0.5f);
        }
    }

    void SpawnAsteroides()
    {
        var medium = Resources.Load("GameObjects/Prefabs/Dangerous/Asteroid medium") as GameObject;

        var newAsteroid = Instantiate(medium);
        newAsteroid.transform.position = transform.position;
        var angle = Random.rotation;
        newAsteroid.transform.rotation = angle;

        newAsteroid.GetComponent<AsteroidLogic>().timer = 30;
        newAsteroid.GetComponent<AsteroidLogic>().isChild = true;

        counterChildren++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var audio = GetComponent<AudioSource>();
        if ((collision.transform.CompareTag("Object") || collision.transform.CompareTag("ObjectTileMap")) && canExplode)
        {
            isExplode = true;
            asteroidAnim.SetTrigger("Explode");
            audio.Play();
        }
        else if (collision.transform.CompareTag("Object") && !canExplode)
        {
            body.AddForce(-transform.right * speed/3, ForceMode2D.Impulse);
            audio.clip = audioClipStuck;
            audio.Play();
        }
        
        
        if(collision.transform.CompareTag("Player"))
        {
            playerHealth.TakeDamage(CalculateDamage());
            Destroy(gameObject);
        }

            
    }

    private int CalculateDamage()
    {
        var damage = body.mass * body.velocity.magnitude / 2;
        Debug.Log($"{damage}");
        if( damage < 5)
            damage = 0;
        return (int)damage;
    }

    public void Explode()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && isChild)
            isExplode = true;
    }
}
