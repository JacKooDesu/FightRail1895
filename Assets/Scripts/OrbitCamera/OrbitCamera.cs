using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform focus = default;


    [SerializeField, Range(1f, 20f)]
    float distance = 5f;

    [SerializeField, Min(0f)]
    float focusRadius = 1f;

    [SerializeField, Range(0f, 1f)]
    float focusCentering = .5f;

    Vector3 focusPoint;

    // 預設面向
    Vector2 orbitAngles = new Vector2(45f, 0f);

    [SerializeField, Range(1f, 360f)]
    float rotationSpeed = 90f;   // 旋轉速度(每秒)

    private void Awake()
    {
        focusPoint = focus.position;
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(ManualRotation());
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = transform.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void UpdateFocusPoint()
    {
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
    IEnumerator ManualRotation()
    {
        while (Input.GetMouseButton(0))
        {
            Vector2 input = new Vector2(
            -Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X")
            );
            const float e = 0.001f;
            if (input.x < -e || input.x > e || input.y < -e || input.y > e)
            {
                orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            }
            yield return null;
        }

    }
}
