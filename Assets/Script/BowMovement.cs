using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMovement : MonoBehaviour
{

    public GameObject crosshairs;
    private Vector3 target;
    private Camera _camera;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;
    public GameObject arrowStart;
    public GameObject player;
    public float arrowSpeed = 10f;

    private void Awake()
    {
        _camera = transform.GetComponent<Camera>();
    }

    private void FireBullet(Vector2 dir, float rotationZ)
    {
        GameObject arrow = Instantiate(arrowPrefab,player.transform);
        arrow.transform.position = arrowStart.transform.position;
        arrow.transform.rotation = Quaternion.Euler(0,0,rotationZ);
        arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowSpeed;
    }

    private void SetCrossHairToMouse()
    {
        target = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,transform.position.z));
        crosshairs.transform.position = new Vector3(target.x,target.y);

        Vector3 difference = target - bowPrefab.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        // if(rotationZ<90f && rotationZ>-90f)
        bowPrefab.transform.rotation = Quaternion.Euler(0,0,rotationZ);
        
        if (Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 dir = difference / distance;
            dir.Normalize();
            FireBullet(dir, rotationZ);
        }
    }

    void FixedUpdate()
    {
        SetCrossHairToMouse();
    }
}
