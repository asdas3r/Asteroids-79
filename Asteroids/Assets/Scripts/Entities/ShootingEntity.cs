using UnityEngine;

public class ShootingEntity : BaseGameEntity
{
    public float bulletOffset = 0.5f;
    public float bulletSpeed = 20f;

    public void Shoot()
    {
        var bulletPoint = transform.rotation * new Vector3(0, bulletOffset, 0);
        GameObject bulletGO = Instantiate(objectsManager.bulletPrefab, transform.position + bulletPoint, transform.rotation);
        bulletGO.tag = gameObject.tag;
    }
}
