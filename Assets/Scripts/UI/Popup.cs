using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Popup : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Ease easeType;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioClip buttonClickSound;
    //[SerializeField] TextMeshProUGUI content;
    [SerializeField] int currentPage = 1;
    [SerializeField] Button nextPageButton;
    [SerializeField] Button previousPageButton;
    [SerializeField] Image image;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text contentText;

    public delegate void OnCloseFunction();
    public OnCloseFunction OnClose;

    private static Popup instance;

    public static Popup Instance
    {
        get
        {
            Debug.Log(instance);
            return instance;
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

    [Button("Show Popup")]
    public void ShowPopup()
    {
        _ = ShowPopupAsync();
    }

    public void ShowPopup(PopupContent popupContent)
    {
        _ = ShowPopupAsync(popupContent);
    }

    [Button("HidePopup")]
    public void ClosePopUp()
    {
        if (OnClose != null)
            OnClose();
        _ = HidePopupAsync();
    }

    public void AddOnCloseCallback(OnCloseFunction callback)
    {
        OnClose += callback;
    }

    public void ClearOnCloseCallback()
    {
        OnClose = null;
    }

    public void GoToNextPage()
    {
        audioSource.clip = buttonClickSound;
        audioSource.Play();
        contentText.pageToDisplay = ++currentPage;
        if (currentPage == contentText.textInfo.pageCount)
            HideNextPage();
        ShowPreviousPage();
    }

    public void GoToPreviousPage()
    {
        audioSource.clip = buttonClickSound;
        audioSource.Play();
        contentText.pageToDisplay = --currentPage;
        if (currentPage == 1)
            HidePreviousPage();

        nextPageButton.gameObject.SetActive(true);
    }

    private void HidePreviousPage()
    {
        previousPageButton.gameObject.SetActive(false);
    }

    private void ShowPreviousPage()
    {
        previousPageButton.gameObject.SetActive(true);
    }

    private void HideNextPage()
    {
        nextPageButton.gameObject.SetActive(false);
    }

    private void ShowNextPage()
    {
        nextPageButton.gameObject.SetActive(true);
    }

    public async UniTask ShowPopupAsync(PopupContent popupContent = null)
    {
        contentText.pageToDisplay = currentPage = 1;

        if (popupContent != null)
        {
            titleText.text = popupContent.Title;
            contentText.text = popupContent.Content;
            image.sprite = popupContent.Sprite;
        }

        HidePreviousPage();

        await UniTask.Delay(TimeSpan.FromSeconds(0.12)); //Seems like Text Mesh Pro needs sometime to update the pageCount after changing the text of the content

        if (HaveMulltiplePages())
            ShowNextPage();
        else
            HideNextPage();
        PostProcessingUtils.Instance.TurnOnBlurInstant(.52f);
        audioSource.clip = openSound;
        audioSource.Play();

        GetComponent<RectTransform>().DOScale(1f, .52f).SetEase(easeType);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
    }

    public async UniTask HidePopupAsync()
    {
        PostProcessingUtils.Instance.TurnOffBlurInstant(.52f);
        audioSource.clip = closeSound;
        audioSource.Play();
        GetComponent<RectTransform>().DOScale(0, .52f).SetEase(easeType);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
    }

    private bool HaveMulltiplePages()
    {
        return contentText.textInfo.pageCount > 1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(contentText, Input.mousePosition, null);

        if (linkIndex == -1)
            return;

        string linkId = contentText.textInfo.linkInfo[linkIndex].GetLinkID();

        var url = linkId switch
        {
            "URL_MOBILE_FOX" => "https://play.google.com/store/apps/details?id=com.LP.MobileFox&hl=pt&gl=US",
            "URL_TETRIS" => "https://github.com/lucrybpin/Unity-Tetris",
            "URL_FOR_FRIENDS" => "https://github.com/lucrybpin/for-friends",
            "URL_PATHFINDER" => "https://github.com/lucrybpin/pathfinder-with-sensors",
            "URL_AI" => "https://github.com/lucrybpin/AI-Unity",
            _ => ""
        };

        if (url != "")
            Application.OpenURL(url);


    }
}
