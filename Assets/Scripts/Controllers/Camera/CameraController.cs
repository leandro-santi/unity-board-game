using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;
    private int _currentTargetIndex = 0;
    private float _switchInterval = 20f; // Debug variable
    private float _timer = 0f;
    private float _rotationSpeed = 2f;

    void LateUpdate()
    {
        if (targets.Length == 0)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _switchInterval)
        {
            SwitchTarget();
            _timer = 0f;
        }

        Vector3 desiredPosition = targets[_currentTargetIndex].position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Vector3 lookAtPosition = targets[_currentTargetIndex].position;

        Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void SwitchTarget()
    {
        _currentTargetIndex = (_currentTargetIndex + 1) % targets.Length;

        offset.z *= -1;
    }
}