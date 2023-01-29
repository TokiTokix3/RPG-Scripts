using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[Serializable]
public class Dialogue
{
    public string[] names;

    [TextArea(1, 5)]
    public string[] sentences;
    public UnityEvent unityEvent;
}
