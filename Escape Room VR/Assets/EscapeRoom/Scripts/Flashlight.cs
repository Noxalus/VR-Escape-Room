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

        protected override void OnEnable()
        {
            base.OnEnable();
            controllerReference = null;
            interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
            {
                var hapticStrength = 10;
                VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
            }
        }

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);

            if (spotlight)
            {
                audioSource.clip = TurnOnSound;
                audioSource.Play();

                spotlight.enabled = true;
            }
        }

        public override void StopUsing(VRTK_InteractUse usingObject)
        {
            base.StopUsing(usingObject);

            if (spotlight)
            {
                audioSource.clip = TurnOnSound;
                audioSource.Play();

                spotlight.enabled = false;
            }
        }

        protected void Start()
        {
            audioSource = GetComponent<AudioSource>();
            spotlight = GameObject.Find("Spotlight").GetComponent<Light>();
        }
    }
}