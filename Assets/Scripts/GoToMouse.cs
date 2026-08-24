using UnityEngine;

public class GoToMouse : MonoBehaviour
{
    public Transform targetPoint; // The fixed point
    public float maxDistance = 2f; // Maximum distance allowed

    void Update()
    {
        // 1. Get mouse position in world coordinates
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = -Camera.main.transform.position.z; // Align with camera depth if 2D/3D
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        // For a pure 2D game in the XY plane, use this instead:
        // Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouseWorld.z = 0f;

        // 2. Find direction and distance vector from point to mouse
        Vector3 direction = mouseWorld - targetPoint.position;

        // 3. Limit the vector length to a max of 2 units
        Vector3 clampedOffset = Vector3.ClampMagnitude(direction, maxDistance);

        // 4. Set the position
        transform.position = targetPoint.position + clampedOffset;
    }
}