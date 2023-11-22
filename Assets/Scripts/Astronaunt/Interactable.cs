using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject InteractImage;
    private CanvasGroup imageCanvasGroup;
    private Interactable_Object interactable;
    void Start()
    {
        imageCanvasGroup = InteractImage.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && interactable != null)
        {
            Debug.Log("OPEN");
            interactable.Interact();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 1.0f;
        imageCanvasGroup.blocksRaycasts = true;
        imageCanvasGroup.interactable = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 1.0f;
        imageCanvasGroup.blocksRaycasts = true;
        imageCanvasGroup.interactable = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interactable = other.GetComponent<Interactable_Object>();
        if (interactable == null) return;
        imageCanvasGroup.alpha = 0.0f;
        imageCanvasGroup.blocksRaycasts = false;
        imageCanvasGroup.interactable = false;
    }
}
