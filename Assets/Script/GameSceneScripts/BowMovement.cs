using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BowMovement : MonoBehaviour
{
    [FormerlySerializedAs("crosshairs")] public GameObject crosshair;
    public Vector3 target;


    [SerializeField] private Camera _camera;

    private void Start()
    {
        // Cursor.visible = false;
    }


    private void SetCrossHairToMouse()
    {
        target = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            _camera.transform.position.z));
        crosshair.transform.position = new Vector3(target.x, target.y);
    }

    private void FixedUpdate()
    {
        SetCrossHairToMouse();
    }
}