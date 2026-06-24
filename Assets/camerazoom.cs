using UnityEngine;

public class camerazoom : MonoBehaviour
{

    public Rigidbody2D rb;
    public Camera cam;
    public float minSpeed;
    public float maxSpeed;
    public float minZoom;
    public float maxZoom;
    public float zoomSmoothTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedValue = Mathf.InverseLerp(minSpeed, maxSpeed, rb.linearVelocity.magnitude);
        float zoomValue = Mathf.Lerp(minZoom, maxZoom, speedValue);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomValue, Time.deltaTime * zoomSmoothTime);
    }
}

