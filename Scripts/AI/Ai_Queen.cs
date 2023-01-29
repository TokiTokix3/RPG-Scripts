using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Queen AI")]
public class Ai_Queen : AI
{
    public int energy = 5;
    public float random = 0f;

    public override void doSomething(GridHandler gridHandler, GridHandler.TileEntity self)
    {
        GridHandler.TileEntity player = gridHandler.characters[0];
        energy++;
        random = Random.Range(0f, 1f);
        if(random < .33f)
        {
            energy -= 5;
            gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[0]);
        }
        else if(random < .66f)
        {
            energy -= 5;
            gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[1]);
        }
        else
        {
            energy -= 5;
            gridHandler.useEnemyAttack(self, self.character.GetComponent<BattleInstance>().character.actions[2]);
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
