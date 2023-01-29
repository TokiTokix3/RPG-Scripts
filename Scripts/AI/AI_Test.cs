using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Test AI")]
public class AI_Test : AI
{
    public override void doSomething(GridHandler gridHandler, GridHandler.TileEntity self)
    {
        GridHandler.TileEntity player = gridHandler.characters[0];
        if (player.y == self.y)
        {
            Debug.Log(self.x - player.x);
            if(self.x - player.x <= 2)
            {
                Debug.Log(self.character.GetComponent<BattleInstance>().character.actions[0].name);
                gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[0]);
            }
            else
            {
                gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[1]);
            }
        }
        else if (Random.Range(0f, 1f) < .75)
        {
            if (self.y < gridHandler.characters[0].y)
            {
                gridHandler.moveEntity(self.x, self.y + 1, self);
            }
            else if (self.y > gridHandler.characters[0].y)
            {
                gridHandler.moveEntity(self.x, self.y - 1, self);
            }
        }
        else
        {
            if(Random.Range(0f, 1f) < .5)
            {
                gridHandler.moveEntity(self.x + 1, self.y, self);
            }
            else
            {
                gridHandler.moveEntity(self.x - 1, self.y, self);
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
