using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barco : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float verticalShift = -1f;
    public float bounceSpeed = 0.25f;

    private int currentWaypointIndex = 0;
    private float originalY;
    private float targetY;
    private bool movingUp = true;

    void Start() {
        originalY = transform.position.y;
        StartCoroutine(bounceCoroutine());
    }

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

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
