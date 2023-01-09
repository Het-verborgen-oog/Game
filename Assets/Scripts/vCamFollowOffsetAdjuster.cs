using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class vCamFollowOffsetAdjuster : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    CinemachineTransposer vCamTransposer;
    [SerializeField] Vector3 newFollowOffset;
    [SerializeField] float lerpDuration = 1f;

    private void Start() {
        vCamTransposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Player") return;
        StartCoroutine(LerpFollowOffset(newFollowOffset, lerpDuration));
    }

    IEnumerator LerpFollowOffset(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = vCamTransposer.m_FollowOffset;
        while (time < duration)
        {
            vCamTransposer.m_FollowOffset = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        vCamTransposer.m_FollowOffset = targetPosition;
    }
}
