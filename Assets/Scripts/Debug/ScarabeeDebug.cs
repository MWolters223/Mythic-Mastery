using UnityEngine;
using UnityEditor;

public class ScarabeeDebug : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draws an arrow to represent the right direction of the object
        Gizmos.DrawRay(transform.position, -transform.right * 0.5f);

        // Draws a wire sphere at the object's position
        Gizmos.DrawWireSphere(transform.position, 0.25f);

        // Draws the predicted reflection pattern
        DrawPredictedReflectionPattern(transform.position + -transform.right * 0.5f, -transform.right, 3);
    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining <= 0)
            return;

        Vector3 startingPosition = position;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        // Perform raycast to detect collisions
        if (Physics.Raycast(ray, out hit, 999))
        {
            // Reflect direction based on the hit normal
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            // If no hit, move in the direction
            position += direction * 3;
        }

        // Draw a line to represent the predicted path
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        // Recursively draw the next segment of the reflection pattern
        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
}