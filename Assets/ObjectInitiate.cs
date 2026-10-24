using UnityEngine;
using System.Collections.Generic;
using Meta.XR.Samples;
using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;
using TMPro;

public class ObjectInitiate : MonoBehaviour
{
    [SerializeField] private GameObject handAndGrabPrefab;
    [SerializeField] private GameObject pokeInteractionPrefab;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Canvas canvas;
   // [SerializeField] private BoneObjectController boneObjectController;
    
    void Start()
    {
        Initialize();
    }

    private void CorrectName(Transform bone)
    {
        if (bone.gameObject.name.Contains(".r"))
            {
                Debug.Log("It is a right bone so lets change the name for a readable name");
                string name = bone.gameObject.name.Split('.')[0];
                bone.gameObject.name = "Right " + name;
            }
            else if (bone.gameObject.name.Contains(".l"))
            {
                Debug.Log("It is a left bone so lets change the name for a readable name");
                string name = bone.gameObject.name.Split('.')[0];
                bone.gameObject.name = "Left " + name;
            }
    }

    private void Initialize()
    {

        if (handAndGrabPrefab == null)
        {
            Debug.LogError("Assign the Hand and Grab prefab in the Inspector.", this);
            return;
        }



        foreach (Transform bone in transform)
        {
            BoneObjectController boneController = bone.GetComponent<BoneObjectController>();
            if (boneController == null)
            {
                Debug.LogError($"Bone '{bone.name}' needs a BoneObjectController component.", bone);
                continue;
            }
            boneController.SetBoneRenderer(bone.GetComponent<Renderer>());
            CorrectName(bone);
            Rigidbody rb = bone.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError($"Bone '{bone.name}' needs a Rigidbody for its grab interactables.", bone);
                continue;
            }
            Grabbable grabbable = bone.GetComponent<Grabbable>();
            if (grabbable == null)
            {
                Debug.LogError($"Bone '{bone.name}' needs a Grabbable component for its grab interactables.", bone);
                continue;
            }
            GameObject instantiatedObject = Instantiate(handAndGrabPrefab, bone);
            instantiatedObject.GetComponent<HandGrabInteractable>().InjectRigidbody(rb);
            instantiatedObject.GetComponent<GrabInteractable>().InjectRigidbody(rb);
            instantiatedObject.GetComponent<HandGrabInteractable>().InjectOptionalPointableElement(grabbable);
            instantiatedObject.GetComponent<GrabInteractable>().InjectOptionalPointableElement(grabbable);
            instantiatedObject.GetComponent<HandGrabInteractable>().InjectOptionalPointableElement(grabbable);
            instantiatedObject.GetComponent<HandGrabInteractable>().WhenPointerEventRaised += (args) =>
            {
                if (args.Type == PointerEventType.Select)
                {
                    boneController.SetColor(highlightColor);
                    UiInstance.instance.nameField.text = $"{bone.name}";
                }
                else if (args.Type == PointerEventType.Unselect)
                {
                    Debug.Log($"Bone '{bone.name}' was released.", bone);
                    UiInstance.instance.nameField.text = "";
                    boneController.SetColor(Color.white);
                }
            };
        }
    }

}
