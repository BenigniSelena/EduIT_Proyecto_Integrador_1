using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullets : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float lifetime = 3f;

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
        if (other.GetComponentInParent<SpaceShipPlayer>() == null && !other.CompareTag("Player"))
        {
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
