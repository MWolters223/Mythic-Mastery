using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        view = GetComponent<PlayerView>();
    }

    public Vector2 GetMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");
        return new Vector2(horizontalInput, verticalInput);
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

    public bool IsShooting()
    {
        return Input.GetMouseButton(0);
    }
}
