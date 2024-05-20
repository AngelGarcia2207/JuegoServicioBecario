using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rio : MonoBehaviour
{
    [SerializeField] GameObject respawnpoint1;
    [SerializeField] GameObject respawnpoint2;
    [SerializeField] Animator transicion;

    private GameObject playerObject;
    private ControladorJugador playerScript;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Pisando");
            StartCoroutine(teleport());
            transicion.SetTrigger("Fade");
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
        yield return new WaitForSeconds(0.1f);
        playerScript.inmovilizado = false;
    }
}
