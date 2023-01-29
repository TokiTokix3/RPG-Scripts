using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New_Effect_Damage", menuName = "Effects/Damage")]
public class Damage : Effect
{
    public int amount = 1;
    public override void useEffect(GridHandler.TileEntity target)
    {
        target.character.GetComponent<BattleInstance>().takeDamage(amount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
