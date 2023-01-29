using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : Character
{
    public int health;
    public int energy;
    public int moveEnergyCost;
    public int level;           // Level of BattleCharacter
    public Effect[] effects;    // Array to store all effects on the BattleCharacter
    public Action[] actions;
    public Item[] items;
    
    public void addAction(Action action)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            if(actions[i] == null)
            {
                actions[i] = action;
                return;
            }
        }
    }
}
