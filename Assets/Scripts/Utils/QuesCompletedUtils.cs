using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;
using UnityEngine.Rendering.PostProcessing;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class QuesCompletedUtils : MonoBehaviour
{
    [SerializeField] TMP_Text questName;
    CanvasGroup canvasGroup;

    private static QuesCompletedUtils instance;

    public static QuesCompletedUtils Instance { get { return instance; } }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }


    public async UniTask DisplayQuestCompleted(string finishedQuest)
    {
        float time = 1f;
        float elapsedTime = 0f;

        questName.text = finishedQuest;

        while (elapsedTime < time)
        {
            await UniTask.Yield();
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / time);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        elapsedTime = 0f;

        while (elapsedTime < time)
        {
            await UniTask.Yield();
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / time);
        }

    }
}
