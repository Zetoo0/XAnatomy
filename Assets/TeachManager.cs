using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class TeachManager : MonoBehaviour
{
    private GameObject organOrBoneParentObject;
   // private SelectedType selected;
    private TMP_Text selectedText;
    private TMP_Text userFeedbackText;
    private TMP_Text correctionText;
    private GameObject randomSelectedObject;
    private GameObject[] objects;
    public static OrganAndBoneTeach instance;
    [SerializeField] private ObjectInitiate objectInitiate;

    private bool teachIsOn;


    public void SetObjectInitiate()
    {
        objectInitiate = GameObject.Find("overview-skeleton(Clone)").GetComponent<ObjectInitiate>();
        Debug.Log("Object initiate set in teach obj: " + objectInitiate);     
    }

    public ObjectInitiate GetObjectInitiate()
    {
        return objectInitiate;
    }

    public bool IsTeachOn()
    {
        return teachIsOn;
    }

    public void SetSelectedText(TMP_Text text)
    {
        selectedText = text;
    }

    public void OnRandomBoneClick()
    {
        Debug.Log("Object Initiate reference: " + objectInitiate);
        if (objectInitiate == null)
        {
            Debug.LogError("ObjectInitiate reference is null. Please ensure it is assigned in the Inspector.");
            return;
        }
        string randomObjectname = objectInitiate.GetRandomObjectName();
        Debug.Log("Random? " + randomObjectname);
        if(randomObjectname == null)
        {
            Debug.LogError("Random object name is null. Please check the ObjectInitiate script.");
            return;
        }
        if(randomObjectname.Equals(""))
        {
            Debug.LogError("Random object name is empty. Please check the ObjectInitiate script.");
            return;
        }
        Debug.Log("Random object name added: " + randomObjectname);
        //selectedText.text = randomObjectname;
        UiInstance.instance.randomBoneField.text = randomObjectname;
        teachIsOn = true;
    }

    public string GetRandomBoneName()
    {
        return UiInstance.instance.randomBoneField.text;
    }

    public void UserSelectBone(string boneName)
    {
        userFeedbackText.text = boneName;
        if (boneName.Equals(selectedText.text))
        {
            correctionText.text = "Correct!";
        }
        else
        {
            correctionText.text = "Wrong!";
        }
    }

}

    /*private void SetParentObject()
    {
        organOrBoneParentObject = GameObject.Find(selectedText.text);
    }


    private void SelectRandomObjToLearn()
    { 
        objectInitiate.GetRandomObjectName();
    }

    public void SetSelectedObjectType(SelectedType selectedType)
    {
        selected = selectedType;
        selectedText.text = SelectedTypeAsString(selected);
    }

    private string SelectedTypeAsString(SelectedType selectedType)
    {
        switch (selectedType)
        {
            case SelectedType.SKELETON:
                return "Skeleton";
            case SelectedType.ARM:
                return "Arm";
            case SelectedType.HAND:
                return "Hand";
            case SelectedType.LEG:
                return "Leg";
            default:
                return "Unknown";
        }
    } 
}

public enum SelectedType
{
    SKELETON, LEG, HAND, ARM, 
}*/