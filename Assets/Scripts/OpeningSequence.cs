using Unity.VisualScripting;
using UnityEngine;

public class OpeningSequence : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform Top;

    public float finalTop;


    public Transform Bottom;

    public float finalBottom;


    private Vector3 targetTop;
    private Vector3 targetBottom;

    private Vector3 topV = Vector3.zero;
    private Vector3 bottomV = Vector3.zero;

    public float smoothTime = 0.3f;


    void Start()
    {
        targetTop = new Vector3(Top.position.x, finalTop, 0);
        targetBottom = new Vector3(Bottom.position.x, finalTop, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Top.position = Vector3.SmoothDamp(Top.position, targetTop, ref topV, smoothTime);
        Bottom.position = Vector3.SmoothDamp(Bottom.position, targetBottom, ref bottomV, smoothTime);
    }
}
