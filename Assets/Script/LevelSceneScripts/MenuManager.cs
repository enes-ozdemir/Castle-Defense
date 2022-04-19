using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }
}