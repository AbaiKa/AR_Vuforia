using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayback : MonoBehaviour
{
    [SerializeField] private Transform playerContent;
    [SerializeField] private VideoPlayer videoComponent;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button exitButton;

    [HideInInspector] public Action onVideoStart;
    [HideInInspector] public Action onVideoEnd;

    private bool isStoped = false;
    private double stopedTime;
    private TextMeshProUGUI stopButtonText;
    private void Start()
    {
        stopButtonText = stopButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Init(VideoProperties properties)
    {
        StartCoroutine(InitRoutine(properties));
    }

    private IEnumerator InitRoutine(VideoProperties properties)
    {
        yield return new WaitForEndOfFrame();
        videoComponent.clip = properties.Clip;
        nameText.text = properties.Name;
        descriptionText.text = properties.Description;

        isStoped = false;
        stopedTime = 0;
        videoComponent.Play();

        onVideoStart?.Invoke();
        Invoke(nameof(OnVideoEnd), (float)videoComponent.clip.length);
        stopButton.onClick.AddListener(OnStopButtonClick);
        exitButton.onClick.AddListener(OnVideoEnd);
        UIManager.Instance.onOpacitySliderChanged += OnOpacityChanged;
    }

    private void OnVideoEnd()
    {
        onVideoEnd?.Invoke();
        CancelInvoke();
    }

    private void OnOpacityChanged(float value)
    {
        canvasGroup.alpha = value;
    }

    private void OnStopButtonClick()
    {
        isStoped = !isStoped;

        string text = "Stop";

        if (isStoped)
        {
            text = "Play";
            stopedTime = videoComponent.time;
            videoComponent.Stop();
        }
        else
        {
            videoComponent.time = stopedTime;
            videoComponent.Play();
        }

        stopButtonText.text = text;
    }
}
