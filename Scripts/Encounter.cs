using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemySpawn{
    public Enemy enemy;
    public int x;
    public int y;
}
[Serializable]
public class PlayerSpawn
{
    public PlayerCharacter player;
    public int x;
    public int y;
}

[CreateAssetMenu(fileName = "New_Encounter", menuName = "Encounter")]
public class Encounter : ScriptableObject
{
    public EnemySpawn[] enemySpawns;
    public PlayerSpawn[] playerSpawns;
    public int gridSizeX;
    public int gridSizeY;
    public Song song;
}
