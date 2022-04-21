using System.Collections;
using UnityEngine;

namespace Script.Utils
{
    public class CameraShake : MonoBehaviour
    {
        public void ShakeCaller(float amount, float duration)
        {
            StartCoroutine(Shake(amount, duration));
        }

        private IEnumerator Shake(float amount, float duration)
        {
            Vector3 originalPos = transform.localPosition;
            int counter = 0;
            while (duration > 0.01f)
            {
                counter++;
                var x = Random.Range(-1f, 1f) * (amount / counter);
                var y = Random.Range(-1f, 1f) * (amount / counter);
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3
                    (originalPos.x + x, originalPos.y + y, originalPos.z), 0.5f);
                duration -= Time.deltaTime;

                yield return new WaitForSeconds(0.1f);
            }

            transform.localPosition = originalPos;
        }
    }
}