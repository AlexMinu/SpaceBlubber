using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float moveForce = 1f;
    public bool invertedControls = false;

    public float massMoveCostPercent = 10f;
    public float criticalMass = 20f;

    public static PlayerMovement instance;

    Vector3 touchPosition;
    float swipeDeadZone = 50f;
    Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        instance = this;
    }

    private void Update()
    {
        #region Editor or Standalone Controlls
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;
            float swipeXAbs = Mathf.Abs(deltaSwipe.x);
            float swipeYAbs = Mathf.Abs(deltaSwipe.y);
            if (swipeXAbs > swipeDeadZone || swipeYAbs > swipeDeadZone)
            {
                if (swipeXAbs >= swipeYAbs)//RIGHT or LEFT
                {
                    Move(transform.right * (deltaSwipe.x > 0f ? 1 : -1));
                }
                else//UP or DOWN
                {
                    Move(transform.up * (deltaSwipe.y > 0f ? 1 : -1));
                }
            }
        }
#endif
        #endregion

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 deltaSwipe = (Vector2)touchPosition - Input.GetTouch(0).position;
                float swipeXAbs = Mathf.Abs(deltaSwipe.x);
                float swipeYAbs = Mathf.Abs(deltaSwipe.y);

                if (swipeXAbs > swipeDeadZone || swipeYAbs > swipeDeadZone)
                {
                    if (swipeXAbs >= swipeYAbs)//RIGHT or LEFT
                    {
                        Move(transform.right * (deltaSwipe.x > 0f ? 1 : -1));
                    }
                    else//UP or DOWN
                    {
                        Move(transform.up * (deltaSwipe.y > 0f ? 1 : -1));
                    }
                }
            }
        }
#endif
    }

    void Move(Vector3 direction)
    {
        rigidBody.AddForce(direction * moveForce * (invertedControls ? -1 : 1), ForceMode.VelocityChange);

        rigidBody.mass *= 1f - massMoveCostPercent / 100f;
        if(rigidBody.mass < criticalMass)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseMass(float ammount)
    {
        rigidBody.mass += ammount;
    }
}
