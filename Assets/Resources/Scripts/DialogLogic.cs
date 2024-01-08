using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI nameCharacter;
    [SerializeField] 
    private TextMeshProUGUI dialogText;
    [SerializeField] 
    private CanvasGroup astroCanvasGroup;
    private Image astroImage;
    [SerializeField]
    private CanvasGroup marikaCanvasGroup;
    private Image marikaImage;

    public float textSpeed = 0.15f;
    private int indexText;
    private List<string> listText;
    private Character characterSpeach;
    private List<Emotional> characteresEmotional;


    private Moving playerMoving;
    private CanvasGroup dialogPanelCanvasGroup;
    private bool dialogGoing;

    private AudioSource dialogAudioSource;
    void Start()
    {
        dialogText.text = String.Empty;
        playerMoving = GameObject.Find("Player-body").GetComponent<Moving>();
        dialogPanelCanvasGroup = GetComponent<CanvasGroup>();
        dialogAudioSource = GetComponent<AudioSource>();

        marikaImage = marikaCanvasGroup.gameObject.GetComponent<Image>();
        astroImage = astroCanvasGroup.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (characteresEmotional == null) return;
        if (!dialogGoing)
        {
            playerMoving.UnFreeze();
        }

        if (Input.GetButtonDown("Jump"))
        {
            var otherText = listText[indexText].Split('~')[1];
            if (dialogText.text == otherText)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogText.text = otherText;
            }
        }

        if (characterSpeach == Character.Astro)
        {
            astroCanvasGroup.alpha = 1;
            astroCanvasGroup.blocksRaycasts = true;
            astroCanvasGroup.interactable = true;

            marikaCanvasGroup.alpha = 0;
            marikaCanvasGroup.blocksRaycasts = false;
            marikaCanvasGroup.interactable = false;

            nameCharacter.text = "Астро-9";

            if (characteresEmotional[indexText] != Emotional.None)
            {
                // ASTRO
                switch (characteresEmotional[indexText])
                {
                    case Emotional.Sad:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Sad");
                        break;
                    case Emotional.Surprise:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Surprise");
                        break;
                    case Emotional.Angry:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Angry");
                        break;
                    case Emotional.Neutral:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Neutrale");
                        break;
                    default:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Neutrale");
                        break;
                }
            }

        }
        else if (characterSpeach == Character.Marika || characterSpeach == Character.ChangeMarika)
        {
            marikaCanvasGroup.alpha = 1;
            marikaCanvasGroup.blocksRaycasts = true;
            marikaCanvasGroup.interactable = true;

            astroCanvasGroup.alpha = 0;
            astroCanvasGroup.blocksRaycasts = false;
            astroCanvasGroup.interactable = false;


            if(characterSpeach == Character.Marika)
                nameCharacter.text = "Оператор-М";
            else if(characterSpeach == Character.ChangeMarika)
            {
                nameCharacter.text = "Марика";
                characterSpeach = Character.ChangeMarika;
            }

            if (characteresEmotional[indexText] != Emotional.None)
            {
                //MARIKA
                switch (characteresEmotional[indexText])
                {
                    case Emotional.Sad:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Sad");
                        break;
                    case Emotional.Happy:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Happy");
                        break;
                    case Emotional.Neutral:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Neutral");
                        break;
                    case Emotional.Angry:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Angry");
                        break;
                    case Emotional.Scared:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Scared");
                        break;
                    case Emotional.Surprise:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Suprised");
                        break;
                    default:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Neutral");
                        break;
                }
            }
        }
    }

    public void StartDialog(List<string> text, Character character, List<Emotional> emotionalsList ,float speedText = 0.15f)
    {
        dialogPanelCanvasGroup.alpha = 1;
        dialogPanelCanvasGroup.blocksRaycasts = true;
        dialogPanelCanvasGroup.interactable = true;

        dialogGoing = true;
        indexText = 0;
        playerMoving.Freeze();
        listText = text;

        characteresEmotional = emotionalsList;

        if (character == Character.Astro)
        {
            astroCanvasGroup.alpha = 1;
            astroCanvasGroup.blocksRaycasts = true;
            astroCanvasGroup.interactable = true;

            marikaCanvasGroup.alpha = 0;
            marikaCanvasGroup.blocksRaycasts = false;
            marikaCanvasGroup.interactable = false;

            nameCharacter.text = "Астро-9";
            characterSpeach = Character.Astro;

            if (characteresEmotional[indexText] != Emotional.None)
            {
                // ASTRO
                switch (characteresEmotional[indexText])
                {
                    case Emotional.Sad:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Sad");
                        break;
                    case Emotional.Surprise:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Surprise");
                        break;
                    case Emotional.Angry:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Angry");
                        break;
                    case Emotional.Neutral:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Neutrale");
                        break;
                    default:
                        astroImage.sprite = Resources.Load<Sprite>("Characters/Astro character/Astro_Neutrale");
                        break;
                }
            }
        }
        else if (characterSpeach == Character.Marika || characterSpeach == Character.ChangeMarika)
        {
            marikaCanvasGroup.alpha = 1;
            marikaCanvasGroup.blocksRaycasts = true;
            marikaCanvasGroup.interactable = true;

            astroCanvasGroup.alpha = 0;
            astroCanvasGroup.blocksRaycasts = false;
            astroCanvasGroup.interactable = false;


            if (characterSpeach == Character.Marika)
                nameCharacter.text = "Оператор-М";
            else if (characterSpeach == Character.ChangeMarika)
            {
                nameCharacter.text = "Марика";
                characterSpeach = Character.ChangeMarika;
            }

            if (characteresEmotional[indexText] != Emotional.None)
            {
                //MARIKA
                switch (characteresEmotional[indexText])
                {
                    case Emotional.Sad:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Sad");
                        break;
                    case Emotional.Happy:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Happy");
                        break;
                    case Emotional.Neutral:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Neutral");
                        break;
                    case Emotional.Angry:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Angry");
                        break;
                    case Emotional.Scared:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Scared");
                        break;
                    case Emotional.Surprise:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Suprised");
                        break;
                    default:
                        marikaImage.sprite = Resources.Load<Sprite>("Characters/Marika character/Marika_Neutral");
                        break;
                }
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogAudioSource.Stop();
        dialogAudioSource.Play();

        dialogText.text = String.Empty;

        var characterChar = char.Parse(listText[indexText].Split('~')[0]);
        var otherText = listText[indexText].Split('~')[1];

        if (characterChar is 'М')
            characterSpeach = Character.Marika;
        else if (characterChar is 'А')
            characterSpeach = Character.Astro;
        else if(characterChar is 'С')
            characterSpeach = Character.ChangeMarika;

        foreach (var c in otherText)
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void NextLine()
    {
        if (indexText < listText.Count - 1)
        {
            indexText++;
            dialogText.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogGoing = false;

            astroCanvasGroup.alpha = 0;
            astroCanvasGroup.blocksRaycasts = false;
            astroCanvasGroup.interactable = false;

            marikaCanvasGroup.alpha = 0;
            marikaCanvasGroup.blocksRaycasts = false;
            marikaCanvasGroup.interactable = false;

            dialogPanelCanvasGroup.alpha = 0;
            dialogPanelCanvasGroup.blocksRaycasts = false;
            dialogPanelCanvasGroup.interactable = false;

            dialogText.text = String.Empty;
        }
    }

    public enum Character
    {
        Astro = 0,
        Marika = 1,
        ChangeMarika = 2,
    }

    public enum Emotional
    {
        None = 0,
        Sad = 1,
        Angry = 2,
        Scared = 3,
        Surprise = 4,
        Happy = 5,
        Neutral = 6,
    }
}
