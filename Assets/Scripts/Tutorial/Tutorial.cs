using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Quest First Steps")]
    [SerializeField] CharacterControls characterControls;
    [SerializeField] ArrowKeysUI arrowKeysUI;
    [Header("Quest Interacting")]
    [SerializeField] GameObject chest;
    [SerializeField] ChestAura chestAura;

    [SerializeField] PopupContent popupContent;
    bool popupClosed = false;

    void Start()
    {
        _ = RunTutorial();
    }

    private async UniTask RunTutorial()
    {
        DisableOutlines();
        await FirstSteps();
        await Interacting();
        EnableOutlines();
    }

    private async UniTask FirstSteps()
    {
        characterControls.DisableMovement();
        await PostProcessingUtils.Instance.TurnOnBlur(0f);
        await PostProcessingUtils.Instance.TurnOffBlur(2f);
        await characterControls.CharacterDialogue.DisplayText("Hi, my name is Lucas.");
        await Utils.WaitForSeconds(1f);
        await characterControls.CharacterDialogue.DisplayText("welcome to my Resume");
        await Utils.WaitForSeconds(1f);
        await characterControls.CharacterDialogue.DisplayText("you can move using WASD keys");
        await Utils.WaitForSeconds(1f);
        await characterControls.CharacterDialogue.DisplayText("");
        characterControls.EnableMovement();
        arrowKeysUI.gameObject.SetActive(true);
        await arrowKeysUI.FinishBasicMoving();
        await FinishQuest("First Steps");
    }

    private static void DisableOutlines()
    {
        InteractableOutline[] interactablesOutlines = FindObjectsOfType<InteractableOutline>();
        foreach (InteractableOutline interactablesOutline in interactablesOutlines)
            interactablesOutline.StopAura();
    }

    private static void EnableOutlines()
    {
        InteractableOutline[] interactablesOutlines = FindObjectsOfType<InteractableOutline>();
        foreach (InteractableOutline interactablesOutline in interactablesOutlines)
            interactablesOutline.StartAura();
    }

    private async UniTask Interacting()
    {
        characterControls.DisableMovement();
        await characterControls.CharacterDialogue.DisplayText("It's time to learn to interact with the world!");
        await Utils.WaitForSeconds(1f);
        await characterControls.CharacterDialogue.DisplayText("I should get closer to an interactable");
        await Utils.WaitForSeconds(1f);
        await characterControls.CharacterDialogue.DisplayText("");
        characterControls.EnableMovement();
        await waitForPlayerInteractWithChest();
        await Utils.WaitForSeconds(1f);
        await FinishQuest("Interacting");
    }

    private async UniTask waitForPlayerInteractWithChest()
    {
        chestAura.StartAura();
        while (characterControls.CharacterInteraction.InteractableGameObject != chest)
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

        characterControls.DisableMovement();
        await characterControls.CharacterDialogue.DisplayText("Press E key to interact");
        characterControls.CharacterInteraction.OnInteract += HaveInteracted;
        interacted = false;
        Popup.Instance.AddOnCloseCallback(ClosePopup);


        while (!interacted)
            await UniTask.Yield();

        while (!popupClosed)
            await UniTask.Yield();
        
        

        await characterControls.CharacterDialogue.DisplayText("");
        characterControls.EnableMovement();
        chestAura.StopAura();
    }

    

    public void ClosePopup()
    {
        popupClosed = true;
    }



    bool interacted = false;
    private void HaveInteracted(CharacterInteraction interactor)
    {
        interacted = true;
    }

    private async UniTask FinishQuest(string questName)
    {
        characterControls.DisableMovement();
        await PostProcessingUtils.Instance.TurnOnBlur(0.52f);
        await QuesCompletedUtils.Instance.DisplayQuestCompleted(questName);
        await PostProcessingUtils.Instance.TurnOffBlur(0.52f);
        characterControls.EnableMovement();
    }

}
