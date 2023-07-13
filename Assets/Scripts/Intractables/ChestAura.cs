using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class ChestAura : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Sequence sequence;
    Color initialColor;

    [Button("Start Aura")]
    public void StartAura()
    {
        sequence = DOTween.Sequence();
        initialColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a);
        Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        sequence.Append(
            spriteRenderer.DOColor(newColor, 1f)
        ).SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }

    [Button("Stop Aura")]
    public void StopAura()
    {
        spriteRenderer.color = initialColor;
        sequence.Kill();
    }

}
