using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelPlaneGenerator : MonoBehaviour
{
    public Texture2D texture;
    public GameObject voxel;
    public float spacing;
    private GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.width; y++)
            {
                if(!(texture.GetPixel(x, y) == Color.clear))
                {
                    temp = Instantiate(voxel, new Vector3(gameObject.transform.position.x + (spacing * x) - ((texture.width/2) * spacing), gameObject.transform.position.y + (spacing * y) - ((texture.height / 2)*spacing), gameObject.transform.position.z), Quaternion.identity);
                    temp.transform.SetParent(gameObject.transform.parent);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
