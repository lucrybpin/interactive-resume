using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    CharacterMovement characterMovement;
    CharacterDialogue characterDialogue;
    CharacterInteraction characterInteraction;

    public CharacterMovement CharacterMovement { get => characterMovement; }
    public CharacterDialogue CharacterDialogue { get => characterDialogue; }
    public CharacterInteraction CharacterInteraction { get => characterInteraction; }

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterDialogue = GetComponent<CharacterDialogue>();
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    public void DisableMovement()
    {
        characterMovement.ResetInput();
        characterMovement.enabled = false;
    }

    public void EnableMovement()
    {
        characterMovement.enabled = true;
    }
}
