using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupContent : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] string title;
    [TextArea]
    [SerializeField] string content;
    public Sprite Sprite { get => sprite; }
    public string Title { get => title; }
    public string Content { get => content; }
    
}
