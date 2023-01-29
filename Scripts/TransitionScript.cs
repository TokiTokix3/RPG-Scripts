using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public Encounter encounter;
    public Vector3 worldPosition;
    public string currentScene;
    public int fightMoney = 100;
    public bool isFinalBattle = false;
    public Dictionary<string, MonoBehaviour> dic = new Dictionary<string, MonoBehaviour>();
    public Dictionary<string, bool> flagDic = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static TransitionScript _instance;
    public static TransitionScript Instance { get { return _instance; } }

    public bool checkFlag(string flag)
    {
        if (flagDic.ContainsKey(flag))
        {
            return flagDic[flag];
        }
        return false;
    }

    public void addFlag(string str, bool flag)
    {
        flagDic.Add(str, flag);
    }

    public void addTrueFlag(string str)
    {
        flagDic.Add(str, true);
    }

    public MonoBehaviour checkDictionary(string str, MonoBehaviour gameObject)
    {
        if (dic.ContainsKey(str))
        {
            return dic[str];
        }
        dic.Add(str, gameObject);
        return gameObject;
    }

    public bool updateDictionary(string str, MonoBehaviour gameObject)
    {
        if (dic.ContainsKey(str))
        {
            dic[str] = gameObject;
            return true;
        }
        return false;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}
