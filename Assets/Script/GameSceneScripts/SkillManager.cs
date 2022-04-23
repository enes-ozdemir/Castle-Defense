using System;
using Script.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Script.GameSceneScripts
{
    public class SkillManager : MonoBehaviour
    {
        [FormerlySerializedAs("enemy")] [SerializeField]
        public EnemyManager enemyManager;
        private GameObject skillEndPrefab;

        private void Start()
        {
            skillEndPrefab = enemyManager.enemy.enemySkill.skillEndEffect;
        }

        private void Update()
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * enemyManager.enemy.enemySkill.skillSpeed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Castle"))
            {
                enemyManager.CastleGotHitByEnemy();
                Debug.Log("Castle git" + enemyManager.enemy.damage);
                SoundManager.PlaySound(enemyManager.enemy.enemySkill.sound);
                var explosionPosition = col.ClosestPoint(transform.position);
                var explosion = Instantiate(skillEndPrefab, explosionPosition, Quaternion.identity);
                Destroy(gameObject);
                Destroy(explosion, 1.5f);
            }
        }
    }
}