using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Item")]
public class Item : Action
{
    public new string name;         // Name of item
    public string description;      // Description of item

    public int purchaseCost;        // Cost to buy
    public int sellCost;            // Cost to sell
    public int uses;                // How many uses the item has
    public bool unlimitedUses;
}
