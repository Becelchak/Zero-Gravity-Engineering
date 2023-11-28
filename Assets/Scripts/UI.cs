using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {

    }

    public void StartMainGame()
    {
        SceneManager.LoadScene("Main Game");
    }

}
