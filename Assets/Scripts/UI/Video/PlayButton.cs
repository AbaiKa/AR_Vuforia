using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public UnityEvent<VideoProperties> onClick;
    private VideoProperties properties;
    public void Init(VideoProperties properties)
    {
        this.properties = properties;

        nameText.text = properties.Name;
        descriptionText.text = properties.Description;

        playButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        onClick?.Invoke(properties);
    }
}
