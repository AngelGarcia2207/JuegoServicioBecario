using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallina : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) {
            return;
        }

        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f) {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.segundoNivelActivo) {
            GameManager.Instance.gallinaCapturada = true;
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
