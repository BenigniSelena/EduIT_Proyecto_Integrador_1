using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FyingEffect : MonoBehaviour
{
    [SerializeField] private float flyPosition = 0.5f;
    [SerializeField] private float flyPositionSpeed = 1f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float flyMovement = Mathf.Sin(Time.time * flyPositionSpeed) * flyPosition;
        transform.position = initialPosition + new Vector3(0f, flyMovement, 0f);
    }
}
