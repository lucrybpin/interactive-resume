using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingUtils : MonoBehaviour
{
    [SerializeField] PostProcessVolume postProcessVolume;

    private static PostProcessingUtils instance;

    public static PostProcessingUtils Instance { get { return instance; } }


    private void Awake()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
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

    public void TurnOnBlurInstant(float time = 2f)
    {
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out DepthOfField dof);

        if (time == 0f) dof.focusDistance.value = 2f;
        else _ = LerpBlur(dof.focusDistance.value, 2f, time);
    }

    public void TurnOffBlurInstant(float time = 2f)
    {
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out DepthOfField dof);

        if (time == 0f) dof.focusDistance.value = 2f;
        else _ = LerpBlur(dof.focusDistance.value, 12f, time);
    }

    public async UniTask TurnOnBlur(float time = 2f)
    {
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out DepthOfField dof);

        if (time == 0f)     dof.focusDistance.value = 2f;
        else await          LerpBlur(dof.focusDistance.value, 2f, time);
    }

    public async UniTask TurnOffBlur(float time = 2f)
    {
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out DepthOfField dof);

        if (time == 0f)     dof.focusDistance.value = 2f;
        else await          LerpBlur(dof.focusDistance.value, 12f, time);
    }

    private async UniTask LerpBlur(float initialValue, float finalValue, float time)
    {
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out DepthOfField dof);

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            await UniTask.Yield();
            elapsedTime += Time.deltaTime;
            dof.focusDistance.value = Mathf.Lerp(initialValue, finalValue, elapsedTime / time);
        }
    }
}
