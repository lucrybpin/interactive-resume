using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTransparency : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    Sequence sequence;
    Color initialColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            initialColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a);
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .43f);
            sprite.DOColor(newColor, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            initialColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a);
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            sprite.DOColor(newColor, 1f);
        }
    }
}
