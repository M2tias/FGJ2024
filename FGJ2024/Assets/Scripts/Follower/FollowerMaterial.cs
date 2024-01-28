using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMaterial : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;
    [SerializeField]
    SkinnedMeshRenderer bodyRenderer;
    [SerializeField]
    MeshRenderer headRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        Material randomMaterial = materials[Random.Range(0, materials.Count - 1)];
        bodyRenderer.sharedMaterial = randomMaterial;
        headRenderer.sharedMaterial = randomMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
