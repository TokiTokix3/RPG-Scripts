using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inhabitant
{
    player, enemy, something, empty
}
public enum State
{
    walkable, blocked
}
public enum Side
{
    player, enemy, neutral
}

public class Tile : MonoBehaviour
{
    public Inhabitant inhabitant = Inhabitant.empty;
    public State state = State.walkable;
    public Collider collider;
    public bool isTargeted;
    public Side side;
    public MeshRenderer meshRenderer;
    public Material redMaterial;
    public Material greenMaterial;
    private Coroutine coroutine;

    public bool isWalkable()
    {
        return inhabitant == Inhabitant.empty;
    }

    public void isBlocked(bool status)
    {
        if(status == true)
        {
            state = State.blocked;
        }
        state = State.walkable;
    }

    public void setInhabitant(Inhabitant newInhabitant)
    {
        inhabitant = newInhabitant;
    }

    public void setSide(Side newSide)
    {
        side = newSide;
        updateColor();
    }

    public void updateColor()
    {
        if (!isTargeted)
        {
            switch (side)
            {
                case Side.player:
                    meshRenderer.material = greenMaterial;
                    break;
                case Side.enemy:
                    meshRenderer.material = redMaterial;
                    break;

            }
        }
        else
        {
            meshRenderer.material = redMaterial;
        }
    }

    public IEnumerator setTargetedTimer(float sec)
    {
        /*if(coroutine != null)
            StopCoroutine(coroutine);*/
        if(sec > .5f)
        {
            yield return new WaitForSeconds(sec - .5f);
            coroutine = StartCoroutine(targetedTimer(.5f));
        }
        else
            coroutine = StartCoroutine(targetedTimer(sec));
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator targetedTimer(float sec)
    {
        isTargeted = true;
        updateColor();
        yield return new WaitForSeconds(sec);
        isTargeted = false;
        updateColor();
    }

    public void setTempo(float tempo)
    {
        GetComponent<Renderer>().material.SetFloat("Speed", tempo * 55);
    }

/*    public void OnCollisionStay(Collision collision)
    {
        Debug.Log(name + " colliding with " + collision);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(name + " colliding with " + contact);
        }
    }

    public void OnTriggerStay(Collision collision)
    {
        Debug.Log(name + " triggering with " + collision);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(name + " triggering with " + contact);
        }
    }*/

    /*public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " colliding with " + collision);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(name + " colliding with " + contact);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
