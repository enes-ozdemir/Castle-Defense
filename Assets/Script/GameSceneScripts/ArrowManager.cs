using System;
using UnityEngine;
using Script;

public class ArrowManager : MonoBehaviour
{
    public GameObject[] arrowStart;
    public float arrowSpeed = 10f;
    public GameObject player;

    [SerializeField] private BowMovement bowMovement;

    private Vector3 target;
    private Sprite arrowSprite;

    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float fireTimer;
    private Vector3 difference;
    private float rotationZ;

    [HideInInspector] public static bool canAttack;

    private void Awake()
    {
        arrowSprite = GameManager.arrow.arrowSprite;
        bowMovement = GetComponent<BowMovement>();
        canAttack = true;
    }

    private void FixedUpdate()
    {
        SetBowRotation();
    }

    private void Update()
    {
        if (fireTimer <= 0)
        {
            IsArrowFired();
            fireTimer = fireInterval;
        }
        else
        {
            fireTimer -= Time.fixedDeltaTime;
        }
    }

    private void SetBowRotation()
    {
        difference = bowMovement.target - player.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 50f;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    private void IsArrowFired()
    {
        if (!canAttack) return;
        if (Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 dir = difference / distance;
            dir.Normalize();
            FireArrow(dir, rotationZ + 50f);
        }
    }

    private void FireArrow(Vector2 dir, float rotationZ)
    {
        for (int i = 0; i < GameManager.weapon.weaponStats.arrowCount; i++)
        {
            GameObject arrow = ObjectPooler.Instance.SpawnArrowFromPool("Arrow", arrowStart[i].transform.position,
                Quaternion.Euler(0, 0, rotationZ)); 
            arrow.gameObject.GetComponent<SpriteRenderer>().sprite = arrowSprite;
            arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
        }
    }
}