using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Action")]
public class Action : ScriptableObject
{
    public float playerStartup;
    public float effectStartup;
    public Sprite emblem;
    public BattleCharacter target;
    public BattleCharacter source;
    public Effect[] effects;
    public int[] effectedSquareX;
    public int[] effectedSquareY;
    public Action[] actions;
    public int coolDown;
    public int energyCost;
}
