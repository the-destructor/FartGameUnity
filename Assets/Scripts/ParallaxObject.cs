using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    
    [Header("Parallax Settings")]
    [Tooltip("0 = moves with camera (infinite distance). 1 = stays locked to world.")]
    [SerializeField] private float parallaxFactor;

    private Vector3 lastCameraPosition;

    void Start()
    {
        // Fallback to Main Camera if none is assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // Calculate how far the camera moved this frame
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;
        
        // Move the object by a fraction of the camera's movement
        transform.position += new Vector3(cameraDelta.x * parallaxFactor, cameraDelta.y * parallaxFactor, 0);
        
        // Save camera position for the next frame
        lastCameraPosition = cameraTransform.position;
    }
}