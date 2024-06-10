using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibroFinal2 : MonoBehaviour
{
    [SerializeField] private Animator transicion;
    [SerializeField] private Animator player;
    [SerializeField] private Animator libro;
    [SerializeField] private GameObject panelProgreso;
    [SerializeField] private GameObject panelVictoria;

    private ControladorJugador playerScript;
    private SFXManager playerSFX;
    private Camera mainCamera;
    private CamaraTerceraPersona cameraScript;
    private bool collided;

    void Start()
    {
        libro.speed = 1f;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorJugador>();
        playerSFX = GameObject.FindGameObjectWithTag("Player").GetComponent<SFXManager>();
        mainCamera = Camera.main;
        cameraScript = mainCamera.GetComponent<CamaraTerceraPersona>();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collided = true;
            playerScript.inmovilizado = true;
            cameraScript.inmovilizado = true;
            player.speed = 0f;
            libro.speed = 0f;
            panelProgreso.SetActive(false);
            transicion.speed = 0.5f;
            playerSFX.PlayWin();
            StartCoroutine("fin");
            transicion.SetTrigger("Fade");
        }
    }

    IEnumerator fin(){
        yield return new WaitForSeconds(4f);
        panelVictoria.SetActive(true);
    }
}
