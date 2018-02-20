namespace VRTK.Examples
{
    using UnityEngine;

    public class Flashlight : VRTK_InteractableObject
    {
        public Material lightRevealMaterial;
        public AudioClip TurnOnSound;
        public AudioClip TurnOffSound;

        private VRTK_ControllerReference controllerReference;
        private Light spotlight;
        private AudioSource audioSource;
        private VRTK_SnapDropZone filterDropZone;
        private Color savedLightColor;
        private bool revealMode = false;

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
                savedLightColor = spotlight.color;
            }

            if (!filterDropZone)
            {
                filterDropZone = GetComponentInChildren<VRTK_SnapDropZone>();

                if (filterDropZone)
                {
                    filterDropZone.ObjectSnappedToDropZone += FilterSnapped;
                    filterDropZone.ObjectUnsnappedFromDropZone += FilterUnsnapped;
                }
            }
        }

        private void FilterUnsnapped(object sender, SnapDropZoneEventArgs e)
        {
            revealMode = true;
            spotlight.color = Color.white;
        }

        private void FilterSnapped(object sender, SnapDropZoneEventArgs e)
        {
            spotlight.color = Color.magenta;
            revealMode = true;
        }

        protected override void Update()
        {
            base.Update();

            if (revealMode)
            {
                lightRevealMaterial.SetVector("_LightPosition", spotlight.transform.position);
                lightRevealMaterial.SetVector("_LightDirection", -spotlight.transform.forward);
                lightRevealMaterial.SetFloat("_LightAngle", spotlight.spotAngle);
            }
        }
    }
}