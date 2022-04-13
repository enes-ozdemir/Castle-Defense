using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] public float skillSpeed = 1f;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject ExplodeEffect;
    [SerializeField] public Transform target;

    private void OnValidate()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * skillSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Castle"))
        {
            var explosionPosition = col.ClosestPoint(transform.position);
            Instantiate(ExplodeEffect, explosionPosition, Quaternion.identity);
            Destroy(particleSystem);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Castle"))
        {
        Instantiate(ExplodeEffect, target.position, Quaternion.identity);
        Destroy(particleSystem);
        }

    }
}