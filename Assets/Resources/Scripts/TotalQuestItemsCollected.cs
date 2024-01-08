using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalQuestItemsCollected : MonoBehaviour
{
    private Interactable player;
    private Text selfText;
    void Start()
    {
        player = GameObject.Find("Player-body").GetComponent<Interactable>();
        selfText = GetComponent<Text>();
    }

    void Update()
    {
        selfText.text = $"{player.GetCountQuestItems()}/3";
    }
}
