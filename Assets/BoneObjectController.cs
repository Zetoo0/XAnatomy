using System.Numerics;
using UnityEngine;

public class BoneObjectController : MonoBehaviour
{

    private Renderer boneRenderer;

    private MaterialPropertyBlock boneMaterialPropertyBlock;
    private UnityEngine.Vector3 objectBasePosition;
    private UnityEngine.Vector3 objectUnselectedPosition;
    private bool objectGrabbedAndNotInPlace = false;

    void Awake()
    {
        this.boneMaterialPropertyBlock = new MaterialPropertyBlock();
        this.objectBasePosition = this.transform.position;
    } 

    public void SetBoneRenderer(Renderer renderer)
    {
        this.boneRenderer = renderer;
    }

    void Update()
    {
        /*if(this.objectGrabbedAndNotInPlace && UnityEngine.Vector3.Distance(objectUnselectedPosition, objectBasePosition) <= 0.1f)
        {
            Debug.Log("Its pretty near lets set it back to the original position");
            this.transform.position = this.objectBasePosition;    
            this.ChangeObjectGrabAndNotInPlace(false); 
        }*/
    }

    public void SetColor(Color color)
    {
        Debug.Log($"Setting color of bone '{gameObject.name}' to {color}.");
        this.boneMaterialPropertyBlock.Clear();
        this.boneMaterialPropertyBlock.SetColor("_BaseColor", color);
        this.boneRenderer.SetPropertyBlock(boneMaterialPropertyBlock);    
    }

    public void SetObjectUnselectedPosition(UnityEngine.Vector3 newPos)
    {
        this.objectUnselectedPosition = newPos;
    }

    public void ChangeObjectGrabAndNotInPlace(bool b)
    {
        this.objectGrabbedAndNotInPlace = b;
    }

    public bool GetObjectGrabAndNotInPlace()
    {
        return this.objectGrabbedAndNotInPlace;
    }

    public void SetObjectBasePosition(UnityEngine.Vector3 pos)
    {
        objectBasePosition = pos;
    }

    public UnityEngine.Vector3 GetObjectBasePosition()
    {
        return this.objectBasePosition;
    }

    public void CheckIfChangedPosIsNearEnough()
    {
        if(this.objectGrabbedAndNotInPlace && UnityEngine.Vector3.Distance(this.objectUnselectedPosition, this.objectBasePosition) < 0.1f)
        {
            Debug.Log("Its pretty near lets set it back to the original position");
            this.transform.position = this.objectBasePosition;    
            this.ChangeObjectGrabAndNotInPlace(false);
        }
    }
    

}
