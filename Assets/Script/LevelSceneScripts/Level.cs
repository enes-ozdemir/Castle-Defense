using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] public Sprite lockImage;
    [SerializeField] public Sprite currentLevelImage;
    [SerializeField] public Sprite oldLevelImage;
}