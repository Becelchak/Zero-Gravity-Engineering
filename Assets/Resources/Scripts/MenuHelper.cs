using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuHelper : MonoBehaviour
{
    private CanvasGroup menuCanvasGroup;
    private Image panelImage;
    private int pageNumber = 0;
    private TextMeshProUGUI titlePage;
    [SerializeField] private List<Sprite> pages;
    void Start()
    {
        menuCanvasGroup = GameObject.Find("MenuHelper").GetComponent<CanvasGroup>();
        panelImage = GameObject.Find("Guide").GetComponent<Image>();
        titlePage = GameObject.Find("TitlePage").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            menuCanvasGroup.alpha = 1;
            menuCanvasGroup.interactable = true;
            menuCanvasGroup.blocksRaycasts = true;
        }

        panelImage.sprite = pages[pageNumber];

        switch (pageNumber)
        {
            case 0:
                titlePage.text = "Ручное открытие шлюза отсека";
                break;
            case 1:
                titlePage.text = "Заправка бензинового генератора";
                break;
            case 2:
                titlePage.text = "Обход процедуры защиты складских помещений";
                break;
            case 3:
                titlePage.text = "Устранение проблем с электропитанием через предохранители";
                break;
        }
    }

    public void NextPage()
    {
        Debug.Log($"{pageNumber}");
        pageNumber++;
        if (pageNumber > pages.Count - 1)
            pageNumber = 0;
    }
}
