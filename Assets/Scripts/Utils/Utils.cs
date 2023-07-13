using Cysharp.Threading.Tasks;
using System;

public static class Utils
{
    public static async UniTask WaitForSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        await UniTask.Yield();
    }
}
