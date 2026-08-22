using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class sliding_water : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startXadd = 0f;
    private float targetXadd = 7.9f;
    private float startX = 0f;
    private float targetX = 0f;
    public float speedX = 5f;
    public float speedY = 5f;
    public float startY = -4f;
    void Start()
    {
        startX = transform.position.x + startXadd;
        targetX = transform.position.x + targetXadd;
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetX, speedX * Time.deltaTime);

        // Apply it to the object
        float newY = startY + Mathf.Sin(Time.time * speedY) * 0.5f;
        transform.position = new Vector3(newX, newY, transform.position.z);

        if (transform.position.x == targetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }
}
