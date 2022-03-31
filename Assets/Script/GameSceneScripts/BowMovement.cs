using System;
using UnityEngine;


public class BowMovement : MonoBehaviour
{
    public GameObject crosshairs;
    private Vector3 target;

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


    private void SetCrossHairToMouse()
    {
        target = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            _camera.transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y);

        Vector3 difference = target - weaponManager.arrowManager.player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 50f;

        // if(rotationZ<90f && rotationZ>-90f)
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 dir = difference / distance;
            dir.Normalize();
            var arrow = GameManager.Arrow.arrowSprite;
            weaponManager.arrowManager.FireArrow(dir, rotationZ + 50f);
        }
    }

    void FixedUpdate()
    {
        SetCrossHairToMouse();
    }
}