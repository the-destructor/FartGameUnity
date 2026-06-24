using Unity.Mathematics;
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
    public bool bob = false;
    public float test_divide = 100f;
    public float3 direction_test = new float3(0f, 0f, 1f);

    void Update()
    {
        if (targetSplineContainer == null || sourceSpriteShape == null)
            return;

        if (!Application.isPlaying && !updateInEditMode)
            return;

        SyncSplines();
    }

    public void SyncSplines()
    {
        if (bob == false)
        {
            UnityEngine.Splines.Spline targetSpline = targetSplineContainer.Spline;
            UnityEngine.U2D.Spline sourceSpline = sourceSpriteShape.spline;

            targetSpline.Clear();

            int count = sourceSpline.GetPointCount();

            /*/ --- FIRST PASS: Insert all points ---
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = sourceSpline.GetPosition(i);
                targetSpline.Insert(i, pos);
                print(i + "pos set");
            }
            */
            // --- SECOND PASS: Apply tangent mode + tangents ---
            for (int i = 0; i < count; i++)
            {
                //BezierKnot knot = sourceSpline[i];
                Vector3 knotUnfixed = sourceSpline.GetLeftTangent(i);
                sourceSpline.SetLeftTangent(i, new Vector3(knotUnfixed.x, knotUnfixed.y, 0f));
                Vector3 knotPos = sourceSpline.GetLeftTangent(i);

                print(knotPos);
                print(i);

                float knotLength = Mathf.Sqrt(Mathf.Pow(knotPos.x, 2f) + Mathf.Pow(knotPos.y, 2f));
                float angle = -Mathf.Atan2(knotPos.y, -knotPos.x);

                print(knotLength);


                Vector3 tangent = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * knotLength;

                targetSpline.Insert(i, new BezierKnot(sourceSpline.GetPosition(i), -tangent, tangent));

                /*print(i + "tangent in" + knot.TangentIn + "tangent out" + knot.TangentOut + "pos" + knot.Position);

                float3 dir3D = math.mul(knot.Rotation, direction_test); // "right" in local space

                // 2) Convert to Vector3 for Sprite stuff
                Vector3 rot = new Vector3(dir3D.x, dir3D.y, 0f);

                //ctor3 lt = new Vector3(, knot.TangentIn.y, 0f);
                // Vector3 rt = new Vector3( knot.TangentOut.y, 0f);

                Vector3 tain = new Vector3(knot.TangentIn.x, knot.TangentIn.y);
                Vector3 taout = new Vector3(knot.TangentOut.x, knot.TangentOut.y);

                // Vector3 tin = rot * tain;
                //Vector3 tout = rot * taout;

                targetSpline.SetTangentMode(i, ShapeTangentMode.Continuous);
                targetSpline.SetLeftTangent(i, tain);
                targetSpline.SetRightTangent(i, taout);
                print(i + "tangent set" + tain + taout);



                // 1. Get the position and tangent (direction) of the 3D spline at the given time
                sourceSpline.Evaluate(i, out float3 position, out float3 tangent, out float3 upVector);

                // 2. Convert the 3D tangent into a 2D direction
                Vector2 dir2D = new Vector2(tangent.x, tangent.y).normalized;

                // 3. Calculate the angle in degrees on the Z-axis
                float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;

                // 4. Apply to your sprite shape point (e.g., modifying the first point)
                // Sprite Shape angles are mapped from -180 to 180
                targetSpline.SetTangentMode(0, ShapeTangentMode.Continuous);
                targetSpline.SetRightTangent(0, dir2D * 0.5f); // Set handle length as needed

            */
            }

            // Match closed/open state
            //targetSpline.isOpenEnded = !sourceSpline.Closed;

            //targetSpriteShape.RefreshSpriteShape();
            bob = true;
        }
    }
}