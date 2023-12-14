using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Interactable_Object : MonoBehaviour
{
    [Header("Common parameter")]
    [SerializeField] private ObjectType type;


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Interact()
    {
        switch (type)
        {
            case ObjectType.Door:
                GetComponent<Door>().Interact();
                break;
            case ObjectType.DoorPanel:
                GetComponent<DoorPanel>().Interact();
                break;
            case ObjectType.Box:
                GetComponent<Box>().Interact();
                break;
            case ObjectType.Generator:
                GetComponent<Generator>().Interact();
                break;
            case ObjectType.Other:
                GetComponent<LootObject>().Interact();
                break;
            default:
                break;
        }
    }

    private enum ObjectType
    {
        Door = 0,
        DoorConsole = 1,
        DoorPanel = 2,
        MainDoor = 3,
        Box = 4,
        Generator = 5,
        Other = 6,
    }

    private enum BoxLoot
    {
        Nothing = 0,
        Card1 = 1, 
        Card2 = 2, 
        Card3 = 3,
        MedKit = 4,
        OxygenKit = 5,
        FlashDrive = 6,
        Canister = 7,
        Nootebook = 8,

    }
}
