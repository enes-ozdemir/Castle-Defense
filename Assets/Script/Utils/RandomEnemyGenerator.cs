using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class RandomEnemyGenerator
{
    public static Enemy GetRandomEnemy(List<WaveUnit> weightedValues)
    {
        Enemy enemyToGenerate = null;

        var totalWeight = 0;

        foreach (var enemyType in weightedValues)
        {
            totalWeight += enemyType.rarity;
        }

        var randomWeightValue = Random.Range(1, totalWeight + 1);

        var processedWeight = 0;

        foreach (var enemyType in weightedValues)
        {
            processedWeight += enemyType.rarity;
            if (randomWeightValue <= processedWeight)
            {
                enemyToGenerate = enemyType.enemy;
                break;
            }
        }

        return enemyToGenerate;
    }
}