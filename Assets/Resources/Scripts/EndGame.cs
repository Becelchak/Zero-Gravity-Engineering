using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup endMenu;
    private Moving playerMove;
    private Oxygen playerOxygen;
    private bool isEndGame = false;
    void Start()
    {
        playerMove = player.GetComponent<Moving>();
        playerOxygen = player.GetComponent<Oxygen>();
    }

    void Update()
    {
        if (isEndGame)
        {
            playerOxygen.EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            endMenu.alpha = 1;
            endMenu.blocksRaycasts = true;
            endMenu.interactable = true;
            isEndGame = true;
            playerMove.Freeze();
        }

    }
}
