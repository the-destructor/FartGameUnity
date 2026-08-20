using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Fart_Boost : MonoBehaviour
{
    public Camera camera1;
    public BoxCollider2D bcollider;
    public Rigidbody2D rb;
    public float FartPower;
    public float maxRotationSpeed = 45;
    public ParticleSystem regfart;
    public ParticleSystem ultrafart;
    public float ShiftCoyoteTime;
    private float ShiftCoyoteTimer = 0;
    public UnityEngine.UI.Slider FartMeter;
    public float InitialFartAmount = 10;
    private float FartAmount;
    public UnityEngine.UI.Slider background;
    private float DampVelocity = 0;
    private Vector3 spawn_position;
    public float slowdownspeed = 1f;
    public TrailRenderer trail;
    public UnityEngine.Splines.Spline CurrentPath;
    public Transform PathPos;
    public float PathSpeed = 0.001f;
    private float st = 0f;
    private float bt = 1f;
    private bool OnPipe = false;
    private bool Direction = true;
    public GameObject PipeOverlay;
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
        SplineUpdate(CurrentPath, PathPos, Direction);
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

        TogglePipeOverlay();

    }

    void TogglePipeOverlay()
    {
        PipeOverlay.SetActive(OnPipe);

    }

    void SplineUpdate(UnityEngine.Splines.Spline spline, Transform pipePos, bool Direction)
    {
        if (spline == null || pipePos == null) 
        {
            st = 0;
            bt = 0;
            bcollider.isTrigger = false;
            return;
        }
        OnPipe = true;
        bcollider.isTrigger = true;
        float end_value;
        // Move forward along spline
        if (Direction)
        {
            st += PathSpeed * Time.deltaTime / spline.GetLength();
            end_value = 1f;
        }
        else
        {
            bt -= PathSpeed * Time.deltaTime / spline.GetLength();
            st = 1 + bt;
            end_value = 0f;
        }
        st = Mathf.Clamp01(st);

        // --- Position ---
        Vector3 pos = spline.EvaluatePosition(st);
        print(pos);
        transform.position = pos + pipePos.position;
        print("onspline");
        if (st == end_value)
        {
            float3 tan = spline.EvaluateTangent(end_value);
            float offset;
            if(Direction)
            {
                offset = 1;
            }
            else
            {
                offset = -1;
            }
            Vector3 tangent = new Vector3(offset * tan.x, offset * tan.y, tan.z).normalized;
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(tangent * 10, ForceMode2D.Impulse);
            CurrentPath = null;
            PathPos = null;
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
        if (other.CompareTag("PipeStart"))
        {
            if (!OnPipe)
            {
                SplineContainer spline = other.GetComponentInParent<SplineContainer>();
                CurrentPath = spline.Spline;
                PathPos = other.transform.parent;
                Direction = true;
            }
        }
        if (other.CompareTag("PipeEnd"))
        {
            if (!OnPipe)
            {
                SplineContainer spline = other.GetComponentInParent<SplineContainer>();
                CurrentPath = spline.Spline;
                PathPos = other.transform.parent;
                Direction = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PipeStart"))
        {
            OnPipe = false;
        }   
        if (other.CompareTag("PipeEnd"))
        {
            OnPipe = false;
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
