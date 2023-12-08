using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemInfo itemInfo; // Protected to allow access in subclasses
    [SerializeField] protected GameObject itemGameObject; // Reference to the item's GameObject

    // Public properties for accessing protected fields
    public ItemInfo Info => itemInfo;
    public GameObject GameObject => itemGameObject;

    // Abstract method for item usage
    public abstract void Use();
}
