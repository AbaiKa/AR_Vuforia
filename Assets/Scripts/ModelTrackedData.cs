using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelTrackedData : MonoBehaviour
{
    [SerializeField] private VideosManager manager;
    [SerializeField] private ModelTargetBehaviour modelTargetBehaviour;
    [SerializeField] private VideoProperties videoProperties;
    void Start()
    {
        modelTargetBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (UIManager.Instance.CurrentPanel == PanelType.Tracking)
        {
            if (targetStatus.Status == Status.TRACKED)
            {
                Debug.Log("Õ¿ÿÿÀÀÀÀ»»»");
                manager.Launch(videoProperties);
            }
        }
    }

    void OnDestroy()
    {
        if (modelTargetBehaviour)
        {
            modelTargetBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
