using UnityEngine;
public interface IDamageable
{
    /// <summary>
    /// Apply damage to the object.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    /// <param name="source">The source of the damage.</param>
    void TakeDamage(float damage, GameObject source);

    /// <summary>
    /// Checks whether the object is still alive.
    /// </summary>
    /// <returns>True if the object is alive, false otherwise.</returns>
    bool IsAlive();

    /// <summary>
    /// Called when the object's health reaches zero.
    /// </summary>
    void Destroy();
}
