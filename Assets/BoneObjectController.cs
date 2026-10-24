using UnityEngine;

public class BoneObjectController : MonoBehaviour
{

    private Renderer boneRenderer;

    private MaterialPropertyBlock boneMaterialPropertyBlock;

    void Awake()
    {
        boneMaterialPropertyBlock = new MaterialPropertyBlock();
    } 

    public void SetBoneRenderer(Renderer renderer)
    {
        boneRenderer = renderer;
    }

    public void SetColor(Color color)
    {
        Debug.Log($"Setting color of bone '{gameObject.name}' to {color}.");
        boneMaterialPropertyBlock.Clear();
        boneMaterialPropertyBlock.SetColor("_BaseColor", color);
        boneRenderer.SetPropertyBlock(boneMaterialPropertyBlock);    
    }

}
