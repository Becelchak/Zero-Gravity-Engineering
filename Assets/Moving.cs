using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0,0,1), Speed * 20f * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0, 0, 1), -Speed  * 20f * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(-Vector3.down * Speed * Time.deltaTime);
        }
    }
}
