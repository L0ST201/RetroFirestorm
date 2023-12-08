using UnityEngine;

/// <summary>
/// ScriptableObject representing information about a gun.
/// </summary>
[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
    [Tooltip("Damage inflicted by the gun.")]
    public float damage;

    [Tooltip("Maximum range of the gun.")]
    public float range;

    [Tooltip("Fire rate of the gun, shots per minute.")]
    public float fireRate;

    [Tooltip("Capacity of the ammunition magazine.")]
    public int ammoCapacity;

    [Tooltip("Time taken to reload the gun in seconds.")]
    public float reloadTime;

    private void OnEnable()
    {

    }
}
