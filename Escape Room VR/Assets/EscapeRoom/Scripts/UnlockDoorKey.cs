using UnityEngine;
using VRTK;
using VRTK.Examples;

public class UnlockDoorKey : MonoBehaviour {

    public GameObject Door;
    public float AnimationSpeed;
    public float Angle;

    private VRTK_SnapDropZone keyDropZone;
    private Vector3 defaultRotation;
    private bool animationStarted = false;

    void Start () {
        if (Door)
        {
            keyDropZone = Door.GetComponentInChildren<VRTK_SnapDropZone>();
            keyDropZone.ObjectSnappedToDropZone += KeySnappedToDropZone;
        }
	}

    private void PlayKeyAnimation()
    {
        // TODO: Play sound
        animationStarted = true;
        defaultRotation = transform.eulerAngles;
    }

    private void OpenDoor()
    {
        animationStarted = false;
        var openableDoorScript = Door.GetComponent<Openable_Door>();

        if (openableDoorScript)
        {
            //openableDoorScript.StartUsing(gameObject);
            openableDoorScript.isUsable = true;
        }
    }

    private void KeySnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        PlayKeyAnimation();
    }

    void Update () {
        if (animationStarted)
        {
            if (transform.localEulerAngles.x < defaultRotation.x + Angle * Mathf.Deg2Rad)
                transform.Rotate(Vector3.down, AnimationSpeed * Time.deltaTime, Space.Self);
            else
                OpenDoor();
        }
    }
}
