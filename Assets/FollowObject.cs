using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform subject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = subject.position;
    }
}
