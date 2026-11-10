using UnityEngine;
using System.Collections.Generic;
using Meta.XR.Samples;
using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;
using TMPro;
using System.Linq;

public class ObjectInitiate : MonoBehaviour
{
    [SerializeField] private GameObject handAndGrabPrefab;
    [SerializeField] private GameObject pokeInteractionPrefab;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TeachManager teachObj;
    
    private List<string> objectNamesList = new List<string>();

    private void Awake() {
        teachObj = GameObject.Find("TeachObj").GetComponent<TeachManager>();
       // teachObj.SetObjectInitiate(this);
        Debug.Log("Object initiate in teach obj: " + teachObj.GetObjectInitiate());
    }

    void Start()
    {
        Initialize();
    }

    private void CorrectName(Transform bone)
    {
        if (!bone.gameObject.name.Contains(".r") && !bone.gameObject.name.Contains(".l")) return;
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

        foreach (Transform child in transform)
        {
            foreach(Transform bone in child)
            {
                Debug.Log($"Bone found: {bone.name}");
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
                    //TODO legyen kiválasztható egy menüpontban hogy szeretnénk-e mozgatni az objektumunkat vagy csak kattintani
                    Debug.Log($"Pointer event '{args.Type}' raised on bone '{bone.name}'.", bone);
                    if (args.Type == PointerEventType.Select)
                    {
                        boneController.SetColor(highlightColor);
                        UiInstance.instance.nameField.text = $"{bone.name}";
                        if (teachObj.IsTeachOn())
                        {
                            Debug.Log($"Teach is on. User selected bone: {bone.name}. Random bone to find: {teachObj.GetRandomBoneName()}");
                            UiInstance.instance.userSelectedField.text = bone.name;
                            if (bone.name.Equals(teachObj.GetRandomBoneName()))
                            {
                                UiInstance.instance.successField.text = "HellYeah";
                            }
                            else
                            {
                                UiInstance.instance.successField.text = "HellNah";
                            }
                        }

                        /*if (teachObj.IsTeachOn() && bone.name.Equals(teachObj.GetRandomBoneName()))
                        {
                            
                        } */
                    }
                    else if (args.Type == PointerEventType.Unselect)
                    {
                        Debug.Log($"Bone '{bone.name}' was released.", bone);
                        UiInstance.instance.nameField.text = "";
                        boneController.SetColor(Color.white);
                        boneController.SetObjectUnselectedPosition(boneController.transform.position);
                        boneController.ChangeObjectGrabAndNotInPlace(true);//we set it to true
                        boneController.CheckIfChangedPosIsNearEnough();
                    }
                };
                Debug.Log($"Bone '{bone.name}' initialized with HandGrabInteractable.");
              //  Debug.Log("Bone adding to objectNames", objectNames.Append(bone.name));
                Debug.Log($"Bone '{bone.name}' added to objectNames array.");
                objectNamesList.Add(bone.name);

            }
            Debug.Log("Finished initializing bones for child: " + child.name + ".");
            Debug.Log("Finished initializing bones for child: " + child.name + ". Total bones found: " + objectNamesList.Count);
            Debug.Log("List of bone names: " + string.Join(", ", objectNamesList));
        }

        /*foreach (Transform bone in transform)
        {

        }*/

        Debug.Log("Getting random name: " + GetRandomObjectName());
        teachObj.OnRandomBoneClick();
    }

    public string GetRandomObjectName()
    {
        if (objectNamesList.Count.Equals(0))
        {
            Debug.LogError("No bones found in the object. Please check the hierarchy and ensure bones are present.");
            return null;
        }
       // int randI = Random.Range(0,objectNamesList.Count);
        Debug.Log("Random bone given: "+ objectNamesList[0]);
        return objectNamesList[0];
    }



}
