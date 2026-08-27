using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowPathSpline : MonoBehaviour
{

    public UnityEngine.Splines.Spline Path;
    public GameObject PathObject;
    public Transform PathPos;
    public float PathSpeed = 0.001f;
    private float st = 0f;
    void Update()
    {
        SplineContainer PathContainer = PathObject.GetComponent<SplineContainer>();
        Path = PathContainer.Spline;
        float totalTime = Path.GetLength() / (float)PathSpeed;
        float bouncedValue = Mathf.PingPong(Time.time / totalTime, 1f);

        st = Mathf.Clamp01(bouncedValue);
        print(st);

        Vector3 pos = Path.EvaluatePosition(st);
        transform.position = pos + PathPos.position;
    }
}
