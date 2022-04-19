using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowLevel = 1;
    public Sprite arrowSprite;
    public ArrowStats arrowStats;

    private void Awake()
    {
        GameManager.arrow = this;
        arrowStats = new ArrowStats();
    }
}