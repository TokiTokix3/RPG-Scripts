using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using static GridHandler;

[CreateAssetMenu(fileName = "New_Effect_HealthUp", menuName = "Effects/HealthUp")]
public class HealthUp : Effect
{
    public int amount;
    public override void useEffect(TileEntity target)
    {
        target.character.GetComponent<BattleInstance>().takeDamage(-amount);
    }
}