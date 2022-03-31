using UnityEngine;
using Script;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowStart;
    public float arrowSpeed = 10f;
    public GameObject player;

    [SerializeField] private WeaponManager weaponManager;

    private Sprite arrowSprite;
    private void Start()
    {
        arrowSprite = GameManager.Arrow.arrowSprite;
    }

    public void FireArrow(Vector2 dir, float rotationZ)
    {
        GameObject arrow = ObjectPooler.Instance.SpawnArrowFromPool("Arrow", arrowStart.transform.position,
            Quaternion.Euler(0, 0, rotationZ));
        arrow.gameObject.GetComponent<SpriteRenderer>().sprite = arrowSprite;
        arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
    }
}