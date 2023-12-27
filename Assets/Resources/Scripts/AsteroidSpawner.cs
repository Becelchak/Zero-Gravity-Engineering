using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> asteroides;
    [SerializeField] private bool playerInZone;
    private Random rnd = new System.Random();
    [SerializeField] private float timer = 15f;
    void Start()
    {
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <=0 && playerInZone)
        {
            var numberNExt = rnd.Next(0, 3);

            var asteroid = Instantiate(asteroides[numberNExt]);
            var modifier = rnd.Next(-3, 3);
            asteroid.transform.position = new Vector3(transform.position.x, transform.position.y + modifier);

            timer = 15f;
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
            playerInZone = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
            playerInZone = false;
    }
}
