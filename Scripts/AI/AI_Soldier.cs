using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier AI")]
public class AI_Soldier : AI
{
    public override void doSomething(GridHandler gridHandler, GridHandler.TileEntity self)
    {
        GridHandler.TileEntity player = gridHandler.characters[0];
        if (self.x > gridHandler.sizeX / 2)
        {
            gridHandler.moveEntity(self.x - 1, self.y, self);
        }
        else if (player.y == self.y)
        {
            if (self.x - player.x >= 3)
            {
                Debug.Log(self.character.GetComponent<BattleInstance>().character.actions[0].name);
                gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[2]);
            }
            else if (self.x - player.x >= 2)
            {
                gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[1]);
            }
            else
            {
                gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[0]);
            }
        }
        else
        {
            if (player.y > self.y)
            {
                gridHandler.moveEntity(self.x, self.y + 1, self);
            }
            else if(player.y < self.y)
            {
                gridHandler.moveEntity(self.x, self.y - 1, self);
            }
        }
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

