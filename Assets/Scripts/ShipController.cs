using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float speedCharacter;
    [SerializeField] Rigidbody rbody;
    [SerializeField] TouchPad touchPad;
    [SerializeField] float roll;
    [SerializeField] float tilt;
    public static event Action<int> OnChangeSpeed;
    private bool touchDown;
    private bool touchUp;

    void FixedUpdate()
    {
        Vector2 direction = touchPad.GetDirection();
        rbody.velocity = new Vector3(direction.x, 0, 0) * speedCharacter;
        rbody.rotation = Quaternion.Euler(Mathf.Abs(rbody.velocity.x) * tilt, rbody.velocity.x * roll, 0);

        if (Input.touchCount > 0)
        {
            if (!touchDown)
            {
                OnChangeSpeed?.Invoke(80);
                touchDown = true;
                touchUp = false;

            }
            rbody.velocity = new Vector3(rbody.velocity.x, -10 - transform.position.y, rbody.velocity.z);
        }
        else if (transform.position.y > -16)
        {
            if (!touchUp)
            {
                OnChangeSpeed?.Invoke(40);
                touchDown = false;
                touchUp = true;
            }
            rbody.velocity = new Vector3(rbody.velocity.x, -16 + Mathf.Abs(transform.position.y), rbody.velocity.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
            Destroy(gameObject);
    }
}
