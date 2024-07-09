using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
	private PlayerModel model;
	private PlayerView view;
	private GameObject projectile;

	public void Initialize(PlayerModel model, PlayerView view, GameObject projectile)
	{
		this.model = model;
		this.view = view;
		this.projectile = projectile;

    }

	public void HandleShooting(Vector3 direction)
	{
		if (direction.magnitude < 1f || direction == Vector3.zero)
			return;

		ShootProjectile(direction);
	}

	private void ShootProjectile(Vector3 direction)
	{
		direction.y = 0;
		GameObject projectileObj = SpawnProjectile(direction);
		Rigidbody rb = projectileObj.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.velocity = direction * model.projectileSpeed;
		}
	}

	private GameObject SpawnProjectile(Vector3 direction)
	{
		Quaternion rotation = Quaternion.LookRotation(direction);
		Vector3 projectilePosition = view.StatueTransform.position + view.StatueTransform.forward * model.projectileRadius + new Vector3(0, model.shootingHeight, 0);
		GameObject projectileObj = Instantiate(projectile, projectilePosition, rotation);
		ConfigureProjectile(projectileObj);
		return projectileObj;
	}

	private void ConfigureProjectile(GameObject projectileObj)
	{
		Projectile projectile = projectileObj.GetComponent<Projectile>();
		projectile.maxReflectCount = model.maxReflectCount;
		projectile.speed = model.projectileSpeed;
	}
}
