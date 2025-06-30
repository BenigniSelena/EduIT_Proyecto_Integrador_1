using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;

    private Vector3 playerMovement;
    [Space]
    [SerializeField] private Text myText;
    [SerializeField] private GameObject laserShots;
    [SerializeField] private int shotsFired = 0;
    [SerializeField] private Transform laserSpawn;

    private void Update()
    {
        MovePlayer();
        if (shotsFired >= 1)
        {
            UpdateUI();
        }
        Shoot();
    }

    private void MovePlayer()
    {
        playerMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            playerMovement.x += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerMovement.x -= 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            playerMovement.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerMovement.z -= 1;
        }

        Vector3 currentPosition = transform.position;
        currentPosition += playerMovement.normalized * moveSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }

    private void UpdateUI()
    {
        myText.text = "Disparos: " + shotsFired.ToString();
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject laser = Instantiate(laserShots, laserSpawn.position, Quaternion.identity);
            laser.SetActive(true);

            shotsFired += 1;
        }
    }
} 