using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Button demosButton;
    [SerializeField] private Button trackingButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private GameObject demosContainerPanel;

    [SerializeField] private VideosManager videoManager;

    [Header("Video")]
    [SerializeField] private Slider contentOpacitySlider;

    [HideInInspector] public Action<float> onOpacitySliderChanged;

    public PanelType CurrentPanel { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        demosButton.onClick.AddListener(OnClickDemoButton);
        trackingButton.onClick.AddListener(OnClickTrackingButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        videoManager.onVideoStart += OnVideoStart;
        videoManager.onVideoEnd += OnVideoEnd;
        contentOpacitySlider.onValueChanged.AddListener((c) => { onOpacitySliderChanged?.Invoke(c); });
        videoManager.Init();


        OnVideoEnd();
        exitButton.gameObject.SetActive(false);
    }
    private void OnClickDemoButton()
    {
        CurrentPanel = PanelType.Demos;
        contentPanel.SetActive(false);
        demosContainerPanel.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
    private void OnClickTrackingButton()
    {
        CurrentPanel = PanelType.Tracking;
        contentPanel.SetActive(false);
        demosContainerPanel.SetActive(false);
        exitButton.gameObject.SetActive(true);
    }
    private void OnClickExitButton()
    {
        contentPanel.SetActive(true);
        contentOpacitySlider.gameObject.SetActive(false);
        demosContainerPanel.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }


    private void OnVideoStart()
    {
        contentPanel.SetActive(false);
        demosContainerPanel.SetActive(false);
        exitButton.gameObject.SetActive(false);
        contentOpacitySlider.gameObject.SetActive(true);
        contentOpacitySlider.value = 1;
    }
    private void OnVideoEnd()
    {
        contentPanel.SetActive(true);
        contentOpacitySlider.gameObject.SetActive(false);
        demosContainerPanel.SetActive(false);
    }
}

public enum PanelType
{
    Demos,
    Tracking
}
