using UnityEngine;

public class MirrorCam : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera mirrorCam;
    [SerializeField] private Transform mirrorNormalPoint;
    [SerializeField] private Transform outPoint;

    public void Update()
    {
        var incomingVec =  playerCam.transform.position - mirrorCam.transform.position;
        var normal = (mirrorNormalPoint.transform.position - mirrorCam.transform.position).normalized;
        var outVec = Vector3.Reflect(incomingVec, normal);
        outPoint.transform.position = mirrorCam.transform.position - outVec;
        mirrorCam.transform.LookAt(outPoint);
    }
}
