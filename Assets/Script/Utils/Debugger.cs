using System.Collections;
using Script.GameSceneScripts;
using UnityEngine;

namespace Script.Utils
{
    public class Debugger : MonoBehaviour
    {
        private IEnumerator Start()
        {
            WaveSpawner wave = GetComponent<WaveSpawner>();

            while (true)
            {
                yield return new WaitForSeconds(1);
                Debug.Log($"Time interval {wave.spawnTimer}");
            }
        }
    }
}