using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullets : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifetime = 3f;

    private bool hasScored = false;

    private void OnEnable()
    {
        Invoke("DestroyBullet", lifetime);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasScored) return;

        if (other.GetComponentInParent<SpaceShipPlayer>() == null && !other.CompareTag("Player"))
        {
            if (other.GetComponentInParent<EnemyControl>())
            {
                GameManager.Instance.AddPoints(50);
                hasScored = true;
            }

            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        CancelInvoke("DestroyBullet");
    }

    private void OnDestroy()
    {
        CancelInvoke("DestroyBullet");
    }

    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
