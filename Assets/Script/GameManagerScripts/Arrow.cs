using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowLevel = 1;
    public Sprite arrowSprite;
    public ArrowStats ArrowStats;

    private void Awake()
    {
        GameManager.arrow = this;
        ArrowStats = new ArrowStats();
    }
}