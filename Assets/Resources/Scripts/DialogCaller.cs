using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCaller : MonoBehaviour
{
    [SerializeField] private List<string> dialogText;
    [SerializeField] private List<DialogLogic.Emotional> emotions;
    [SerializeField] private DialogLogic.Character character;
    [SerializeField] private float textSpeed;
    [SerializeField] private Special specialParameter;
    private DialogLogic logic;

    private Generator generator;
    private Interactable player;

    void Start()
    {
        logic = GameObject.Find("Dialog").GetComponent<DialogLogic>();
        if (specialParameter != null)
        {
            switch (specialParameter)
            {
                case Special.GeneratorFull:
                    generator = GameObject.Find("Generator").GetComponent<Generator>();
                    break;
                default:
                    player = GameObject.Find("Player-body").GetComponent<Interactable>();
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && CheckSpecialParameter())
        {
            logic.StartDialog(dialogText, character, emotions);
            Destroy(gameObject);
        }
    }

    bool CheckSpecialParameter()
    {
        if (generator != null) return generator.PowerStatus();

        switch (specialParameter)
        {
            case Special.FirstItemGet:
                return player.GetFirstItemStatus();
            case Special.SecondItemGet:
                return player.GetSecondItemStatus();
            case Special.ThirdItemGet:
                return player.GetThirdItemStatus();
        }

        return true;
    }


    public enum Special
    {
        None = 0,
        GeneratorFull = 1,
        FirstItemGet = 2,
        SecondItemGet = 3,
        ThirdItemGet = 4,
    }

}
