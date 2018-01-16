using UnityEngine;

public class Screw : MonoBehaviour {

    private float screwOffset = 0.08f;
    private bool unscrewed = false;
    private float speed = 0.1f;

    public void Unscrew(float deltaTime)
    {
        if (unscrewed)
            return;

        transform.Translate(new Vector3(speed * deltaTime, 0f, 0f));

        if (transform.localPosition.x >= screwOffset)
        {
            gameObject.AddComponent<Rigidbody>();

            var rigidBody = gameObject.GetComponent<Rigidbody>();
            rigidBody.AddTorque(0, Random.Range(-30f, 30f), Random.Range(-30f, 30f));

            unscrewed = true;
        }
    }
}
