using UnityEngine;

[ExecuteAlways]
public class ParallaxObject : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform editorCameraTransform;

    [Header("Parallax Settings")]
    [Tooltip("0 = does not move with camera. 1 = moves exactly with camera.")]
    [SerializeField] private float parallaxFactor;

    private Vector3 lastCameraPosition;
    private Transform currentCamera;

    private void OnEnable()
    {
        currentCamera = GetCurrentCamera();

        if (currentCamera != null)
            lastCameraPosition = currentCamera.position;
    }

    private void OnStart()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Transform newCamera = GetCurrentCamera();

        if (newCamera == null)
            return;

        // Camera reference changed
        if (newCamera != currentCamera)
        {
            currentCamera = newCamera;
            lastCameraPosition = currentCamera.position;
            return;
        }

        Vector3 cameraDelta = currentCamera.position - lastCameraPosition;

        transform.position += new Vector3(
            cameraDelta.x * parallaxFactor,
            cameraDelta.y * parallaxFactor,
            0f
        );

        lastCameraPosition = currentCamera.position;
    }

    private Transform GetCurrentCamera()
    {
        if (Application.isPlaying)
        {
            if (cameraTransform != null)
                return cameraTransform;

            if (Camera.main != null)
                return Camera.main.transform;

            return null;
        }

        // Editor mode
        return editorCameraTransform;
    }
}