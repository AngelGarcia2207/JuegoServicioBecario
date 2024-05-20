using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCueva : MonoBehaviour
{
    [SerializeField] ControladorJugador controlador;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator transicion;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Teleporter3")){
            StartCoroutine("teleport3");
            transicion.SetTrigger("Fade");
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Teleporter4")){
            StartCoroutine("teleport4");
            transicion.SetTrigger("Fade");
        }
    }

    IEnumerator teleport3(){
        controlador.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        playerTransform.position = new Vector3(234f, 2.6f, -582f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        yield return new WaitForSeconds(0.1f);
        controlador.inmovilizado = false;
    }

    IEnumerator teleport4(){
        controlador.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        playerTransform.position = new Vector3(234f, 89.4f, -76f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        yield return new WaitForSeconds(0.1f);
        controlador.inmovilizado = false;
    }
}
