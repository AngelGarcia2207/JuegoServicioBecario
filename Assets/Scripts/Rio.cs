using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rio : MonoBehaviour
{
    [SerializeField] GameObject respawnpoint1;
    [SerializeField] GameObject respawnpoint2;
    [SerializeField] Animator transicion;
    [SerializeField] SFXManager playerSFX;
    [SerializeField] GameObject plane2;

    private GameObject playerObject;
    private ControladorJugador playerScript;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) { 
            StartCoroutine(teleport());
            plane2.SetActive(true);
            transicion.SetTrigger("Fade");
            playerSFX.PlaySplash();
        }
    }

    IEnumerator teleport() {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<ControladorJugador>();

        playerScript.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.respawnpoint == 1) {
            playerObject.transform.position = respawnpoint1.transform.position;
        }
        else if (GameManager.Instance.respawnpoint == 2) {
            playerObject.transform.position = respawnpoint2.transform.position;
        }
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        plane2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerScript.inmovilizado = false;
    }
}
