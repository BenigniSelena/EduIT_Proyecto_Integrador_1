using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlayer : MonoBehaviour
{
    [SerializeField] private float flyPosition = 0.5f;
    [SerializeField] private float flyPositionSpeed = 1f;

    [SerializeField] private float moveSpeed = 10f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float flyMovement = Mathf.Sin(Time.time * flyPositionSpeed) * flyPosition;
        transform.position = new Vector3(startPosition.x, startPosition.y + flyMovement, startPosition.z);

        if (Input.GetKey(KeyCode.D))
        {
            startPosition.x += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            startPosition.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            startPosition.z += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            startPosition.z -= moveSpeed * Time.deltaTime;
        }
    }
}