using Unity.VisualScripting;
using UnityEngine;

public class UiInstance : MonoBehaviour
{
    public static UiInstance instance;

    void Awake()
    {
        instance = this;
    }

    public TMPro.TMP_Text nameField;
    public TMPro.TMP_Text randomBoneField;
    public TMPro.TMP_Text userSelectedField;
    public TMPro.TMP_Text successField;
    public Vector3 selectedObjectCurrentPosition;
}
