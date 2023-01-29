using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOST }

public class BattleManager : MonoBehaviour
{
    public GridHandler gridHandler;
    public CameraController cameraController;

    public BattleManager()
    {

    }

    void start()
    {
        Debug.Log("something");
        GameObject instance = GameObject.Find("transitionObject");
        Debug.Log(instance);
        startEncounter(instance.GetComponent<Encounter>());
    }

    public void forcedStart()
    {
        Debug.Log("something");
        GameObject instance = GameObject.Find("transitionObject");
        Debug.Log(instance);
        startEncounter(GameObject.FindObjectOfType<TransitionScript>().encounter);
    }

    void update()
    {

    }

    public void startEncounter(Encounter encounter)
    {
        CreateGrid(encounter.gridSizeX, encounter.gridSizeY, Vector3.zero);
        Debug.Log(encounter.name);
        Debug.Log(encounter.enemySpawns);
        foreach(EnemySpawn enemy in encounter.enemySpawns)
        {
            gridHandler.spawnEnemyCharacter(enemy.x, enemy.y, enemy.enemy);
        }
        gridHandler.spawnPlayerCharacter(encounter.playerSpawns[0].x, encounter.playerSpawns[0].y, encounter.playerSpawns[0].player);
        cameraController.setCameraPostition(new Vector3((float)(encounter.gridSizeX * .4), 2.33f, -2.84f));
        cameraController.setCameraRotation(Quaternion.Euler(23,0,0));
        Debug.Log("playing song " + encounter.song.name);
        gridHandler.startSong(encounter.song);
        gridHandler.cameraController = cameraController;
    }

    public void spawnEnemyArray(Enemy[] enemies)
    {
        foreach(Enemy enemy in enemies)
        {
            //gridHandler.spawnEnemyCharacter(enemy);
        }
    }

    public void CreateGrid(int x, int y, Vector3 origin)
    {
        gridHandler.createGrid(x, y, origin);
    }

    public void moveSelectedPlayer(int direction)
    {
        gridHandler.moveSelectedCharacter(direction);
    }

    public void useAttack(int attackNumber)
    {
        gridHandler.usePlayerAttack(attackNumber);
    }

    public void useItem(int itemNumber)
    {
        StartCoroutine(gridHandler.usePlayerItem(itemNumber));
    }

}