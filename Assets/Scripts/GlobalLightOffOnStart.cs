using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightOffOnStart : MonoBehaviour
{
    public Light2D Light;
    public float Intensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Light.intensity = Intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
