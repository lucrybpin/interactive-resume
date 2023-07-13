using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterControlsEvent : UnityEvent<CharacterControls>
{
}

public class FlexibleTrigger : MonoBehaviour
{
    public CharacterControlsEvent OnTriggerEnter;
    public CharacterControlsEvent OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterControls character))
        {
            OnTriggerEnter.Invoke(character);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterControls character))
        {
            OnTriggerExit.Invoke(character);
        }
    }
}
