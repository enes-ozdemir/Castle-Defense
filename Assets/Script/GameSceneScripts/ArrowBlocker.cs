using System;
using UnityEngine;

namespace Script.GameSceneScripts
{
    public class ArrowBlocker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Arrow"))
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}