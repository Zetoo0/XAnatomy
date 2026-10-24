using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class OrganAndBoneTeach : MonoBehaviour
{
    private GameObject organOrBoneParentObject;
    private SelectedType selected;
    private TMP_Text selectedText;
    private GameObject randomSelectedObject;
    private GameObject[] objects;
    private void Start()
    {
        if(organOrBoneParentObject == null)
        {
            Debug.LogError("Organ or bone gameobject doesnt exists");
            return;
        }
    }
    private void SetParentObject()
    {
        organOrBoneParentObject = GameObject.Find(selectedText.text);
    }

    private void InitiateObjects()
    {
        foreach(Transform obj in organOrBoneParentObject.transform)
        {
            objects.Append(obj.gameObject);
        }
    }

    private void SelectRandomObjToLearn()
    { 
        randomSelectedObject = objects[0];
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
}