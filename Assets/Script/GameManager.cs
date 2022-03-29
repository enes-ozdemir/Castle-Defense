using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] private CastleHealthManager castleHealthManager;
    [SerializeField] private int castleLevel;
    
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private int weaponLevel;

    private int maxEnemyCount;
    private int currentEnemyCount;

    private int waveNumber;
    
    void Start()
    {
        castleHealthManager.SetCastleLevel(castleLevel);
    }

    void Update()
    {
        
    }
}
