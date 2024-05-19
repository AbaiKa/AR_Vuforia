using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Videos", order = 1)]
public class VideoProperties : ScriptableObject
{
    [field: SerializeField] public UnityEngine.Video.VideoClip Clip { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}
