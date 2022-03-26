using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int waveDifficulty;
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
        isAttackAllowed = false;
    }

    void FixedUpdate()
    {
        if (isMovementAllowed) MoveTowardsTower();
    }

    private void MoveTowardsTower()
    {
        rb.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * enemy.speed;
    }

    private void GetHit(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isMovementAllowed = false;
            anim.Play("Dead");
            Destroy(col);
            Destroy(prefab, 1f);
        }
        else
        {
            healthBar.SetHealth((float) maxHealth / currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Arrow"))
        {
            GetHit(collision.GetComponent<ArrowManager>().damage);
            Destroy(collision.gameObject);
        }
        else if (collision.tag.Equals("Castle"))
        {
            anim.Play("Attack");
            isMovementAllowed = false;
            isAttackAllowed = true;

            var castle = collision.gameObject;
            castle.GetComponent<CastleHealthManager>().Hit(enemy.damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Castle"))
        {
            var castle = collision.gameObject;
            castle.GetComponent<CastleHealthManager>().Hit(enemy.damage);
        }
    }
}