using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private float speed = 10f;
    private float autodestructionTime = 3f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);

            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Scene Plane"))
        {
            Destroy(gameObject, autodestructionTime);
        }
    }
}
