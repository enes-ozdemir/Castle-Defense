using Script.Utils;
using UnityEngine;

namespace Script.GameSceneScripts
{
    [CreateAssetMenu]
    public class Skill : ScriptableObject
    {
        [SerializeField] public GameObject skillPrefab;
        [SerializeField] public GameObject skillStartEffect;
        [SerializeField] public GameObject skillEndEffect;
        [SerializeField] public SoundManager.Sound sound;
        [SerializeField] public float skillSpeed = 1f;
    }
}