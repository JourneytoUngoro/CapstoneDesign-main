using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTesting : MonoBehaviour
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material blueMaterial;

    public void GenerateCube()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ChangeColor(int index)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        switch(index)
        {
            case 0:
                meshRenderer.material = whiteMaterial; break;
            case 1:
                meshRenderer.material = redMaterial; break;
            case 2:
                meshRenderer.material = greenMaterial; break;
            case 3:
                meshRenderer.material = blueMaterial; break;
            default:
                break;
        }
    }
}
