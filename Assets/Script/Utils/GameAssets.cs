using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _Instance;

    public static GameAssets Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _Instance;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}