using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform subject;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_pos = new Vector3(subject.position.x, subject.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, new_pos, ref velocity, 0.2f);
    }
}
