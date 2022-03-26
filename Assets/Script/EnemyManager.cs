using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int waveDifficulty;

    public Enemy enemy;

    private Rigidbody2D rb;

    private GameObject bloodEffect;

    public int maxHealth;
    public int currentHealth;
    private Animator anim;

    public GameObject prefab;

    private Player player;

    [Space(10)] [Header("Bars UI")] [SerializeField]
    private HealthBar healthBar;

    private bool isMovementAllowed;

    private Collider2D col;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = enemy.health;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        isMovementAllowed = true;
        col = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (isMovementAllowed) MoveTowardsTower();
    }

    private void MoveTowardsTower()
    {
        rb.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * enemy.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetHit(collision.GetComponentInParent<Player>().damage);
        Destroy(collision.gameObject);
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
            healthBar.SetHealth(maxHealth / currentHealth);
        }
    }
}