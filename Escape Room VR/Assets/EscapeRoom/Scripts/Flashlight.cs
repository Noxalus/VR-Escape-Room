namespace VRTK.Examples
{
    using UnityEngine;

    public class Flashlight : VRTK_InteractableObject
    {
        private VRTK_ControllerReference controllerReference;
        private Light spotlight;
        public AudioClip TurnOnSound;
        public AudioClip TurnOffSound;
        private AudioSource audioSource;

        private bool turnedOn = false;

        public override void Grabbed(VRTK_InteractGrab grabbingObject)
        {
            base.Grabbed(grabbingObject);
            controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);
            controllerReference = null;
        }

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            if (spotlight)
            {
                turnedOn = !turnedOn;
                spotlight.enabled = turnedOn;
                audioSource.clip = turnedOn ? TurnOnSound : TurnOffSound;

                audioSource.Play();
            }
        }

        protected void Start()
        {
            if (!audioSource)
                audioSource = gameObject.AddComponent<AudioSource>();

            if (!spotlight)
            {
                spotlight = GetComponentInChildren<Light>();
                turnedOn = spotlight.enabled;
            }
        }
    }
}