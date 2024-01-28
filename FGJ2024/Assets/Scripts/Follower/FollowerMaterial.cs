using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMaterial : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;
    [SerializeField]
    List<Material> armMaterials;


    [SerializeField]
    SkinnedMeshRenderer bodyRenderer;
    [SerializeField]
    MeshRenderer headRenderer;

    [SerializeField]
    List<MeshRenderer> armRenderers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        int index = Random.Range(0, materials.Count);
        Material randomMaterial = materials[index];
        Material armMaterial = armMaterials[index];
        bodyRenderer.sharedMaterial = randomMaterial;
        headRenderer.sharedMaterial = randomMaterial;

        foreach(MeshRenderer mesh in armRenderers)
        {
            mesh.material = armMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
