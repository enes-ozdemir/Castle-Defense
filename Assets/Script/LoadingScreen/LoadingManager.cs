using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] loadingBackgrounds;

    [FormerlySerializedAs("LoadingBarFill")]
    public Image loadingBarFill;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }

    private void Start()
    {
        int rand = Random.Range(1, Constant.Tips.Length);
        tipsText.text = Constant.Tips[rand];

        int random = Random.Range(1, loadingBackgrounds.Length);
        backgroundImage.sprite = loadingBackgrounds[random];

        LoadScene(SceneManagement.sceneNumber);
    }
}

public static class SceneManagement
{
    public static int sceneNumber;
}