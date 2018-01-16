using UnityEngine;
using VRTK;

public class Screwdriver : VRTK_InteractableObject
{
    float spinSpeed = 0f;
    Transform rotator;
    private GameObject blade;
    private Screw currentScrewComponent;

    protected void Start()
    {
        rotator = transform.Find("Blade");
        blade = GameObject.Find("Blade");
    }

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        spinSpeed = 360f;
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
        spinSpeed = 0f;
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "ScrewdriverBase")
        {
            currentScrewComponent = collider.gameObject.transform.parent.gameObject.GetComponent<Screw>();
            blade.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "ScrewdriverBase")
        {
            currentScrewComponent = null;
            blade.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    protected override void Update()
    {
        base.Update();

        rotator.transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));

        if (currentScrewComponent && IsUsing())
            currentScrewComponent.Unscrew(Time.deltaTime);
    }
}
