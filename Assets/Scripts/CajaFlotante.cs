using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaFlotante : MonoBehaviour
{
    public float verticalShift = -2f;
    public float bounceSpeed = 0.5f;

    private float originalY;
    private float targetY;
    private bool movingUp = true;

    void Start() {
        originalY = transform.position.y;
        StartCoroutine(bounceCoroutine());
    }

    IEnumerator bounceCoroutine() {
        while (true) {
            if (movingUp) {
            targetY = transform.position.y + verticalShift;
            }
            else {
                targetY = originalY;
            }

            while (Mathf.Abs(targetY - transform.position.y) > 0.01f) {
                float newY = Mathf.MoveTowards(transform.position.y, targetY, bounceSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }

            movingUp = !movingUp;
        }
    }
}
