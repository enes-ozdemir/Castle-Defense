using UnityEngine;

public class BaseEnemyManager : MonoBehaviour
{
    public static bool isBaseAttackAllowed;
    public static bool isBaseMovementAllowed;

    private void Awake()
    {
        isBaseAttackAllowed = true;
        isBaseMovementAllowed = true;
    }
}