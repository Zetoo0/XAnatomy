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
}
