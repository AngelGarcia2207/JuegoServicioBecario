using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    public Vector3 offset;
    private Transform target;
    [Range (0,1)] public float lerpValue;
    public float sensibilidad;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Jugador").transform; // Usar el nombre del objeto jugador dentro de Unity
    }

    // LateUpdate ejecuta su código después del Update()
    void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up) * offset;

        transform.LookAt(target);
    }
}
