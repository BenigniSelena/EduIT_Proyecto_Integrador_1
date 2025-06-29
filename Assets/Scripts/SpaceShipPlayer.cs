using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 35f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = startPosition.x;
        currentPosition.z = startPosition.z;
        transform.position = currentPosition;

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