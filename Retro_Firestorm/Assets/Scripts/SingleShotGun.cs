using Photon.Pun;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] 
    private Camera cam;

    private PhotonView PV;

    /// <summary>
    /// Initialization method for the gun.
    /// </summary>
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    /// <summary>
    /// Overrides the Use method for shooting functionality.
    /// </summary>
    public override void Use()
    {
        Shoot();
    }

    /// <summary>
    /// Handles the shooting logic.
    /// </summary>
    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IDamageable target = hit.collider.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                // Using 'this.gameObject' as the source of the damage
                target.TakeDamage(((GunInfo)itemInfo).damage, this.gameObject);
            }

            // Existing network call for shooting effects
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }
	
    /// <summary>
    /// RPC call for shooting, instantiates bullet impact and sets parent.
    /// </summary>
    [PunRPC]
    private void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
            Destroy(bulletImpactObj, 10f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);
        }
    }
}
