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
            case ObjectType.DoorConsole:
                GetComponent<DoorConsole>().Interact();
                break;
            case ObjectType.ConsoleElectric:
                GetComponent<ConsoleElectric>().Interact();
                break;
            default:
                break;
        }
    }

    public void EndInteract()
    {
        switch (type)
        {
            case ObjectType.DoorPanel:
                GetComponent<DoorPanel>().EndInteract();
                break;
            case ObjectType.Door:
                GetComponent<Door>().EndInteract();
                break;
            case ObjectType.Generator:
                //GetComponent<Generator>().EndInteract();
                break;
            case ObjectType.DoorConsole:
                GetComponent<DoorConsole>().EndInteract();
                break;
            case ObjectType.ConsoleElectric:
                GetComponent<ConsoleElectric>().EndInteract();
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
        ConsoleElectric = 7,
    }

}
