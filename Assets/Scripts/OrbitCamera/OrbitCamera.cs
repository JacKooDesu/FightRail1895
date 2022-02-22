using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    Camera cam;
    Vector3 CameraHalfExtends
    {
        get
        {
            Vector3 halfExtends;
            halfExtends.y = cam.nearClipPlane * Mathf.Tan(.5f * Mathf.Deg2Rad * cam.fieldOfView);
            halfExtends.x = halfExtends.y * cam.aspect;
            halfExtends.z = 0f;
            return halfExtends;
        }
    }

    [SerializeField]
    Transform focus = default;
    public Transform Focus
    {
        set
        {
            focus = value;
        }
        get => focus;
    }
    public void SetFocus(Transform t)
    {
        focus = t;
    }


    [SerializeField, Range(1f, 50f)]
    float distance = 5f;
    public float Distance
    {
        set
        {
            distance = value;
        }
        get => distance;
    }

    [SerializeField, Min(0f)]
    float focusRadius = 1f;

    [SerializeField, Range(0f, 1f)]
    float focusCentering = .5f;

    Vector3 focusPoint, previousFocusPoint;

    // 預設面向
    Vector2 orbitAngles = new Vector2(45f, 0f);

    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;   // 旋轉速度(每秒)

    [SerializeField, Range(-89f, +89f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    [SerializeField, Min(0f)]
    float alignDelay = 5f;

    [SerializeField, Range(0f, 90f)]
    float alignSmoothRange = 45f;
    float lastManualRotationTime;

    [SerializeField]
    LayerMask mask = -1;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        Quaternion lookRotation;
        if (ManualRotation() || AutoMaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        Vector3 lookDirection = transform.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        Vector3 rectOffset = lookDirection * cam.nearClipPlane;
        Vector3 rectPosition = lookPosition + rectOffset;
        Vector3 castFrom = focus.position;
        Vector3 castLine = rectPosition - castFrom;
        float castDistance = castLine.magnitude;
        Vector3 castDirection = castLine / castDistance;


        if (
            Physics.BoxCast(
                castFrom,
                CameraHalfExtends,
                castDirection,
                out RaycastHit hit,
                lookRotation,
                castDistance,
                mask))
        {
            rectPosition = castFrom + castDirection * hit.distance;
            lookPosition = rectPosition - rectOffset;
        }
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > .01f && focusCentering > 0f)
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime); // use unscaledDeltaTime prevent game pause

            if (distance > focusRadius)
                t = Mathf.Min(t, focusRadius / distance);

            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }

    }

    // 使用滑鼠轉向
    bool ManualRotation()
    {

        Vector2 input = new Vector2(
        -Input.GetAxis("Mouse Y"),
        Input.GetAxis("Mouse X")
        );
        const float e = 0.001f;
        if (
            Input.GetMouseButton(1) &&
            (input.x < -e ||
            input.x > e ||
            input.y < -e ||
            input.y > e))
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledDeltaTime;

            return true;
        }
        return false;
    }

    bool AutoMaticRotation()
    {
        if (Time.unscaledDeltaTime - lastManualRotationTime < alignDelay)
        {
            return false;
        }

        Vector2 movement = new Vector2(
            focusPoint.x - previousFocusPoint.x,
            focusPoint.z - previousFocusPoint.z
        );

        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < .0001f)
            return false;

        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        float rotationChange = rotationSpeed * Mathf.Min(movementDeltaSqr, Time.unscaledDeltaTime);
        if (deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        }
        else if (180f - deltaAbs < alignSmoothRange)
        {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }
        orbitAngles.y = Mathf.MoveTowards(orbitAngles.y, headingAngle, rotationChange);

        return true;
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f)
            orbitAngles.y += 360f;
        else if (orbitAngles.y >= 360f)
            orbitAngles.y -= 360f;

    }

    static float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360 - angle : angle;
    }


}
