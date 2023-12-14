using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private bool isExplode;
    [SerializeField] private bool canExplode;
    private Rigidbody2D body;
    private Health playerHealth;

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * speed, ForceMode2D.Impulse);

        if (name.Contains("large"))
        {
            var rnd = new System.Random();
            if(rnd.Next(0,2) == 1)
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
        if(isExplode)
        {
            Debug.Log($"{name} exploded!");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Object") && canExplode)
            isExplode = true;
        else if(collision.transform.CompareTag("Player"))
        {
            playerHealth.TakeDamage(CalculateDamage());
            Destroy(gameObject);
        }

            
    }

    private int CalculateDamage()
    {
        var damage = body.mass * body.velocity.magnitude / 3;
        Debug.Log($"{damage}");
        if( damage < 3)
            damage = 0;
        return (int)damage;
    }
}
