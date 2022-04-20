using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : BaseEnemyManager
{
    private bool isMovementAllowed;
    private bool isAttackAllowed;

    [Header("Dependencies")] public Enemy enemy;

    [Header("Components")] public GameObject prefab;
    private Collider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    [Space(10)] [Header("Bars UI")] [SerializeField]
    private HealthBar healthBar;

    public int maxHealth;
    public int currentHealth;

    private float lastAttackTime;
    public float attackDelay;

    [SerializeField] private CastleHealthManager castleHealthManager;

    [SerializeField] private GameObject bloodParticle;

    public bool isDead = false;

    [SerializeField] private GameObject damageText;

    private void Awake()
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
        if (isMovementAllowed && isBaseMovementAllowed) MoveTowardsTower();
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
            GameObject vfx;

            if (enemy.isRangeUnit)
            {
                var position = transform.position;

                if (enemy.enemySkill.skillStartEffect != null)
                {
                    var attackEffect = Instantiate(enemy.enemySkill.skillStartEffect, position, Quaternion.identity);
                    Destroy(attackEffect, 1f);
                }

                vfx = Instantiate(enemy.enemySkill.skillPrefab, position + new Vector3(0, 0.2f, 0),
                    Quaternion.identity);
                var skillManager = vfx.AddComponent<SkillManager>();
                Destroy(vfx, 5f);
                skillManager.enemy = enemy;
            }

            castleHealthManager.CastleGotHit(enemy.damage);
            lastAttackTime = Time.time;
        }
    }

    private void MoveTowardsTower()
    {
        rb.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * enemy.speed;
    }

    private void GetHit(int damage)
    {
        var bloodParticlePrefab = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        Destroy(bloodParticlePrefab, 1f);
        healthBar.healthCanvas.gameObject.SetActive(true);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if (!isDead) Die();
            isDead = true;
        }
        else
        {
            healthBar.SetHealth((float) currentHealth / maxHealth);
        }
    }

    private void Die()
    {
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
            if (isMovementAllowed && !enemy.isBoss)
            {
                Vector3 hitForce = new Vector3(0.05f, 0, 0);
                transform.position += hitForce;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Castle"))
        {
            isMovementAllowed = true;
            isAttackAllowed = false;
            MoveTowardsTower();
            anim.Play("Walk");
        }
    }
}