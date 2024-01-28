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
        bodyRenderer.sharedMaterial = materials[Random.Range(0, materials.Count - 1)];
        headRenderer.sharedMaterial = materials[Random.Range(0, materials.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
