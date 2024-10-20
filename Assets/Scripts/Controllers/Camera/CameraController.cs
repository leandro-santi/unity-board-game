using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotationSpeed;

    private int _currentTargetIndex;

    private void Start()
    {
        _currentTargetIndex = 0;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = targets[_currentTargetIndex].position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Vector3 lookAtPosition = targets[_currentTargetIndex].position;

        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SwitchTarget()
    {
        _currentTargetIndex = (_currentTargetIndex + 1) % targets.Length;

        offset.z *= -1;
    }
}