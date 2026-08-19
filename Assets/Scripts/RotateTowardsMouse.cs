using Unity.VisualScripting;
using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{

    public float Offset;
    public ParticleSystem ps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(MouseDirectionAsVector().y, MouseDirectionAsVector().x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle+Offset);
        
    }

    Vector2 MouseDirectionAsVector()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouse_direction = (mouse_pos - transform.position);
        return mouse_direction;
    }
}
