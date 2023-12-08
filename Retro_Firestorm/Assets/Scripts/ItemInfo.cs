using UnityEngine;

[CreateAssetMenu(fileName = "NewItemInfo", menuName = "Item Info", order = 51)]
public class ItemInfo : ScriptableObject
{
    [SerializeField] protected string itemName; // Protected for subclass access
    [SerializeField] protected string description; // Additional field for item description
    [SerializeField] protected Sprite icon; // Icon for the item

    // Public accessors
    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
}
