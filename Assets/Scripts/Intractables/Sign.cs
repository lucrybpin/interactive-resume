using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable
{
    CharacterControls interactorControls;
    [SerializeField]
    [TextArea]
    string signMessage;

    public GameObject GameObject { get => this.gameObject; }

    private void Start()
    {
        Animate();
    }

    public void Interact(CharacterInteraction interactor)
    {
        _ = InteractAsync(interactor);
    }

    public async UniTask InteractAsync(CharacterInteraction interactor)
    {
        interactorControls = interactor.GetComponent<CharacterControls>();
        interactorControls.DisableMovement();
        await interactorControls.CharacterDialogue.DisplayText(signMessage);
        interactorControls.EnableMovement();
    }

    public void OnEnterRange(CharacterControls character)
    {
        character.CharacterInteraction.SetInteractable(this);
    }

    public void OnExitRange(CharacterControls character)
    {
        character.CharacterInteraction.ClearInteractable();
        if (interactorControls != null)
            _ = interactorControls.CharacterDialogue.DisplayText("");
    }

    public void Animate()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 7), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutCubic);
    }
}
