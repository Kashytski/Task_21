using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool active = true;
    private Vector3 move = new Vector3(0, 1, 0);
    private Vector3 spawnPoint = new Vector3(0, 35.95f, 0);

    void Awake()
    {
        ShipController.OnChangeSpeed += ChangeSpeed;
    }

    private void ChangeSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    void OnDestroy()
    {
        ShipController.OnChangeSpeed += ChangeSpeed;
    }
    void Update()
    {
        transform.position -= move * speed / 10 * Time.deltaTime;
        if (transform.position.y < -spawnPoint.y)
            transform.position = spawnPoint;
    }
}