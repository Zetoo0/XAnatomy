using UnityEngine;
using Meta.XR.MRUtilityKit;
using UnityEngine.XR.ARSubsystems;
//using static OVRAnchor;

public class QrCodeTrackManager : MonoBehaviour
{

    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject[] arrayOfSpawnableObject;
    [SerializeField] TeachManager teachObj;
    public string spawnObjectName;
    private bool isThereSpawnedObject = false;
   public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Oh yeah tracked the qr code: {trackable.TrackableType}");
        if (trackable.TrackableType.Equals(OVRAnchor.TrackableType.QRCode))
        {
            Debug.Log("Its a QR code");
            if (!isThereSpawnedObject)
            {
                Debug.Log("Lets spawn an object");
                Instantiate(skeleton, this.transform);
                teachObj.SetObjectInitiate();
                isThereSpawnedObject = true;
            }
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
