using Script.GameManagerScripts;
using TMPro;
using UnityEngine;

namespace Script.GameSceneScripts
{
    public class EnemyManager : BaseEnemyManager
    {
        private bool isMovementAllowed;
        private bool isAttackAllowed;
        public bool isDead;

        [Header("Dependencies")] public Enemy enemy;
        [SerializeField] private CastleHealthManager castleHealthManager;
        [SerializeField] private GameObject bloodParticle;
        [SerializeField] private GameObject damageText;

        [Header("Components")] public GameObject prefab;
        private Collider2D col;
        private Rigidbody2D rb;
        private Animator anim;

        [Space(10)] [Header("Bars UI")] [SerializeField]
        private HealthBar healthBar;

        public int maxHealth;
        public int currentHealth;

        [Header("Attack Config")] private float lastAttackTime;
        public float attackDelay;

        private void Awake()
        {
            InitEnemy();
        }

        private void InitEnemy()
        {
            attackDelay = enemy.attackSpeed;

            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            col = GetComponent<Collider2D>();

            maxHealth = enemy.health;
            currentHealth = maxHealth;
            isMovementAllowed = true;

            healthBar.healthCanvas.gameObject.SetActive(false);
            castleHealthManager = GetComponentInParent<CastleHealthManager>();
        }

        private void Update()
        {
            CheckAttack();
        }

        private void FixedUpdate()
        {
            if (isMovementAllowed && isBaseMovementAllowed) MoveTowardsCastle();
        }

        private void CheckAttack()
        {
            //Todo integrate attack speed
            float distanceToCastle = Mathf.Abs(transform.position.x - castleHealthManager.castleLocation.position.x);
            if (distanceToCastle < enemy.attackRange && Time.time > lastAttackTime + attackDelay)
            {
                anim.Play("Attack");
                isMovementAllowed = false;
                isAttackAllowed = true;

                CheckIfRangeUnit();

                castleHealthManager.CastleGotHit(enemy.damage);
                lastAttackTime = Time.time;
            }
        }

        private void CheckIfRangeUnit()
        {
            if (enemy.isRangeUnit)
            {
                GameObject vfx;

                var position = transform.position;

                if (enemy.enemySkill.skillStartEffect != null)
                {
                    var attackEffect = Instantiate(enemy.enemySkill.skillStartEffect, position,
                        Quaternion.identity);
                    Destroy(attackEffect, 1f);
                }

                vfx = Instantiate(enemy.enemySkill.skillPrefab, position + new Vector3(0, 0.2f, 0),
                    Quaternion.identity);
                Destroy(vfx, 5f);

                var skillManager = vfx.AddComponent<SkillManager>();
                skillManager.enemy = enemy;
            }
        }

        private void MoveTowardsCastle()
        {
            rb.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * enemy.speed;
        }

        private void GetHit(int damage)
        {
            var bloodParticlePrefab = Instantiate(bloodParticle, transform.position, Quaternion.identity);
            Destroy(bloodParticlePrefab, 1f);
            healthBar.healthCanvas.gameObject.SetActive(true);
            currentHealth -= damage;

            CheckDead();
        }

        private void CheckDead()
        {
            if (currentHealth <= 0)
            {
                if (!isDead) Die();
            }
            else
            {
                healthBar.SetHealth((float) currentHealth / maxHealth);
            }
        }

        private void Die()
        {
            isDead = true;

            healthBar.healthCanvas.gameObject.SetActive(false);

            if (GameUIManager.currentEnemyCount != 0) GameUIManager.currentEnemyCount--;

            healthBar.SetHealth((float) currentHealth / maxHealth);
            isMovementAllowed = false;
            anim.Play("Dead");

            GoldDropManager.Instance.CheckLoot(enemy.enemyGoldAward, transform.position);

            WaveSpawner.RemoveEnemyFromWave();
            Destroy(col);
            Destroy(prefab, 1f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Arrow"))
            {
                collision.gameObject.SetActive(false);
                int damage = (int) ((WeaponStats.additionalWeaponDamage + WeaponStats.weaponBaseDamage) *
                                    Random.Range(0.8f, 1.2f));

                GetHit(damage);
                var damageTextPrefab = Instantiate(damageText, transform.position + new Vector3(0, 0.4f, 0),
                    Quaternion.identity);

                damageTextPrefab.GetComponentInChildren<TextMeshPro>().text = damage.ToString();

                CheckIfAddForceToEnemy();
            }
        }

        private void CheckIfAddForceToEnemy()
        {
            if (isMovementAllowed && !enemy.isBoss)
            {
                Vector3 hitForce = new Vector3(0.05f, 0, 0);
                transform.position += hitForce;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.Equals("Castle"))
            {
                isMovementAllowed = true;
                isAttackAllowed = false;
                MoveTowardsCastle();
                anim.Play("Walk");
            }
        }
    }
}