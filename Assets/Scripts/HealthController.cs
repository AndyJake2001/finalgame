using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    public float health = 1f;
    public float maxHealth = 1f;

    private CollidableEntityController mainController;

    // Start is called before the first frame update
    void Start() {
        mainController = gameObject.GetComponent<CollidableEntityController>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void TriggerCollision(Rigidbody offender) {
        // Get the healthbar.
        Image healthBar = mainController.healthBar;

        if (healthBar != null) {
            // Reduce the health based on velocity - TODO.
            health -= 0.1f /* * offender.velocity.magnitude*/;

            // Reduce healthbar amount.
            // Recolor based on the health.
            healthBar.fillAmount = health / maxHealth;
            if (healthBar.fillAmount < 0.2f) {
                healthBar.color = Color.red;
            } else if (healthBar.fillAmount < 0.4f) {
                healthBar.color = Color.yellow;
            } else {
                healthBar.color = Color.green;
            }
        }

        // Play a crash sound at the crash site.
        var sound = mainController.crashSound;
        AudioSource.PlayClipAtPoint(sound.clip, gameObject.transform.position);

        // Destroy the object if no health remaining.
        if (health <= 0) {
            // Play a death animation at the death site. 
            var anim = mainController.deathAnimation;
            var death = Instantiate(anim, gameObject.transform.position, Quaternion.identity);
            death.Play();

            // Destroy the death animation after a second (after it's finished).
            Destroy(death, 1f);

            mainController.NotifyDeath();

            // Destroy the entity.
            Destroy(gameObject);

            // Get the offender's health controller.
            var offenderHealth = offender.GetComponent<HealthController>();

            // Heal the offender when the victim is destroyed.
            if (offenderHealth.health <= offenderHealth.maxHealth - 0.1f) {
                offenderHealth.health += 0.1f;
            }
        }
    }
}
