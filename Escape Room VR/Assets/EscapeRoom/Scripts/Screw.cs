using UnityEngine;
using VRTK;

public class Screw : MonoBehaviour {

    private ScrewedPlate screwPlate;

    private float screwOffset = 0.075f;
    private bool unscrewed = false;
    private float speed = 0.1f;

    protected void Start()
    {
        screwPlate = transform.parent.gameObject.GetComponent<ScrewedPlate>();
    }

    public bool IsScrewed()
    {
        return !unscrewed;
    }

    public void Unscrew(float deltaTime)
    {
        if (unscrewed)
            return;

        transform.Translate(new Vector3(0f, 0f, speed * deltaTime));

        //if (transform.localPosition.x >= screwOffset)
        //{
        //    gameObject.AddComponent<Rigidbody>();

        //    var rigidBody = gameObject.GetComponent<Rigidbody>();
        //    rigidBody.AddTorque(Random.Range(-60f, 60f), Random.Range(-60f, 60f), Random.Range(-60f, 60f));
        //    rigidBody.AddForce(Random.Range(0f, 10f), 0f, 0f);

        //    unscrewed = true;
        //    transform.SetParent(transform.parent.transform.parent.transform);

        //    screwPlate.OnUnscrew(this);
        //}
    }
}
