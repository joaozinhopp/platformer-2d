using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public Transform playerSideReference;

    void Start()
    {
        // Inicializa��es, se necess�rio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile, positionToShoot.position, Quaternion.identity);
        projectile.direction = Vector3.right * playerSideReference.transform.localScale.x;
    }
}
