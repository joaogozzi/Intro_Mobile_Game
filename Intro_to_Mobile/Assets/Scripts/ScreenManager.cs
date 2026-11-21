using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject inputBlocker = null;
    [SerializeField] List<GameObject> screens = new List<GameObject>();
    [SerializeField] string initialScreen;
    [SerializeField] private float transitionDuration = 1.0f;
    private GameObject topScreen = null;
    private Coroutine screenTransition = null;

    private static ScreenManager instance = null;
    public static ScreenManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            foreach (GameObject screen in screens)
            {
                screen.SetActive(initialScreen == screen.name);
            }
        }
        else
            Destroy(gameObject);
    }

    public void OpenScreen(string screenName)
    {
        topScreen = null;
        foreach (GameObject screen in screens)
        {
            if (screen.name == screenName)
            {
                topScreen = screen;
                break;
            }
        }

        if (screenTransition != null)
            StopCoroutine(screenTransition);

        screenTransition = StartCoroutine(OpenScreen(topScreen));
    }

    public void CloseScreen()
    {
        if (topScreen == null)
            return;

        if (screenTransition != null)
            StopCoroutine(screenTransition);

        screenTransition = StartCoroutine(CloseScreen(topScreen));
    }

    IEnumerator OpenScreen(GameObject go)
    {
        inputBlocker.SetActive(true);
        RectTransform rectTransform = go.transform as RectTransform;
        rectTransform.anchoredPosition = new Vector2(-Screen.width * 2, rectTransform.anchoredPosition.y);
        go.SetActive(true);

        Tween screenTween = rectTransform.DOAnchorPosX(0.0f, transitionDuration).SetEase(Ease.OutExpo);
        yield return screenTween.WaitForCompletion();

        screenTween.Kill();

        inputBlocker.SetActive(false);
    }

    IEnumerator CloseScreen(GameObject go)
    {
        inputBlocker.SetActive(true);
        RectTransform rectTransform = go.transform as RectTransform;

        Tween screenTween = rectTransform.DOAnchorPosX(-Screen.width * 2, transitionDuration).SetEase(Ease.OutExpo);
        yield return screenTween.WaitForCompletion();

        screenTween.Kill();
        go.SetActive(false);

        inputBlocker.SetActive(false);
    }
}