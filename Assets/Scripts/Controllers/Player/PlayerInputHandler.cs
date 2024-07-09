using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        return new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
    }

    public bool IsShooting()
    {
        return Input.GetMouseButton(0);
    }

    public Vector3 GetMouseDirection(Transform playerTransform, LayerMask groundMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3 direction = hit.point - playerTransform.position;
            direction.Normalize();
            return direction;
        }
        return Vector3.zero;
    }
}
