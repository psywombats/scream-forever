using UnityEngine;

public class CarAutodriver : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed = 60;

    public void Update()
    {
        body.velocity = transform.forward * speed;
    }
}