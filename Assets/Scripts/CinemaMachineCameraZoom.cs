using Unity.Cinemachine;
using UnityEngine;

public class CinemaMachineCameraZoom : MonoBehaviour
{

    public Rigidbody2D rb;
    public CinemachineCamera vcam;
    public float minSpeed;
    public float maxSpeed;
    public float minZoom;
    public float maxZoom;
    public float zoomSmoothTime;
    private float currentZoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentZoom = vcam.Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float speedValue = Mathf.InverseLerp(minSpeed, maxSpeed, rb.linearVelocity.magnitude);
        float zoomValue = Mathf.Lerp(minZoom, maxZoom, speedValue);

        currentZoom = Mathf.Lerp(currentZoom, zoomValue, Time.deltaTime * zoomSmoothTime);

        vcam.Lens.OrthographicSize = currentZoom;

    }
}

