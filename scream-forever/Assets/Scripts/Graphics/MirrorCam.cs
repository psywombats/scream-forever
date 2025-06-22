using UnityEngine;

public class MirrorCam : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private Camera mirrorCam;
    [SerializeField] private Transform mirrorNormalPoint;

    public void Update()
    {
        var incomingVec = mirrorCam.transform.position - playerCam.transform.position;
        var outVec = Vector3.Reflect(incomingVec, mirrorNormalPoint.transform.position - mirrorCam.transform.position);
        mirrorCam.transform.LookAt(mirrorCam.transform.position - outVec);
    }
}
