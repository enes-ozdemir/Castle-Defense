using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    [SerializeField] public GameObject skillPrefab;
    [SerializeField] public GameObject skillStartEffect;
    [SerializeField] public GameObject skillEndEffect;
    [SerializeField] public float skillSpeed = 1f;
}