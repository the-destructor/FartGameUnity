using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Rendering.Universal;
public class FlickeringLight : MonoBehaviour
{

    public float centreIntensity = 0.7f;
    public float speed = 0.5f;
    public float amplitude = 0.3f;
    public Light2D light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.intensity = centreIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        float newIntensity = centreIntensity + Mathf.Sin(Time.time * speed) * amplitude;
        light.intensity = newIntensity;
    }
}
