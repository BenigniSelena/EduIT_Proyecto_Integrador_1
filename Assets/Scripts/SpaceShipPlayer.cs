using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    private Vector3 playerMovement;

    [Space]
    [SerializeField] private Text myText;
    [SerializeField] private GameObject laserLeftPrefab;
    [SerializeField] private GameObject laserRightPrefab;

    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    [SerializeField] private Image leftLaserIcon;
    [SerializeField] private Image rightLaserIcon;
    [SerializeField] private Text leftLaserCooldownText;
    [SerializeField] private Text rightLaserCooldownText;

    private bool canShootLeft = true;
    private bool canShootRight = true;

    private int shotsFired = 0;

    private void Update()
    {
        MovePlayer();
        Shoot();

        if (shotsFired >= 1)
        {
            UpdateUI();
        }
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
            if (canShootLeft)
            {
                FireLaser(laserLeftPrefab, leftSpawn);
                canShootLeft = false;
                StartCoroutine(ReloadShot(() => canShootLeft = true, leftLaserIcon, leftLaserCooldownText));
            }
            else if (canShootRight)
            {
                FireLaser(laserRightPrefab, rightSpawn);
                canShootRight = false;
                StartCoroutine(ReloadShot(() => canShootRight = true, rightLaserIcon, rightLaserCooldownText));
            }
        }
    }

    private void FireLaser(GameObject laserPrefab, Transform spawnPoint)
    {
        GameObject laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
        laser.SetActive(true);
        shotsFired += 1;
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
}
