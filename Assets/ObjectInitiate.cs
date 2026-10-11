using UnityEngine;
using System.Collections.Generic;
using Meta.XR.Samples;
using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;

public class ModelTestComponents : MonoBehaviour
{
    [SerializeField] private GameObject handAndGrabPrefab;
    
    void Start()
    {
        Initialize();
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
        }
    }

}
