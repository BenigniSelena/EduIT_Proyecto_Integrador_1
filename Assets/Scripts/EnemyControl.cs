using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private float speed = 10f;
    [SerializeField] private GameObject destroyEffect;

    private bool isDestroyed = false;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        if (other.GetComponent<LaserBullets>())
        {
            isDestroyed = true;

            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
