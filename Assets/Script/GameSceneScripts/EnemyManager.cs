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
    
    private void Awake()
    {
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
                
                Instantiate(enemy.enemySkill.skillStartEffect, position, Quaternion.identity);
                vfx = Instantiate(enemy.enemySkill.skillPrefab, position, Quaternion.identity);
                var skillManager = vfx.AddComponent<SkillManager>();
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
        healthBar.healthCanvas.gameObject.SetActive(true);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
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
            GetHit(GameManager.weapon.WeaponStats.WeaponDamage);

            Vector3 hitForce =
                new Vector3(
                    0.05f, 0, 0);

            transform.position += hitForce;
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