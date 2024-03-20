using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    private Transform target;
    private new Camera camera;

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
    }


    void Update() {
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
            if (Physics.Raycast(target.position, direction, out hit, maxDistance)) {
                distance = (hit.point - target.position).magnitude;
            }

            transform.position = target.position + direction * distance;
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        }
    }

    private void CalculateNearPlaneSize() {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

    }
}
