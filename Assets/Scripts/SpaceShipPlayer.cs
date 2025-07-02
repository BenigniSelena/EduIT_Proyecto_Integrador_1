using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipPlayer : MonoBehaviour
{
    private float moveSpeed = 30f;
    private Vector3 playerMovement;
    private Vector3 initialPosition;

    [SerializeField] private int playerLives = 3;
    private int currentLives;

    private Rigidbody playerRigidBody;

    [SerializeField] private GameObject laserLeftPrefab;
    [SerializeField] private GameObject laserRightPrefab;

    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    [SerializeField] private Image leftLaserIcon;
    [SerializeField] private Image rightLaserIcon;
    [SerializeField] private Text leftLaserCooldownText;
    [SerializeField] private Text rightLaserCooldownText;

    [SerializeField] private Image firstLife;
    [SerializeField] private Image secondLife;
    [SerializeField] private Image thirdLife;

    private bool canShootLeft = true;
    private bool canShootRight = true;

    private void Start()
    {
        initialPosition = new Vector3(0f, 10f, -27.5f);
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.position = initialPosition;

        currentLives = playerLives;
        transform.position = initialPosition;
    }

    private void Update()
    {
        MovePlayer();
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
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + playerMovement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canShootLeft)
        {
            FireLaser(laserLeftPrefab, leftSpawn);
            canShootLeft = false;
            StartCoroutine(ReloadShot(() => canShootLeft = true, leftLaserIcon, leftLaserCooldownText));
        }

        if (Input.GetKey(KeyCode.Mouse1) && canShootRight)
        {
            FireLaser(laserRightPrefab, rightSpawn);
            canShootRight = false;
            StartCoroutine(ReloadShot(() => canShootRight = true, rightLaserIcon, rightLaserCooldownText));
        }
    }

    private void FireLaser(GameObject laserPrefab, Transform spawnPoint)
    {
        GameObject laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
        laser.SetActive(true);
    }

    private IEnumerator ReloadShot(System.Action onReloadComplete, Image laserIcon, Text cooldownText)
    {
        Color originalColor = laserIcon.color;
        Color cooldownColor = new Color32(0x5C, 0x5C, 0x5C, 0xFF);

        laserIcon.color = cooldownColor;
        cooldownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            cooldownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        laserIcon.color = originalColor;
        cooldownText.gameObject.SetActive(false);

        onReloadComplete?.Invoke();
    }

    private void LoseLife()
    {
        currentLives--;

        if (currentLives == 2)
        {
            Destroy(firstLife);
        }

        if (currentLives == 1)
        {
            Destroy(secondLife);
        }

        if (currentLives <= 0)
        {
            Destroy(gameObject);
            Destroy(thirdLife);
        }
        else
        {
            transform.position = initialPosition;
            playerRigidBody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Scene Plane"))
        {
            transform.position = initialPosition;
        }
        else
        {
            LoseLife();
        }
    }
}
