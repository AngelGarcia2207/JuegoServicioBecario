using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    private float movimientoX;
    private float movimientoZ;
    private Vector3 inputJugador;
    private Vector3 direccionJugador;

    public CharacterController player;
    public float velocidad;
    public float gravedad = 9.8f;
    public float velocidadCaida;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimientoX = Input.GetAxis("Horizontal");
        movimientoZ = Input.GetAxis("Vertical");

        direccionarCamara();

        // ClampMagnitude impide que el movimiento en diagonal supere la velocidad máxima establecida
        inputJugador = Vector3.ClampMagnitude(new Vector3(movimientoX, 0, movimientoZ), 1);
        direccionJugador = inputJugador.x * camRight + inputJugador.z * camForward;
        direccionJugador = direccionJugador * velocidad;
        player.transform.LookAt(player.transform.position + direccionJugador);

        aplicarGravedad();

        inputsEspeciales();

        aplicarAnimacion(movimientoX, movimientoZ);

        player.Move(direccionJugador * Time.deltaTime);
    }

    void inputsEspeciales() {
        if (player.isGrounded && Input.GetButtonDown("Jump")) {
            velocidadCaida = jumpForce;
            direccionJugador.y = velocidadCaida;
        }
    }

    void direccionarCamara() {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void aplicarAnimacion(float movimientoX, float movimientoZ) {
        if (movimientoX != 0 || movimientoZ != 0) {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }

        if (player.isGrounded) {
            animator.SetBool("Jumping", false);
        }
        else {
            animator.SetBool("Jumping", true);
        }
    }

    void aplicarGravedad() {
        if (player.isGrounded) {
            velocidadCaida = -gravedad;
            direccionJugador.y = velocidadCaida;
        }
        else {
            velocidadCaida -= gravedad * Time.deltaTime;
            direccionJugador.y = velocidadCaida;
        }
    }
}