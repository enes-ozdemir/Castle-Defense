using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Script.Utils
{
    public class PopupManager : MonoBehaviour
    {
        private static TextMeshProUGUI _cannotAffordText;
        [SerializeField] private TextMeshProUGUI originalText;

        private static PopupManager _instance;

        private void Awake()
        {
            _cannotAffordText = originalText;
            _instance = this;
        }

        public static void ShowCannotAffordPopup()
        {
            Vector3 mousePosition = Input.mousePosition + new Vector3(0, 50, 0);
            _instance.StartCoroutine(WaitForFadeEffect(_cannotAffordText.gameObject));
            _cannotAffordText.transform.position = mousePosition;
        }
        public static void ShowMessagePopup(string message)
        {
            Vector3 mousePosition = Input.mousePosition + new Vector3(0, 50, 0);
            _cannotAffordText.text = message;
            _instance.StartCoroutine(WaitForFadeEffect(_cannotAffordText.gameObject));
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
}