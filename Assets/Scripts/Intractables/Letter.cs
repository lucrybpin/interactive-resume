using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour, IInteractable
{
    [SerializeField] PopupContent popupContent;

    public GameObject GameObject { get => this.gameObject; }

    public void Highlight()
    {
        //
    }

    public void Interact(CharacterInteraction interactor)
    {
        Debug.Log("Letter");
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
