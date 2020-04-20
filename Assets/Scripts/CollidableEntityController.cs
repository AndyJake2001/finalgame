using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollidableEntityController : MonoBehaviour {
    public GameObject target;
    public Image healthBar;
    public ParticleSystem deathAnimation;
    public AudioSource crashSound;

    public float speed = 10f;
    public float turnSpeed = 100f;

    [SerializeField]
    private bool isPlayer = false;

    private Rigidbody curBody;

    // Start is called before the first frame update
    void Start() {
        curBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {
        if (PauseMenu.isPaused) {
            return;
        }

        if (isPlayer) {
            // Do player input.
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Translate based on input, rotate based on input.
            // Translate right instead of foward because the lowpoly unity prefab
            // is very strange, and seems to be rotated.
            transform.Translate(Vector3.right * Time.deltaTime * speed * vertical);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontal);
        } else {
            if (target != null) {
                // Get the look direction. Don't account for up/down.
                Vector3 diff = (target.transform.position - transform.position);
                diff.y = 0;
                Vector3 lookDirection = diff.normalized;

                if (lookDirection != Vector3.zero) {
                    // Add the force to the entity.
                    curBody.AddForce(lookDirection * speed);

                    // Rotate the entity to the target.
                    Quaternion rot = Quaternion.LookRotation(diff) * Quaternion.Euler(-Vector3.up * 90);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
                }
            } else {
                var entities = GameObject.FindGameObjectsWithTag("Entity");
                target = entities[Random.Range(0, entities.Length - 1)];
            }
        }

        // Move the health bar above the player.
        if (healthBar != null) {
            healthBar.transform.position = transform.position + Vector3.up * 2;
        }
    }

    public void NotifyDeath() {
        if (isPlayer) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
