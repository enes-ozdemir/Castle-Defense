using Script.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.LevelSceneScripts
{
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.PlaySound(SoundManager.Sound.BackgroundMusic);
        }

        public void LoadMapScene()
        {
            SceneManager.LoadScene("MapScene");
        }
    }
}