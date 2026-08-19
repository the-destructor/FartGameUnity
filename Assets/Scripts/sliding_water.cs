using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class sliding_water : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startX = 0f;
    private float targetX = 7.9f;
    public float speedX = 5f;
    public float speedY = 5f;
    void Start()
    {
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.MoveTowards(transform.position.x, targetX, speedX * Time.deltaTime);

        // Apply it to the object
        float newY = -4 + Mathf.Sin(Time.time * speedY) * 0.5f;
        transform.position = new Vector3(newX, newY, transform.position.z);

        if (transform.position.x == targetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }
}
