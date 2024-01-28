using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMaterial : MonoBehaviour
{
    [SerializeField]
    List<Texture> textures;
    [SerializeField]
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        material.mainTexture = textures[Random.Range(0, textures.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
