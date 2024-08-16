using System.Collections.Generic;
using UnityEngine;

public class ScarabeeDebug : MonoBehaviour
{
    public float speed = 100f; // Same speed as the scarabee
    public int maxReflections = 3; // Same max reflections as the scarabee
    public int raycastDistance = 999;

    public List<Vector3> cachedReflectionPoints;
    public List<Vector3> traveledPathPoints;

    void Start()
    {
        // Cache the initial reflection pattern
        cachedReflectionPoints = new List<Vector3>();
        traveledPathPoints = new List<Vector3>();
        CacheReflectionPattern(transform.position, transform.forward, maxReflections);
    }

    void OnDrawGizmos()
    {
        // Draw the cached reflection pattern
        if (cachedReflectionPoints != null && cachedReflectionPoints.Count > 1)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < cachedReflectionPoints.Count - 1; i += 2)
            {
                Gizmos.DrawLine(cachedReflectionPoints[i], cachedReflectionPoints[i + 1]);
            }
        }

        // Draw the traveled path
        if (traveledPathPoints != null && traveledPathPoints.Count > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < traveledPathPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(traveledPathPoints[i], traveledPathPoints[i + 1]);
            }
        }

        // Also draw the arrow and sphere
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 0.5f);
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    private void CacheReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining <= 0)
            return;

        Vector3 startingPosition = position;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // Reflect direction based on the hit normal
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            // If no hit, move in the direction
            position += direction * raycastDistance;
        }

        // Cache the position for later use
        cachedReflectionPoints.Add(startingPosition);
        cachedReflectionPoints.Add(position);

        // Recursively cache the next segment of the reflection pattern
        CacheReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
}