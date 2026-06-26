using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class CopySpriteShapeSpline : MonoBehaviour
{
    public SplineContainer targetSplineContainer;
    public SpriteShapeController sourceSpriteShape;

    public bool updateInEditMode = true;
    public bool RecalculateCompleted = false;
    public float test_divide = 100f;
    public float3 direction_test = new float3(0f, 0f, 1f);

    public GameObject PipeStartObj;
    public GameObject PipeEndObj;

    void Start()
    {
        if (targetSplineContainer == null || sourceSpriteShape == null)
            return;

        if (!Application.isPlaying && !updateInEditMode)
            return;
        RecalculateCompleted = false;
        SyncSplines();
        
    }
    void Update()
    {
        if (targetSplineContainer == null || sourceSpriteShape == null)
            return;

        if (!Application.isPlaying && !updateInEditMode)
            return;
        if (RecalculateCompleted == false)
        {
            SyncSplines();
        }
    }

    public void SyncSplines()
    {
        if (RecalculateCompleted == false)
        {
            UnityEngine.Splines.Spline targetSpline = targetSplineContainer.Spline;
            UnityEngine.U2D.Spline sourceSpline = sourceSpriteShape.spline;

            targetSpline.Clear();

            int count = sourceSpline.GetPointCount();

            for (int i = 0; i < count; i++)
            {
                PipeEndObj.transform.position = sourceSpline.GetPosition(count-1) + transform.position;
                PipeStartObj.transform.position = sourceSpline.GetPosition(0) + transform.position;

                Vector3 knotUnfixedR = Vector3.zero;
                Vector3 knotPosR = Vector3.zero;
                Vector3 knotUnfixedL = Vector3.zero;
                Vector3 knotPosL = Vector3.zero;

                knotUnfixedR = sourceSpline.GetRightTangent(i);
                sourceSpline.SetRightTangent(i, new Vector3(knotUnfixedR.x, knotUnfixedR.y, 0f));
                knotPosR = sourceSpline.GetRightTangent(i);


                knotUnfixedL = sourceSpline.GetLeftTangent(i);
                sourceSpline.SetLeftTangent(i, new Vector3(knotUnfixedL.x, knotUnfixedL.y, 0f));
                knotPosL = sourceSpline.GetLeftTangent(i);



                float knotLengthL = Mathf.Sqrt(Mathf.Pow(knotPosL.x, 2f) + Mathf.Pow(knotPosL.y, 2f));
                float knotLengthR = Mathf.Sqrt(Mathf.Pow(knotPosR.x, 2f) + Mathf.Pow(knotPosR.y, 2f));
                float angleL = Mathf.Atan2(knotPosL.y, knotPosL.x);
                float angleR = Mathf.Atan2(knotPosR.y, knotPosR.x);



                Vector3 tangentL = new Vector3(Mathf.Cos(angleL), Mathf.Sin(angleL), 0f) * knotLengthL;
                Vector3 tangentR = new Vector3(Mathf.Cos(angleR), Mathf.Sin(angleR), 0f) * knotLengthR;

                targetSpline.Insert(i, new BezierKnot(sourceSpline.GetPosition(i), tangentL, tangentR));

                
            }
            RecalculateCompleted = true;
        }
    }
}