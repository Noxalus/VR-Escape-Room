using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class LightSwitch : MonoBehaviour {
    private VRTK_Button_UnityEvents buttonEvents;
    private Light roomLight;

    private void Start()
    {
        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();

        buttonEvents.OnPushed.AddListener(handlePush);
        roomLight = GameObject.Find("RoomLight").GetComponent<Light>();
    }

    private void handlePush(object sender, Control3DEventArgs e)
    {
        VRTK_Logger.Info("Pushed");
        roomLight.enabled = !roomLight.enabled;
    }
}