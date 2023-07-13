using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ArrowKeysUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer upArrow;
    [SerializeField] SpriteRenderer downArrow;
    [SerializeField] SpriteRenderer leftArrow;
    [SerializeField] SpriteRenderer rightArrow;

    CharacterMovement characterMovement;
    [SerializeField] float pressedUpTime = 0;
    [SerializeField] float pressedDownTime = 0;
    [SerializeField] float pressedLeftTime = 0;
    [SerializeField] float pressedRightTime = 0;

    [SerializeField] float pressTimeToFinish = 0.34f;

    delegate void OnMoveFinished();

    OnMoveFinished OnMoveUpFinished;
    OnMoveFinished OnMoveDownFinished;
    OnMoveFinished OnMoveLeftFinished;
    OnMoveFinished OnMoveRightFinished;

    int finishedDirections = 0;

    private void Start()
    {
        characterMovement = transform.parent.GetComponent<CharacterMovement>();
        OnMoveUpFinished = DismissUpArrowSprite;
        OnMoveDownFinished = DismissDownArrowSprite;
        OnMoveLeftFinished = DismissLeftArrowSprite;
        OnMoveRightFinished = DismissRighArrowSprite;
    }

    private void Update()
    {
        DisplayArrowSprites();
    }

    private void DisplayArrowSprites()
    {
        Vector2 direction = characterMovement.Direction;

        upArrow.enabled = true;
        downArrow.enabled = true;
        leftArrow.enabled = true;
        rightArrow.enabled = true;

        if (direction.y > 0 && pressedUpTime < pressTimeToFinish)
        {
            pressedUpTime += Time.deltaTime;
            float upNewAlpha = Mathf.Lerp(0.2f, 1, pressedUpTime / pressTimeToFinish);
            upArrow.color = new Color(upArrow.color.r, upArrow.color.g, upArrow.color.b, upNewAlpha);
            if (pressedUpTime >= pressTimeToFinish)
                OnMoveUpFinished();
        }
        if (direction.y < 0 && pressedDownTime < pressTimeToFinish)
        {
            pressedDownTime += Time.deltaTime;
            float downNewAlpha = Mathf.Lerp(0.2f, 1, pressedDownTime / pressTimeToFinish);
            downArrow.color = new Color(downArrow.color.r, downArrow.color.g, downArrow.color.b, downNewAlpha);
            if (pressedDownTime >= pressTimeToFinish)
                OnMoveDownFinished();
        }
        if (direction.x < 0 && pressedLeftTime < pressTimeToFinish)
        {
            pressedLeftTime += Time.deltaTime;
            float leftNewAlpha = Mathf.Lerp(0.2f, 1, pressedLeftTime / pressTimeToFinish);
            leftArrow.color = new Color(leftArrow.color.r, leftArrow.color.g, leftArrow.color.b, leftNewAlpha);
            if (pressedLeftTime >= pressTimeToFinish)
                OnMoveLeftFinished();
        }
        if (direction.x > 0 && pressedRightTime < pressTimeToFinish)
        {
            pressedRightTime += Time.deltaTime;
            float rightNewAlpha = Mathf.Lerp(0.2f, 1, pressedLeftTime / pressTimeToFinish);
            rightArrow.color = new Color(rightArrow.color.r, rightArrow.color.g, rightArrow.color.b, rightNewAlpha);
            if (pressedRightTime >= pressTimeToFinish)
                OnMoveRightFinished();
        }
    }

    private void DismissUpArrowSprite()
    {
        upArrow.transform.DOScale(0f, 1f).SetEase(Ease.InBack);
        finishedDirections++;
    }
    private void DismissDownArrowSprite()
    {
        downArrow.transform.DOScale(0f, 1f).SetEase(Ease.InBack);
        finishedDirections++;
    }

    private void DismissLeftArrowSprite()
    {
        leftArrow.transform.DOScale(0f, 1f).SetEase(Ease.InBack);
        finishedDirections++;
    }

    private void DismissRighArrowSprite()
    {
        rightArrow.transform.DOScale(0f, 1f).SetEase(Ease.InBack);
        finishedDirections++;
    }

    public async UniTask FinishBasicMoving()
    {
        while (finishedDirections < 4)
        {
            await UniTask.Yield();
        }
    }
}
