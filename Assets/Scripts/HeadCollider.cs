using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour {
    private Rigidbody parentBody;

    // Start is called before the first frame update
    void Start() {
        // Get the parent body.
        parentBody = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        // Check whether colliding with an entity or not.
        if (other.CompareTag("Entity")) {
            // Get the other game object.
            GameObject parent = other.gameObject;

            // Trigger the collision with the other body.
            parent.gameObject.GetComponent<HealthController>().TriggerCollision(parentBody);
        }
    }
}
