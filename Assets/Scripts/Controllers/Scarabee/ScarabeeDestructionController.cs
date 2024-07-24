using UnityEngine;

public class ScarabeeDestructionController : MonoBehaviour
{
    public void DestroyObject(GameObject obj)
    {
        GameObject parent = obj.transform.parent?.gameObject;
        Destroy(parent ?? obj);
    }

    public void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
}
