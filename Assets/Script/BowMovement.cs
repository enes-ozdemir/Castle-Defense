using UnityEngine;

public class BowMovement : MonoBehaviour
{
    public GameObject crosshairs;
    private Vector3 target;
    public GameObject arrowStart;
    public GameObject player;
    public float arrowSpeed = 10f;

    private WeaponManager weaponManager;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
       // Cursor.visible = false;
    }

    private void FireBullet(Vector2 dir, float rotationZ)
    {
        GameObject arrow = Instantiate(weaponManager.arrowPrefab, player.transform);
        weaponManager.arrowPrefab.GetComponent<ArrowManager>().damage = weaponManager.damage;
        arrow.transform.position = arrowStart.transform.position;
        arrow.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
    }

    private void SetCrossHairToMouse()
    {
        target = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            _camera.transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -50f;

        // if(rotationZ<90f && rotationZ>-90f)
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

       // weaponManager.bowPrefab.transform.LookAt(crosshairs.transform.position);
        if (Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 dir = difference / distance;
            dir.Normalize();
            FireBullet(dir, rotationZ+50f);
        }
    }

    void FixedUpdate()
    {
        SetCrossHairToMouse();
    }
}