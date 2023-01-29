using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridHandler;

public abstract class Effect : ScriptableObject
{
    public int duration;
    public GameObject animation;
    public EffectType effectType;
    public float hitShake = 0;
    public AudioClip audio;
    public abstract void useEffect(TileEntity target);
}
public enum EffectType
{
    passive, immediate
}
