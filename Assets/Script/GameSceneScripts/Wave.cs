using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
   public List<WaveUnit> waveUnits;
    public int award;
}

[System.Serializable]
public class WaveUnit
{
    public Enemy enemy;
    public int rarity;
}