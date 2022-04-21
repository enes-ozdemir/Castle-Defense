using System.Collections.Generic;
using UnityEngine;

namespace Script.GameSceneScripts
{
    [CreateAssetMenu]
    public class Wave : ScriptableObject
    {
        public List<WaveUnit> waveUnits;
        public int waveDuration;
        public int waveValue;
        public Enemy boss;
    }

    [System.Serializable]
    public class WaveUnit
    {
        public Enemy enemy;
        public int rarity;
    }
}