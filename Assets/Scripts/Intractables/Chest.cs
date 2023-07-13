using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] PopupContent popupContent;

    public GameObject GameObject { get => this.gameObject; }

    private void Start()
    {
        popupContent = GetComponent<PopupContent>();
    }

    public void Interact(CharacterInteraction interactor)
    {
        Popup.Instance.ShowPopup();
        _ = Popup.Instance.ShowPopupAsync(popupContent);
    }

    public void OnEnterRange(CharacterControls character)
    {
        character.CharacterInteraction.SetInteractable(this);
    }

    public void OnExitRange(CharacterControls character)
    {
        character.CharacterInteraction.ClearInteractable();
    }
}
