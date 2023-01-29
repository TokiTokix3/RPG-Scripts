using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHandler;

[CreateAssetMenu(fileName = "New_Effect_Tired", menuName = "Effects/Tired")]
public class Tired : Effect
{
    public override void useEffect(TileEntity target)
    {
        target.character.GetComponent<BattleInstance>().takeDamage(2);
    }
}
