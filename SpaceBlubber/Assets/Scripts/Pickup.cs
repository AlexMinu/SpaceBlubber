using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public float massValue = 5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement.instance.IncreaseMass(massValue);
        Destroy(gameObject);
    }
}
