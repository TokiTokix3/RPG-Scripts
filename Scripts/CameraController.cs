using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public float translationalStrength = 0;
    public float rotationalStrength = 1;
    public Vector3 basePosition;
    public Quaternion baseRotation;
    private float trauma = 0;
    private float yawOffset;
    private float rollOffset;
    private float pitchOffset;


    public void setCameraPostition(Vector3 position)
    {
        basePosition = position;
        updatePosition();
    }

    private void updatePosition()
    {
        camera.transform.position = basePosition;
    }

    public void setCameraRotation(Quaternion rotation)
    {
        baseRotation = rotation;
        updateRotation();
    }

    private void updateRotation()
    {
        camera.transform.rotation = Quaternion.Euler(baseRotation.eulerAngles + new Vector3(pitchOffset, yawOffset, rollOffset));
    }

    public void addTrauma(float add)
    {
        if(trauma < 1)
            trauma += add;
    }

    public void lookAt(GameObject thing)
    {
        camera.transform.LookAt(thing.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trauma > 0)
        {
            trauma -= .15f;
            yawOffset = .1f * trauma * trauma * negativePerloin(Time.time * 5, 0);
            pitchOffset = .1f * trauma * trauma * negativePerloin(Time.time * 5, 0);
            rollOffset = 10 * trauma * trauma * negativePerloin(Time.time * 5, 0);
            updateRotation();
        }
        else
            trauma = 0;
        
    }
    // -1 to 1
    private float negativePerloin(float x, float y)
    {
        return (Mathf.PerlinNoise(x, y) * 2) - 1;
    }
}
