using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;

public class AnimalHitController : MonoBehaviour
{
    [SerializeField] private MultibumpComponent bump;
    [SerializeField] private DetachOnSpawnBehavior spawner;
    [SerializeField] private RotationComponent rotter1;
    [SerializeField] private RotationComponent rotter2;
    [SerializeField] private RandomWalkComponent randWalk;
    [SerializeField] private RandomRotComponent randRot;
    [SerializeField] private GameObject bloodspatter1;
    [SerializeField] private GameObject bloodspatter2;
    [Space]
    [SerializeField] private float reqDist;
    [SerializeField] private float buckAngle = -10f;
    [SerializeField] private float buckDuration = .2f;
    [SerializeField] private float skidAngle = .45f;
    [SerializeField] private float skidDuration = .2f;
    [SerializeField] private float midpointDuration = .1f;
    [SerializeField] private float brakeDuration = 2.2f;
    [SerializeField] private float recoverDelay = .8f;
    [SerializeField] private float recoverDuration = .4f;
    [SerializeField] private float cooldownDelay = 2.5f;

    public IEnumerator HitRoutine()
    {
        var av = Global.Instance.Avatar;
        if (av.Speed < 22)
        {
            av.Speed = 22;
        }
        spawner.Spawn();
        while (Mathf.Abs(av.transform.position.z - spawner.disableChild.transform.position.z) > reqDist)
        {
            yield return null;
        }

        // start the THONK of the first impact
        spawner.disableChild.transform.SetParent(av.camera.transform, worldPositionStays:true);
        rotter1.enabled = true;
        bump.ContinuousMode = true;
        av.bump.enabled = false;
        randRot.enabled = true;
        randWalk.enabled = true;
        //av.IsDrivingAllowed = false;
        av.IsHardBraking = true;
        av.passengerArea.transform.DOBlendablePunchRotation(new Vector3(buckAngle, 0f, 0f), buckDuration).Play();
        av.passengerArea.transform.DOBlendableLocalRotateBy(new Vector3(0f, skidAngle, 0f), skidDuration).Play();
        bloodspatter1.SetActive(true);
        
        // redirect the critter off of the windshield
        spawner.disableChild.transform.SetParent(av.transform, worldPositionStays:true);
        rotter1.enabled = false;
        rotter2.enabled = true;
        yield return CoUtils.Wait(midpointDuration);
        bloodspatter2.SetActive(true);
        StartCoroutine(av.SmoothBrakeRoutine(brakeDuration));

        // recover from the impact
        yield return CoUtils.Wait(recoverDelay);
        randRot.enabled = false;
        randWalk.enabled = false;
        randWalk.transform.DOLocalMove(Vector3.zero, recoverDuration).Play();
        randRot.transform.DOLocalRotate(Vector3.zero, recoverDuration).Play();
        yield return CoUtils.Wait(cooldownDelay);
        av.IsHardBraking = false;
    }
}