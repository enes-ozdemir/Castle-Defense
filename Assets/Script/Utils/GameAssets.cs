using UnityEngine;

namespace Script.Utils
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _instance;

        public static GameAssets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
                return _instance;
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
}