using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    private Transform target;
    private new Camera camera;
    private Vector2 nearPlaneSize;

    private Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    public bool inmovilizado = false;
    public float maxDistance;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Jugador").transform; // Usar el nombre del objeto jugador dentro de Unity
        
        camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

        CalculateNearPlaneSize();
    }


    void Update() {
        if (!GameManager.Instance.isPaused) {
            float horizontal = Input.GetAxis("Mouse X");

            if (horizontal != 0) {
                angle.x += horizontal * Mathf.Deg2Rad * sensitivity.x;
            }

            float vertical = Input.GetAxis("Mouse Y");

            if (vertical != 0) {
                angle.y += vertical * Mathf.Deg2Rad * sensitivity.y;
                angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
            }
        }
    }

    // LateUpdate ejecuta su código después del Update()
    void LateUpdate() {
        if (!inmovilizado) {
            Vector3 direction = new Vector3(
                Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
                -Mathf.Sin(angle.y),
                -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
            );

            RaycastHit hit;
            float distance = maxDistance;
            Vector3[] cameraPoints = GetCameraCollisionPoints(direction);

            foreach (Vector3 point in cameraPoints) {
                if (Physics.Raycast(point, direction, out hit, maxDistance)) {
                    distance = Mathf.Min((hit.point - target.position).magnitude, distance);
                }
            }

            transform.position = target.position + direction * distance;
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
    }

    private void CalculateNearPlaneSize() {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        nearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direction) {
        Vector3 position = target.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.1f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[] {
            center - right + up,
            center + right + up,
            center - right - up,
            center + right - up
        };
    }
}
