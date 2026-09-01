using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using static UnityEngine.Rendering.DebugUI;
public class WinEffect : MonoBehaviour
{
    // Define the teleportation interval
    private float interval = 5.0f;

    public Rigidbody2D rb;

    void Start()
    {
        // Start the repeating timer loop
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(interval - 1f);

            // Generate a random X coordinate
            float randomX = UnityEngine.Random.Range(-16f, 18f);
            float randomAng = UnityEngine.Random.Range(-450f, 450f);

            // Update the object's position while keeping Y and Z the same
            transform.position = new Vector3(-100f, 55f, 10f);
            yield return new WaitForSeconds(1f);
            transform.position = new Vector3(randomX, 55f, transform.position.z);
            rb.linearVelocity = Vector2.down * 10f;
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = randomAng;
        }
    }
}
