using System;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int castleLevel = 13;
    public int castleHealth = 10200;
    public int castleUpgradeCost = 100;

    private void Awake()
    {
        GameManager.castle = this;
    }
}