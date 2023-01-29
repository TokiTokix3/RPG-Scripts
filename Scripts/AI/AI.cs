using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHandler;

[SerializeField]
public abstract class AI : ScriptableObject
{

    public abstract void doSomething(GridHandler gridHandler, TileEntity self);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
