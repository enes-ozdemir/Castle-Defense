using System;
using UnityEngine;
using System.Collections.Generic;
using Script;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowStart;
    public float arrowSpeed = 10f;
    public GameObject player;

    [SerializeField] private WeaponManager weaponManager;

    private Sprite arrowSprite;
    private void Start()
    {
        arrowSprite = GameManager.Arrow.arrowSprite;
    }

    public void FireArrow(Vector2 dir, float rotationZ)
    {
        GameObject arrow = ObjectPooler.Instance.SpawnArrowFromPool("Arrow", arrowStart.transform.position,
            Quaternion.Euler(0, 0, rotationZ));
        var spriteRenderer = arrow.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = arrowSprite;
        //  GameObject arrow = Instantiate(weaponManager.arrowPrefab, player.transform);
        // arrow.transform.position = arrowStart.transform.position;
        //arrow.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
    }
}