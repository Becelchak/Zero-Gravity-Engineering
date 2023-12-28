using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerBody;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        body.velocity = playerBody.velocity;
    }
}
