using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class representing a gun. Inherits from Item.
/// </summary>
public abstract class Gun : Item
{
    [SerializeField]
    protected GameObject bulletImpactPrefab; 

    /// <summary>
    /// Use this gun. Abstract method that must be implemented in derived classes.
    /// </summary>
    public abstract override void Use();

    /// <summary>
    /// Gets or sets the bullet impact prefab.
    /// </summary>
    public GameObject BulletImpactPrefab
    {
        get { return bulletImpactPrefab; }
        set { bulletImpactPrefab = value; }
    }

}

