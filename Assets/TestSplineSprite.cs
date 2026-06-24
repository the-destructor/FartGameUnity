using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UIElements;
[ExecuteInEditMode]
public class TestSplineSprite : MonoBehaviour
{
    public SpriteShapeController targetSpriteShape;

    public bool updateInEditMode = true;
    public bool john = false;
    public float test_divide = 100f;
    public float3 direction_test = new float3(0f, 0f, 1f);
    public int pointID = 0;
    public Vector3 RTTest = Vector3.zero;

    void Update()
    {
        if (!Application.isPlaying && !updateInEditMode)
            return;

        SyncaSplines();
    }

    public void SyncaSplines()
    {
        if (john == false)
        {
            UnityEngine.U2D.Spline targetSpline = targetSpriteShape.spline;

            targetSpline.SetTangentMode(pointID, ShapeTangentMode.Continuous);
            targetSpline.SetRightTangent(pointID, RTTest);


            targetSpriteShape.RefreshSpriteShape();
            john = true;
        }
    }
}