using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RuneToken : MonoBehaviour, IInteractable
{
    [SerializeField] PopupContent popupContent;
    [SerializeField] SpriteRenderer runeScript;
    [SerializeField] AudioSource audioSource;
    private Sequence sequence;

    public GameObject GameObject { get => this.gameObject; }

    public void Interact(CharacterInteraction interactor)
    {
        StartSound();
        Popup.Instance.ShowPopup();
        _ = Popup.Instance.ShowPopupAsync(popupContent);
        Popup.Instance.AddOnCloseCallback(StopSound);
    }

    public void OnEnterRange(CharacterControls character)
    {
        character.CharacterInteraction.SetInteractable(this);
        GlowWords();
    }

    public void OnExitRange(CharacterControls character)
    {
        character.CharacterInteraction.ClearInteractable();
        HideWords();
        Popup.Instance.ClearOnCloseCallback();
    }

    public void GlowWords()
    {
        sequence = DOTween.Sequence();
        Color newColor = new Color(runeScript.color.r, runeScript.color.g, runeScript.color.b, 1f);
        sequence.Append(
            runeScript.DOColor(newColor, 1f)
        );
        sequence.Play();
    }

    public void HideWords()
    {
        sequence = DOTween.Sequence();
        Color newColor = new Color(runeScript.color.r, runeScript.color.g, runeScript.color.b, 0f);
        sequence.Append(
            runeScript.DOColor(newColor, 1f)
        );
        sequence.Play();
    }

    public void StartSound()
    {
        audioSource.Play();
        _ = LerpVolume(1f, 1f);
    }

    public void StopSound()
    {
        _ = LerpVolume(0f, 1f);
    }

    private async UniTask LerpVolume(float desiredVolume, float time)
    {
        float initialVolume = audioSource.volume;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, desiredVolume, elapsedTime / time);
            await UniTask.Yield();
        }
    }

}
