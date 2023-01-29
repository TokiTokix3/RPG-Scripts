using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridHandler : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    Vector3 origin;
    GameObject[,] tiles;
    
    public GameObject tile;
    public GameObject gridEntity;
    public int selectedCharacter = 0;
    public TileEntity[] characters = new TileEntity[4];
    public List<TileEntity> enemies = new List<TileEntity>();
    public float tempo = .5f;
    public BattleCharacter exampleBattleCharacter;
    public BattleCharacter exampleEnemyCharacter;
    public UIManager ui;
    public int buffer = 0;
    public AudioManager audioManager;
    public Scene returnScene;
    public CameraController cameraController;
    public GameObject gridOutline;
    GameObject[,] outlines;

    public class TileEntity
    {
        public GameObject character;
        public int x, y;
    }

    public void createGrid(int x, int y, Vector3 origin)
    {
        sizeX = x;
        sizeY = y;
        tiles = new GameObject[sizeX, sizeY];
        outlines = new GameObject[sizeX, sizeY];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                //Debug.LogError(tile + " || " + (origin + new Vector3(i, j, 0)) + " || " + Quaternion.identity);
                tiles[i,j] = Instantiate(tile, origin + new Vector3(i, 0, j), Quaternion.identity);
                outlines[i,j] = Instantiate(gridOutline, origin + new Vector3(i, 0, j) - new Vector3(6.1085f, 0, 0) + new Vector3(0, 0, 5.325f), Quaternion.identity);
                if(i < (x / 2))
                {
                    tiles[i, j].GetComponent<Tile>().setSide(Side.player);
                }
                else
                {
                    tiles[i, j].GetComponent<Tile>().setSide(Side.enemy);
                }
                Debug.Log(i + " || " + j);
            }
        }
    }

    public void moveSelectedCharacter(int direction)
    {
        BattleInstance battleInstance = characters[selectedCharacter].character.GetComponent<BattleInstance>();
        if (battleInstance.canAct)
        {
            int x = characters[selectedCharacter].x;
            int y = characters[selectedCharacter].y;

            switch (direction)
            {
                case 4:
                    if (moveEntity(x - 1, y, characters[selectedCharacter]))
                        tiles[x, y].GetComponent<Tile>().isBlocked(false);
                    break;
                case 6:
                    if(tiles[x + 1, y].GetComponent<Tile>().side == Side.player)
                        if (moveEntity(x + 1, y, characters[selectedCharacter]))
                            tiles[x, y].GetComponent<Tile>().isBlocked(false);
                    break;
                case 8:
                    if (moveEntity(x, y + 1, characters[selectedCharacter]))
                        tiles[x, y].GetComponent<Tile>().isBlocked(false);
                    break;
                case 2:
                    if (moveEntity(x, y - 1, characters[selectedCharacter]))
                        tiles[x, y].GetComponent<Tile>().isBlocked(false);
                    break;
            }
        }
    }

    public bool moveEntity(int x, int y, TileEntity entity)
    {
        if (isOnGrid(x, y) && entity.character.GetComponent<BattleInstance>().character is Enemy)
            if (tiles[x, y].GetComponent<Tile>().side != Side.enemy)
                return false;
        if (isOnGrid(x, y) && tiles[x, y].GetComponent<Tile>().isWalkable() && entity.character.GetComponent<BattleInstance>().canAct)
        {
            tiles[entity.x, entity.y].GetComponent<Tile>().setInhabitant(Inhabitant.empty);
            entity.character.GetComponent<BattleInstance>().loseEnergy(entity.character.GetComponent<BattleInstance>().character.moveEnergyCost);
            if(characters.Contains( entity))
                //ui.setEnergy(entity.character.GetComponent<BattleInstance>().currentEnergy);
            Debug.Log(entity.character.name + " to position " + x + "," + y);
            entity.x = x;
            entity.y = y;
            tiles[x, y].GetComponent<Tile>().isBlocked(true);
            tiles[x, y].GetComponent<Tile>().setInhabitant(Inhabitant.something);
            updateEntityWorldPostition(entity);
            return true;
        }
        return false;
    }

    private TileEntity spawnCharacter(int x, int y, BattleCharacter instance)
    {
        if(isOnGrid(x, y))
        {
            TileEntity entity = new TileEntity();
            entity.character = Instantiate(gridEntity, tiles[x, y].transform.position + new Vector3(0, .005f, 0), Quaternion.identity);
            entity.character.GetComponent<BattleInstance>().character = instance;
            entity.x = x;
            entity.y = y;
            updateEntityWorldPostition(entity);
            tiles[x, y].GetComponent<Tile>().isBlocked(true);
            return entity;
        }
        return null;
    }

    public void spawnPlayerCharacter(int x, int y, BattleCharacter instance)
    {       
        characters[0] = spawnCharacter(x, y, instance);
        tiles[x, y].GetComponent<Tile>().setInhabitant(Inhabitant.player);
    }

    public void spawnEnemyCharacter(int x, int y, BattleCharacter instance)
    {
        Debug.Log("spawning enemy at" + x + ", " + y);
        enemies.Add(spawnCharacter(x, y, instance));
        tiles[x, y].GetComponent<Tile>().setInhabitant(Inhabitant.enemy);
    }

    public void updateEntityWorldPostition(TileEntity entity)
    {
        entity.character.transform.position = tiles[entity.x, entity.y].transform.position + new Vector3(0,.27f,0);
    }

    public bool isOnGrid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < sizeX && y < sizeY;
    }

    public void useEnemyAttack(TileEntity tileEntity, Action action)
    {
        StartCoroutine(startAction(tileEntity, action));
    }

    public void usePlayerAttack(int attackNumber)
    {
        TileEntity tileEntity = characters[selectedCharacter];
        Action baseAction = tileEntity.character.GetComponent<BattleInstance>().character.actions[attackNumber];

        StartCoroutine(startAction(tileEntity, baseAction));
    }

    public IEnumerator startAction(TileEntity tileEntity, Action action)
    {
        Debug.Log("using action " + action.name);
        BattleInstance battleInstance = tileEntity.character.GetComponent<BattleInstance>();
        Tile tile;
        if (battleInstance.canAct && battleInstance.currentEnergy >= action.energyCost)
        {
            battleInstance.loseEnergy(action.energyCost);
            //ui.setEnergy(battleInstance.currentEnergy);

            StartCoroutine(battleInstance.setActTimer(action.playerStartup * tempo));
            if (action.actions == null | action.actions.Length == 0 | action.actions[0] == null)
            {
                action.actions = new Action[1];
                action.actions[0] = action;
            }
            foreach (Action subAction in action.actions)
            {
                for (int i = 0; i < subAction.effectedSquareX.Length; i++)
                {
                    //Debug.Log(i);
                    //Debug.Log(action.effectedSquareX);
                    if (isOnGrid(tileEntity.x + subAction.effectedSquareX[i], tileEntity.y + subAction.effectedSquareY[i]))
                    {
                        tile = tiles[tileEntity.x + subAction.effectedSquareX[i], tileEntity.y + subAction.effectedSquareY[i]].GetComponent<Tile>();
                        StartCoroutine(tile.setTargetedTimer(subAction.effectStartup * tempo));
                    }
                }
                StartCoroutine(useAttack(tileEntity, subAction));
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator usePlayerItem(int itemNumber)
    {
        BattleInstance battleInstance = characters[selectedCharacter].character.GetComponent<BattleInstance>();
        Item baseAction = battleInstance.character.items[itemNumber];
        if (battleInstance.canAct && baseAction.uses > 0)
        {
            Debug.Log("using item " + itemNumber);
            baseAction.uses--;

            StartCoroutine(battleInstance.setActTimer(baseAction.playerStartup));
            if (baseAction.actions == null | baseAction.actions[0] == null | baseAction.actions.Length == 0)
            {
                baseAction.actions = new Action[1];
                baseAction.actions[0] = baseAction;
            }
            foreach (Action action in baseAction.actions)
            {
                Debug.Log("using item " + itemNumber);
                StartCoroutine(useAttack(characters[selectedCharacter], action));
            }
            yield return new WaitForEndOfFrame();
        }

    }

    public IEnumerator useAttack(TileEntity tileEntity, Action action)
    {
        Debug.Log("attempting action " + action.name);
        yield return new WaitForSeconds(action.effectStartup * tempo);
        int tempX;
        int tempY;
        for (int i = 0; i < action.effectedSquareX.Length; i++)
        {
            tempX = tileEntity.x + action.effectedSquareX[i];
            tempY = tileEntity.y + action.effectedSquareY[i];
            if (isOnGrid(tempX, tempY)){
                foreach (Effect effect in action.effects)
                {
                    Debug.Log("attempting effect " + effect.name);
                    attemptEffect(effect, tempX, tempY);
                    Instantiate(effect.animation, tiles[tempX, tempY].transform.position + new Vector3(0,.5f,0), tiles[tempX, tempY].transform.rotation);
                }
            }
        }
    }

    public void attemptEffect(Effect effect, int x, int y)
    {
        foreach(TileEntity tileEntity in enemies.ToArray())
        {
            if (tileEntity != null)
            {
                if (tileEntity.x == x && tileEntity.y == y)
                {
                    effect.useEffect(tileEntity);
                    audioManager.Play(effect.audio);
                    cameraController.addTrauma(effect.hitShake);
                }
            }
        }
        foreach (TileEntity tileEntity in characters)
        {
            if (tileEntity != null)
            {
                if (tileEntity.x == x && tileEntity.y == y)
                {
                    effect.useEffect(tileEntity);
                    audioManager.Play(effect.audio);
                    cameraController.addTrauma(effect.hitShake);
                }
            }
        }
    }

    public IEnumerator rythym()
    {
        BattleInstance player = characters[0].character.GetComponent<BattleInstance>();
        while (1==1) {
            yield return new WaitForSeconds(tempo);
            foreach (TileEntity entity in enemies.ToArray())
            {
                if (!entity.character.GetComponent<BattleInstance>().isDead())
                    ((Enemy)entity.character.GetComponent<BattleInstance>().character).Ai.doSomething(this, entity);
            }
            player.loseEnergy(-5);
        }
    }

    public void startSong(Song song)
    {
        tempo = song.tempo;

        for(int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Debug.LogWarning(tiles[x, y].GetComponent<Renderer>().sharedMaterial);
                tiles[x, y].GetComponent<Tile>().setTempo(tempo);
            }
        }

        if(audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic(song.song);
        StartCoroutine(rythym());
    }



    // Start is called before the first frame update
    void Start()
    {
        //characters = new TileEntity[4];
        
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (TileEntity entity in enemies.ToArray())
        {
            if (!entity.character.GetComponent<BattleInstance>().isDead())
            {
                count++;
            }
        }
        if (count == 0)
        {
            buffer++;
            if (buffer > 5)
            {
                Debug.Log(GameObject.FindObjectOfType<TransitionScript>().currentScene);
                ((characters[0].character.GetComponent<BattleInstance>().character) as PlayerCharacter).money += GameObject.FindObjectOfType<TransitionScript>().fightMoney;
                SceneManager.LoadScene(GameObject.FindObjectOfType<TransitionScript>().currentScene);
            }     
        }
        else
        {
            foreach (TileEntity entity in enemies.ToArray())
            {
                if (entity.character.GetComponent<BattleInstance>().isDead())
                {
                    //Object.Destroy(entity.character);
                    //enemies.Remove(entity);
                }
                //((Enemy)entity.character.GetComponent<BattleInstance>().character).Ai.doSomething(this, entity);
            }
        }
        if (characters[0].character.GetComponent<BattleInstance>().isDead())
        {
            buffer++;
            if (buffer > 5)
            {
                Destroy(GameObject.FindObjectOfType<TransitionScript>().gameObject);
                SceneManager.LoadScene("TitleScene");
            }
        }
        ui.setEnergy(characters[0].character.GetComponent<BattleInstance>().currentEnergy);
        ui.setHealth(characters[0].character.GetComponent<BattleInstance>().currentHealth);
    }

    public void gameOver()
    {
        Debug.LogWarning("Game over man");
    }
}
