using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHandler;

[CreateAssetMenu(fileName = "New_Effect_DefUp", menuName = "Effects/DefUp")]
public class DefUp : Effect
{
    public override void useEffect(TileEntity target)
    {
        target.character.GetComponent<BattleInstance>().takeDamage(2);
    }
}
