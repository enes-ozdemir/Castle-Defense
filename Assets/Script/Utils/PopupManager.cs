using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    private static TextMeshProUGUI _cannotAffordText;
    [SerializeField] private TextMeshProUGUI originalText;

    private static PopupManager Instance;

    private void Awake()
    {
        _cannotAffordText = originalText;
        Instance = this;
    }

    public static void ShowCannotAffordPopup()
    {
        Vector3 mousePosition = Input.mousePosition + new Vector3(0, 50, 0);
        Instance.StartCoroutine(WaitForFadeEffect(_cannotAffordText.gameObject));
        _cannotAffordText.transform.position = mousePosition;
    }

    private static IEnumerator WaitForFadeEffect(GameObject popupObject)
    {
        popupObject.SetActive(true);
        _cannotAffordText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        _cannotAffordText.DOFade(1f, 0.5f);
        popupObject.SetActive(false);
    }
}