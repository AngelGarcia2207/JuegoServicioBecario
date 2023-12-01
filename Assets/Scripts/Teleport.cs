using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] ControladorJugador controlador;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator transicion;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Teleporter1")){
            StartCoroutine("teleport1");
            transicion.SetTrigger("Fade");
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Teleporter2")){
            StartCoroutine("teleport2");
            transicion.SetTrigger("Fade");
        }
    }

    IEnumerator teleport1(){
        controlador.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        playerTransform.position = new Vector3(8.25f, 3f, -302f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        controlador.inmovilizado = false;
    }

    IEnumerator teleport2(){
        controlador.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        playerTransform.position = new Vector3(8.25f, 55.3f, -120f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        controlador.inmovilizado = false;
    }
}