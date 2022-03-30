using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] public Sprite lockImage;
    [SerializeField] public Sprite levelImage;
    [SerializeField] public Sprite backgroundImage;
    [SerializeField] public Sprite levelCountImage;
}