using Unity.VisualScripting;
using UnityEngine;

public class OpeningSequence : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform Top;
    public float finalTop;

    public Transform Bottom;
    

    public float finalBottom;

    private Vector3 initialTop;
    private Vector3 initialBottom;

    private float t = 0;

    public float speed = 1;
    void Start()
    {
        initialTop = Top.position;
        initialBottom = Bottom.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speed;
        Top.position = Vector3.Lerp(initialTop, new Vector3(initialTop.x, finalTop, 0), t);
        Bottom.position = Vector3.Lerp(initialBottom, new Vector3(initialBottom.x, finalBottom, 0), t);
    }
}
