using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IdleZoom : MonoBehaviour, IIdleAction
{
    [SerializeField]
    private Vector3 zoomPosition;
    [SerializeField]
    private float transitionTime = 1f;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private bool zoomedIn;
    private Vector3 defaultPosition;

    private void Start() {
        defaultPosition = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    public void SwitchIdleState(bool isIdle)
    {
        

        if (isIdle && !zoomedIn)
        {
            StartCoroutine(MoveCamera(zoomPosition, transitionTime));
        }

        if (!isIdle && zoomedIn)
        {
            StartCoroutine(MoveCamera(defaultPosition, transitionTime));
        }

        zoomedIn = isIdle;
    }

    private IEnumerator MoveCamera(Vector3 targetPosition, float transitionTime)
    {
        CinemachineTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 startPosition = transposer.m_FollowOffset;


        float progression = 0;
        while(progression < 1)
        {
            Vector3 lerpVector = Vector3.Lerp(startPosition, targetPosition, progression);
            progression += Mathf.Min((Time.deltaTime / transitionTime), 1);
            
            transposer.m_FollowOffset = lerpVector;
            Debug.Log(progression + " | " + lerpVector);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}
