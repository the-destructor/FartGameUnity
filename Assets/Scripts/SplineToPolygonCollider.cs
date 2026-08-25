using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class SplineToPolygonCollider : MonoBehaviour
{
    public PolygonCollider2D polyCollider;
    public SpriteShapeController sourceSpriteShape;

    [ContextMenu("Sync Splines Now")]
    public void SyncSplines()
    {
        if (polyCollider == null || sourceSpriteShape == null) return;

        Vector2[] points = polyCollider.GetPath(0);
        UnityEngine.U2D.Spline spline = sourceSpriteShape.spline;
        if (spline == null) return;

        spline.Clear();

        foreach (Vector2 point in points)
        {
            try
            {
                spline.InsertPointAt(spline.GetPointCount(), point);
                sourceSpriteShape.spline.SetHeight(spline.GetPointCount() - 1, 1f);
                spline.SetTangentMode(spline.GetPointCount() - 1, ShapeTangentMode.Linear);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Caught an exception in the SplineToPolygonCollider script: {ex.Message}");
            }
        }

        // Refresh the sprite shape controller
        sourceSpriteShape.RefreshSpriteShape();
    }
}