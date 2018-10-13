using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float moveForce = 1f;

    Vector3 touchPosition;
    float swipeDeadZone = 50f;
    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;
            float swipeXAbs = Mathf.Abs(deltaSwipe.x);
            float swipeYAbs = Mathf.Abs(deltaSwipe.y);

            if(swipeXAbs > swipeDeadZone || swipeYAbs > swipeDeadZone)
            {
                if (swipeXAbs >= swipeYAbs)//RIGHT or LEFT
                {
                    rigidBody.AddForce(transform.right * (deltaSwipe.x > 0f ? 1 : -1) * moveForce, ForceMode.Impulse);
                }
                else//UP or DOWN
                {
                    rigidBody.AddForce(transform.up * (deltaSwipe.y > 0f ? 1 : -1) * moveForce, ForceMode.Impulse);
                }
            }
        }
    }
}
