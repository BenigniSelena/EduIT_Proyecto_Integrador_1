using UnityEngine;

public class Autodestruction : MonoBehaviour
{
    private float seconds = 3f;

    private void Start()
    {
        Destroy(gameObject, seconds);
    }
}
