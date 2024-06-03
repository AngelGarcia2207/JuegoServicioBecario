using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private GameObject shadow;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            sfxSource.Play();
            GameManager.Instance.ObtenerMoneda();
            StartCoroutine(DestroyThis());
            renderer.enabled = false;
            collider.enabled = false;
            shadow.SetActive(false);
        }
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}