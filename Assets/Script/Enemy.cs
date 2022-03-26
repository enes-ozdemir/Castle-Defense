using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    [Header("Enemy Info")] 
    [SerializeField] private string enemyName;
    [SerializeField] public int health;
    [SerializeField] public float speed;
    [SerializeField] public int damage;
    [SerializeField] public int attackSpeed;
    [SerializeField] public AttackType attackType;
    
    [Space(10)]
    [SerializeField] public GameObject enemyPrefab;

}