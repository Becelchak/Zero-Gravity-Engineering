using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public void StartMainGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
