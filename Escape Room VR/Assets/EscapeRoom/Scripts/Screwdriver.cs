using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Screwdriver : VRTK_InteractableObject
{
    float spinSpeed = 0f;
    Transform rotator;
    private Animator animator;
    private GameObject blade;
    private GameObject currentScrewScript;
    private float screwOffset = 0.08f;

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

        StopUnscrewingAnimation();
    }

    private void StopUnscrewingAnimation()
    {
        //if (animator)
        //    animator.speed = 0;
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "ScrewdriverBase")
        {
            animator = collider.gameObject.GetComponentInParent<Animator>();
            currentScrew = collider.gameObject.transform.parent.gameObject;

            blade.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "ScrewdriverBase")
        {
            StopUnscrewingAnimation();
            animator = null;
            currentScrew = null;
            blade.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (animator)
        {
            if (IsUsing())
            {
                animator.speed = 1;

                // Animation finished?
                //if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
                //    animator.enabled = false;
            }
            //else
            //    animator.speed = 0;
        }

        rotator.transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));

        if (currentScrew && IsUsing())
        {
            currentScrew.transform.Translate(new Vector3(0.1f * Time.deltaTime, 0f, 0f));

            if (currentScrew.transform.localPosition.x >= screwOffset)
                currentScrew.AddComponent<Rigidbody>();
        }
    }
}
