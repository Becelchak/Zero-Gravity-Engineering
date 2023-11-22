using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Animator healthBarAnimator;
    void Start()
    {
        
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
            case < 30:
                healthBarAnimator.SetTrigger("Bad");
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Take damage! Now health = {health}");
    }

    public int GetHealth()
    {
        return health;
    }
}