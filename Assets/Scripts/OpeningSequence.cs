using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject Canvas;
    public Slider slider;


    void Start()
    {
        Canvas.SetActive(true);
        targetTop = new Vector3(Top.position.x, finalTop, 0);
        targetBottom = new Vector3(Bottom.position.x, finalTop, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (PersistentObject.CompletedLoading)
        {
            Canvas.SetActive(false);
            Top.position = Vector3.SmoothDamp(Top.position, targetTop, ref topV, smoothTime);
            Bottom.position = Vector3.SmoothDamp(Bottom.position, targetBottom, ref bottomV, smoothTime);
        }
        else
        {
            Canvas.SetActive(true);
            slider.maxValue = PersistentObject.FileCount;
            slider.value = PersistentObject.FilesLoaded;
        }
    }
}
