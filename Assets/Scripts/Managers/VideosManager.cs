using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideosManager : MonoBehaviour
{
    [SerializeField] private PlayButton playButtonPrefab;
    [SerializeField] private Transform playButtonsContainer;
    [SerializeField] private VideoPlayback videoPlayerComponent;
    [SerializeField] private VideoProperties[] videoProperties;

    [HideInInspector] public Action onVideoStart;
    [HideInInspector] public Action onVideoEnd;
    public bool InProgress { get; private set; }
    public void Init()
    {
        for (int i = 0; i < videoProperties.Length; i++)
        {
            var button = Instantiate(playButtonPrefab, playButtonsContainer);
            button.Init(videoProperties[i]);
            button.onClick.AddListener(Launch);
        }


        videoPlayerComponent.onVideoStart += () => onVideoStart?.Invoke();
        videoPlayerComponent.onVideoEnd += () =>
        {
            InProgress = false;
            onVideoEnd?.Invoke();
            videoPlayerComponent.gameObject.SetActive(false);
        };

        videoPlayerComponent.gameObject.SetActive(false);
    }

    public void Launch(VideoProperties properties)
    {
        if (InProgress)
            return;

        InProgress = true;

        videoPlayerComponent.gameObject.SetActive(true);

        videoPlayerComponent.transform.position = Camera.main.transform.forward * 2;
        videoPlayerComponent.transform.LookAt(Camera.main.transform);

        Vector3 euler = Camera.main.transform.transform.rotation.eulerAngles;
        videoPlayerComponent.transform.rotation = Quaternion.Euler(0f, euler.y, euler.z);

        videoPlayerComponent.Init(properties);
    }
}
