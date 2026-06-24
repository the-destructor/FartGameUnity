using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Fart_Boost : MonoBehaviour
{
    public Camera camera1;
    public Rigidbody2D rb;
    public float FartPower;
    public float maxRotationSpeed = 45;
    public ParticleSystem regfart;
    public ParticleSystem ultrafart;
    public float ShiftCoyoteTime;
    private float ShiftCoyoteTimer = 0;
    public Slider FartMeter;
    public float InitialFartAmount = 10;
    private float FartAmount;
    public Slider background;
    private float DampVelocity = 0;
    private Vector3 spawn_position;
    public float slowdownspeed = 1f;
    public TrailRenderer trail;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FartMeter.maxValue = InitialFartAmount;
        FartAmount = InitialFartAmount;
        background.maxValue = InitialFartAmount;
        spawn_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (FartAmount > 10)
            FartAmount = 10;
        FartMeter.value = FartAmount;
        SliderValueSmoothAwesomeDropEffect();
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxRotationSpeed, maxRotationSpeed);
        CountDownShiftTime();

        if (Input.GetKeyDown(KeyCode.Space) && (ShiftCoyoteTimer <= 0) && FartAmount > 0)
        {
            rb.AddForce(MouseDirectionAsVector() * FartPower, ForceMode2D.Impulse);
            rb.AddTorque(MouseDirectionAsVector().x, ForceMode2D.Impulse);
            regfart.Play();
            background.value = FartAmount;
            FartAmount -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (ShiftCoyoteTimer > 0) && FartAmount > 0)
        {
            rb.AddForce(MouseDirectionAsVector() * FartPower * 10, ForceMode2D.Impulse);
            rb.AddTorque(MouseDirectionAsVector().x, ForceMode2D.Impulse);
            ultrafart.Play();
            background.value = FartAmount;
            FartAmount -= 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShiftCoyoteTimer = ShiftCoyoteTime;
        }
    }

    void SliderValueSmoothAwesomeDropEffect()
    {
        background.value = Mathf.SmoothDamp(background.value, FartAmount, ref DampVelocity, 0.2f);
    }

    Vector3 MouseDirectionAsVector()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos.z = 0;
        Vector3 mouse_direction = (mouse_pos - transform.position).normalized;
        return mouse_direction;
    }

    void CountDownShiftTime()
    {
        if (ShiftCoyoteTimer > 0)
        {
            ShiftCoyoteTimer -= Time.deltaTime;
        }
        else
        {
            ShiftCoyoteTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollectableFuel"))
        {
            FartAmount += 5f;
            background.value = FartAmount;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Kill"))
        {
            DeathFunction();
        }
        if (other.CompareTag("CollectableAutoFart"))
        {
            StartCoroutine(AutoFart(other));
        }
    }

    private void DeathFunction()
    {
        transform.position = spawn_position;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        FartMeter.maxValue = InitialFartAmount;
        FartAmount = InitialFartAmount;
        background.maxValue = InitialFartAmount;
    }

    private IEnumerator AutoFart(Collider2D other)
    {
        Destroy(other.gameObject);
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, slowdownspeed);
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, slowdownspeed);
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, 0f, slowdownspeed);
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 1f;
        rb.AddForce(MouseDirectionAsVector() * FartPower * 10, ForceMode2D.Impulse);
        rb.AddTorque(MouseDirectionAsVector().x, ForceMode2D.Impulse);
        ultrafart.Play();
    }
}
