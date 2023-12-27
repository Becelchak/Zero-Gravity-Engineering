using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Generator : Interactable_Object
{
    private GameObject lootUI;
    [SerializeField] private List<Button> doorButtonsPowerOn;
    private AudioSource doorAudioSource;
    [SerializeField] private List<Button> doorButtonsPowerOff;
    [SerializeField] private List<Door> doorImportant;
    private Image fuel;
    private Image powerStatus;
    private bool isActive = true;
    private float fuelReserve = 1f;
    private Toggle toggle;

    private float playerFuelValue;
    private Interactable playerAccess;

    void Start()
    {
        var player = GameObject.Find("Player");
        playerAccess = player.GetComponent<Interactable>();

        lootUI = GameObject.Find("Loot message");
        doorAudioSource = GetComponent<AudioSource>();
        fuel = GameObject.Find("Fuel reserve").GetComponent<Image>();
        fuelReserve = fuel.fillAmount;
        foreach (var button in doorButtonsPowerOn)
        {
            button.interactable = false;
        }

        foreach (var collider in doorImportant)
        {
            if (!collider.name.Contains("Big"))
                collider.gameObject.GetComponents<BoxCollider2D>()[1].enabled = false;
            collider.enabled = false;
        }
    }
    void Update()
    {
        if (toggle != null)
        {
            if (!toggle.isOn)
                doorAudioSource.Stop();
            Fill();
        }
        if (isActive)
        {
            fuel.fillAmount -= 0.000001f;
            foreach (var button in doorButtonsPowerOn)
            {
                button.interactable = true;
            }
            foreach (var collider in doorImportant)
            {
                collider.enabled = true;
                if(!collider.name.Contains("Big"))
                    collider.gameObject.GetComponents<BoxCollider2D>()[1].enabled = true;
            }
        }

        if (fuel.fillAmount <= 0f)
        {
            GeneratorPowerOff();
            foreach (var button in doorButtonsPowerOn)
            {
                button.interactable = false;
            }
            foreach (var button in doorButtonsPowerOff)
            {
                button.onClick.Invoke();
            }
        }
    }

    public void Interact()
    {
        var panelGen = GameObject.Find("Generator panel").GetComponent<CanvasGroup>();
        panelGen.alpha = 1.0f;
        panelGen.interactable = true;
        panelGen.blocksRaycasts = true;

        powerStatus = panelGen.transform.GetChild(3).GetComponent<Image>();
        playerFuelValue = playerAccess.GetCanisterValue();
    }

    public void EndInteract()
    {
        var panelGen = GameObject.Find("Generator panel").GetComponent<CanvasGroup>();
        panelGen.alpha = 0.0f;
        panelGen.interactable = false;
        panelGen.blocksRaycasts = false;
    }

    public void FillFuel(Toggle toggle)
    {
        this.toggle = toggle;
        doorAudioSource.Play();
    }

    private void Fill()
    {
        if (fuelReserve <= 1f && toggle.isOn && playerFuelValue > 0)
        {
            fuelReserve += 0.0005f;
            playerFuelValue -= 0.0005f;
            fuel.fillAmount = fuelReserve;
        }
        else if (playerFuelValue <= 0 && toggle.isOn)
        {
            var canvasLootUI = lootUI.GetComponent<CanvasGroup>();
            var textLootMessage = lootUI.GetComponentInChildren<Text>();

            canvasLootUI.alpha = 1.0f;
            canvasLootUI.blocksRaycasts = true;
            textLootMessage.text = $"Недостаточно топлива!";
            doorAudioSource.Stop();
            playerAccess.RemoveCanister();
            toggle.isOn = false;
        }

    }
    public void GeneratorPowerOn()
    {
        isActive = true;
    }

    public void GeneratorPowerOff()
    {
        isActive = false;
    }
}
