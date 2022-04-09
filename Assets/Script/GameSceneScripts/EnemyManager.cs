using UnityEngine;

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        maxHealth = enemy.health;
        currentHealth = maxHealth;
        isMovementAllowed = true;

        healthBar.healthCanvas.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isMovementAllowed && isBaseMovementAllowed) MoveTowardsTower();
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
            healthBar.SetHealth((float) maxHealth / currentHealth);
            Debug.Log((float) maxHealth / currentHealth + "Oranı can");
        }
    }

    private void Die()
    {
        healthBar.SetHealth((float) maxHealth / 1f);
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
        else if (collision.tag.Equals("Castle") && isBaseAttackAllowed)
        {
            //todo remove the attack from here
            anim.Play("Attack");
            isMovementAllowed = false;
            isAttackAllowed = true;

            var castle = collision.gameObject;
            castle.GetComponentInParent<CastleHealthManager>().CastleGotHit(enemy.damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //todo Change this according to attack speed
        if (collision.tag.Equals("Castle") && isBaseAttackAllowed)
        {
            var castle = collision.gameObject;
            castle.GetComponentInParent<CastleHealthManager>().CastleGotHit(enemy.damage);
        }
    }
}