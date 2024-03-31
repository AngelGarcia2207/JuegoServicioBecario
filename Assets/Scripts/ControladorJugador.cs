using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControladorJugador : MonoBehaviour
{
    private float movimientoX;
    private float movimientoZ;
    private Vector3 inputJugador;
    private Vector3 direccionJugador;
    public bool inmovilizado = false;

    public CharacterController player;
    public float velocidad;
    public float gravedad = 9.8f;
    public float velocidadCaida;
    public float jumpForce;
    public float floorRaycastDistance;

    public float ledgeRaycastHeight;
    public float ledgeRaycastDistance;
    public float ledgeClimbingTime;
    private float ledgeClimbingRemainingTime;
    private bool climbingDelay = false;
    public float climbingHorizontalDisplacement;
    public float climbingVerticalDisplacement;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inmovilizado && !climbingDelay) {
            movimientoX = Input.GetAxis("Horizontal");
            movimientoZ = Input.GetAxis("Vertical");
        }
        else {
            movimientoX = 0;
            movimientoZ = 0;
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }

        direccionarCamara();

        // ClampMagnitude impide que el movimiento en diagonal supere la velocidad máxima establecida
        inputJugador = Vector3.ClampMagnitude(new Vector3(movimientoX, 0, movimientoZ), 1);
        direccionJugador = inputJugador.x * camRight + inputJugador.z * camForward;
        direccionJugador = direccionJugador * velocidad;
        player.transform.LookAt(player.transform.position + direccionJugador);

        aplicarGravedad();

        inputsEspeciales();

        if (!climbingDelay) {
            climbingDelay = raycastLedge();
            if (climbingDelay) {
                ledgeClimbingRemainingTime = ledgeClimbingTime;
                animator.SetBool("Climbing", true);
            }
        }
        else {
            ledgeClimbingRemainingTime -= Time.deltaTime;
            if (ledgeClimbingRemainingTime <= 0f) {
                ledgeClimb();
                climbingDelay = false;
                animator.SetBool("Climbing", false);
            }
        }

        aplicarAnimacion(movimientoX, movimientoZ);

        player.Move(direccionJugador * Time.deltaTime);
    }

    void inputsEspeciales() {
        if ((player.isGrounded || raycastFloor()) && Input.GetButtonDown("Jump")) {
            velocidadCaida = jumpForce;
            direccionJugador.y = velocidadCaida;
        }

        if (player.isGrounded && Input.GetKey("g")) {
            animator.SetBool("Dancing", true);
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
            animator.SetBool("Dancing", false);
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
            animator.SetBool("Dancing", false);
        }
    }

    void aplicarGravedad() {
        if (!climbingDelay) {
            if (player.isGrounded) {
                velocidadCaida = -gravedad * 0.1f;
                direccionJugador.y = velocidadCaida;
            }
            else {
                velocidadCaida -= gravedad * Time.deltaTime;
                direccionJugador.y = velocidadCaida;
            }
        }
        else {
            velocidadCaida = 0;
        }
    }

    bool raycastFloor() {
        Vector3 origin = transform.position;
        RaycastHit hit;
        Vector3 direction = -transform.up;

        if (Physics.Raycast(origin, direction, out hit, floorRaycastDistance)) {
            return true;
        }
        else {
            return false;
        }
    }

    bool raycastLedge() {
        Vector3 origin = transform.position + new Vector3(0,ledgeRaycastHeight,0);
        RaycastHit hit;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out hit, ledgeRaycastDistance)) {
            if (hit.collider.CompareTag("Ledge")) {
                Debug.DrawRay(origin, direction * ledgeRaycastDistance, Color.white);
                return true;
            }
        }
        else {
            Debug.DrawRay(origin, direction * ledgeRaycastDistance, Color.white);
        }
        
        return false;
    }

    void ledgeClimb() {
        Vector3 horizontalDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 horizontalDisplacement = horizontalDirection * climbingHorizontalDisplacement;

        Vector3 verticalDisplacement = new Vector3(0f, climbingVerticalDisplacement, 0f);

        Vector3 displacement = horizontalDisplacement + verticalDisplacement;

        transform.position = transform.position + displacement;
    }
}