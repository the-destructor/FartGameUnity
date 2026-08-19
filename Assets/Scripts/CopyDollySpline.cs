using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class CopyDollySpline : MonoBehaviour
{
    public SplineContainer sourceSplineContainer;
    public SpriteShapeController targetSpriteShape;

    public bool updateInEditMode = true;
    public bool bob = false;
    public float test_divide = 100f;
    public float3 direction_test = new float3 (0f, 0f, 1f);

    void Update()
    {
        if (sourceSplineContainer == null || targetSpriteShape == null)
            return;

        if (!Application.isPlaying && !updateInEditMode)
            return;

        SyncSplines();
    }

    public void SyncSplines()
    {
        if (bob == false)
        {
            UnityEngine.Splines.Spline sourceSpline = sourceSplineContainer.Spline;
            UnityEngine.U2D.Spline targetSpline = targetSpriteShape.spline;

            targetSpline.Clear();

            int count = sourceSpline.Count;

            // --- FIRST PASS: Insert all points ---
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = sourceSpline[i].Position;
                targetSpline.InsertPointAt(i, pos);
                print(i + "pos set");
            }

            // --- SECOND PASS: Apply tangent mode + tangents ---
            for (int i = 0; i < count; i++)
            {
                BezierKnot knot = sourceSpline[i];
                print(i + "tangent in" + knot.TangentIn + "tangent out" + knot.TangentOut + "pos" + knot.Position);

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

            }

            // Match closed/open state
            targetSpline.isOpenEnded = !sourceSpline.Closed;

            targetSpriteShape.RefreshSpriteShape();
            bob = true;
        }
    }
}