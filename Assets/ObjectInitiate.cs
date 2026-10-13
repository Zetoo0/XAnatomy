using UnityEngine;
using System.Collections.Generic;
using Meta.XR.Samples;
using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;
using TMPro;

public class ModelTestComponents : MonoBehaviour
{
    [SerializeField] private GameObject handAndGrabPrefab;
    [SerializeField] private GameObject pokeInteractionPrefab;
    [SerializeField] private TMP_Text feedbackText;
    
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
            if (bone.gameObject.name.Contains(".r"))
            {
                Debug.Log("It is a right bone so lets change the name for a readable name");
                string name = bone.gameObject.name.Split('.')[0];
                bone.gameObject.name = "Right " + name;
            }
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
    
               // Debug.Log($"Bone '{bone.name}' pointer event: {args.Type}.", bone);
                if (args.Type == PointerEventType.Select)
                {
                    Debug.Log($"Bone '{bone.name}' was grabbed.", bone);
                    feedbackText.text = $"'{bone.name}'";
                    bone.GetComponent<Renderer>().material.color = Color.yellow;
                }
                else if (args.Type == PointerEventType.Unselect)
                {
                    Debug.Log($"Bone '{bone.name}' was released.", bone);
                    feedbackText.text = "";
                    bone.GetComponent<Renderer>().material.color = Color.white;
                }
                
            };

        }
    }



}
