using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.XR.ARSubsystems;
//using static OVRAnchor;

public class QrCodeTrackManager : MonoBehaviour
{

    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject[] arrayOfSpawnableObject;
    public string spawnObjectName;
    private bool isThereSpawnedObject = false;
   public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Hell yeah tracked the kuár kód: {trackable.TrackableType}");
        if (trackable.TrackableType.Equals(OVRAnchor.TrackableType.QRCode))
        {
            Debug.Log("Its a QR code");
            if (!isThereSpawnedObject)
            {
                Debug.Log("Lets spawn an object");
                Instantiate(skeleton, this.transform);
                isThereSpawnedObject = true;
            }
         //   Instantiate(skeleton);
        }
    }

    public void SetSpawnObject(string name)
    {
        spawnObjectName = name;
    }

    public string GetSpawnObjectName(string name)
    {
        return spawnObjectName;
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.Log("LecsaPunk");
    }
}
