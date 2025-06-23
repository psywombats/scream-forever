using UnityEngine;

public class RandomRotComponent : MonoBehaviour {

    [SerializeField] private Vector3 veloMax;
    [SerializeField] private Vector3 veloGain;

    private Vector3 Velocity;

    public void Update() {
        Velocity += new Vector3(
            veloGain.x * Time.deltaTime * Random.Range(-1f, 1f),
            veloGain.y * Time.deltaTime * Random.Range(-1f, 1f),
            veloGain.z * Time.deltaTime * Random.Range(-1f, 1f));
        Velocity.x = Mathf.Clamp(Velocity.x, -veloMax.x, veloMax.x);
        Velocity.y = Mathf.Clamp(Velocity.y, -veloMax.y, veloMax.y);
        Velocity.z = Mathf.Clamp(Velocity.z, -veloMax.z, veloMax.z);
        var eulers = transform.localRotation.eulerAngles + Velocity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(eulers);
    }
}