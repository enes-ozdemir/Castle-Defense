using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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