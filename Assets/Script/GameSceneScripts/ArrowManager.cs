using Script.GameManagerScripts;
using Script.Utils;
using UnityEngine;

namespace Script.GameSceneScripts
{
    public class ArrowManager : MonoBehaviour
    {
        public GameObject player;

        //Arrow Settings
        public GameObject[] arrowStart;
        public float arrowSpeed = 10f;
        private int arrowCount;
        private Sprite arrowSprite;

        //Bow Settings
        [SerializeField] private BowMovement bowMovement;
        [SerializeField] private float fireInterval = 1f;
        [SerializeField] private float fireTimer;
        private Vector3 target;
        private Vector3 differenceFromTarget;
        private float rotationZ;
        public static bool canPlayerAttack;

        private void Awake()
        {
            bowMovement = GetComponent<BowMovement>();

            InitBowAndArrowConfig();
        }

        private void InitBowAndArrowConfig()
        {
            arrowSprite = Arrow.arrowSprite;
            arrowCount = ArrowStats.arrowCount;
            //fireInterval = 0.2f / ArrowStats.fireInterval;
            fireInterval = 0.8f / ArrowStats.fireInterval;
            canPlayerAttack = true;

            Debug.Log("Fire Interval : " + fireInterval);
            Debug.Log("Arrow Count : " + arrowCount);
        }

        private void FixedUpdate()
        {
            SetBowRotation();
        }

        private void Update()
        {
            CheckFireTimer();
        }

        private void CheckFireTimer()
        {
            if (fireTimer <= 0)
            {
                IsArrowFired();
                fireTimer = fireInterval;
            }
            else fireTimer -= Time.fixedDeltaTime;
        }

        private void SetBowRotation()
        {
            differenceFromTarget = bowMovement.target - player.transform.position;
            rotationZ = Mathf.Atan2(differenceFromTarget.y, differenceFromTarget.x) * Mathf.Rad2Deg - 50f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }

        private void IsArrowFired()
        {
            if (!canPlayerAttack) return;
            if (Input.GetMouseButton(0))
            {
                float distance = differenceFromTarget.magnitude;
                Vector2 dir = differenceFromTarget / distance;
                dir.Normalize();
                FireArrow(dir, rotationZ + 50f);
            }
        }

        private void FireArrow(Vector2 dir, float rotationZ)
        {
            for (int i = 0; i < arrowCount; i++)
            {
                SoundManager.PlaySound(SoundManager.Sound.ArrowSound);
                GameObject arrow = ObjectPooler.Instance.SpawnArrowFromPool("Arrow", arrowStart[i].transform.position,
                    Quaternion.Euler(0, 0, rotationZ));
                arrow.gameObject.GetComponent<SpriteRenderer>().sprite = arrowSprite;
                arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
            }
        }
    }
}