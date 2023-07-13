using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] IInteractable interactable;
    [SerializeField] GameObject interactableGameObject;
    [SerializeField] GameObject displayInteractKey;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip interactSound;

    public delegate void InteractDelegate(CharacterInteraction interactor);
    public InteractDelegate OnInteract;

    public GameObject InteractableGameObject { get => interactableGameObject; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        OnInteract += PlayInteractSound;
    }

    public void SetInteractable(IInteractable newInteractable)
    {
        interactable = newInteractable;
        interactableGameObject = newInteractable.GameObject;
        //displayInteractKey.SetActive(true);
        displayInteractKey.gameObject.transform.DOScale(1f, 0.25f);
        OnInteract += newInteractable.Interact;
    }

    public void ClearInteractable()
    {
        OnInteract -= interactable.Interact;
        interactable = null;
        interactableGameObject = null;
        displayInteractKey.gameObject.transform.DOScale(0f, 0.25f);
    }


    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (interactable == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            displayInteractKey.gameObject.transform.DOShakeScale(.25f, 0.2f).OnComplete(() =>
            {
                displayInteractKey.gameObject.transform.DOScale(0f, 0.25f);
                if (OnInteract != null)
                    OnInteract(this);
            });
        }
    }

    private void PlayInteractSound(CharacterInteraction interactor)
    {
        audioSource.clip = interactSound;
        audioSource.Play();
    }
}
