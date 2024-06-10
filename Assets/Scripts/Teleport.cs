using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] ControladorJugador controlador;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator transicion;
    [SerializeField] MusicManager music;

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
        music.StopAll();
        playerTransform.position = new Vector3(8.25f, 2.6f, -567f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        music.PlayBank();
        yield return new WaitForSeconds(0.1f);
        controlador.inmovilizado = false;
    }

    IEnumerator teleport2(){
        controlador.inmovilizado = true;
        yield return new WaitForSeconds(1f);
        music.StopAll();
        playerTransform.position = new Vector3(8.25f, 186f, -258f);
        yield return new WaitForSeconds(1f);
        transicion.SetTrigger("Fade");
        music.PlayNormalMusic();
        yield return new WaitForSeconds(0.1f);
        controlador.inmovilizado = false;
    }
}