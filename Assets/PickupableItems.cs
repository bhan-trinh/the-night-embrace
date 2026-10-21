using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PickupableItems : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public Sprite inventorySprite;

    [TextAreaAttribute]
    public string itemDesc;
}
