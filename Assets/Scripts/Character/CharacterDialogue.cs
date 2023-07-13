using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;

public class CharacterDialogue : MonoBehaviour
{
    [SerializeField] TMP_Text displayText;

    public async UniTask DisplayText(string text)
    {
        displayText.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.052));
            displayText.text += text[i];
            await UniTask.Yield();
        }
    }
}
