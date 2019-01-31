using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public float currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public AudioClip hurtClip;
	public float flashSpeed = 1f;                               // The speed the damageImage will fade at.
	public Color dmgColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public Color healColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	bool healed;												// True when the player is being healed;


	void Awake ()
	{
		playerAudio = gameObject.GetComponent <AudioSource> ();

		// Set the initial health of the player.
		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
	}


	void Update ()
	{

		if (!healed && !damaged) {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = dmgColor;
		}

		// Reset the damaged flag.
		damaged = false;

		if(healed)
		{
			damageImage.color = healColor;
		}

		// Reset the damaged flag.
		healed = false;

		if(Input.GetKeyDown(KeyCode.Q)){
			this.TakeDamage(10.0f);
		}
	}


	public void TakeDamage (float amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		playerAudio.clip = hurtClip;
		playerAudio.Play();
		playerAudio.clip = null;

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}

	public void Heal(float amount){
		healed = true;

		currentHealth += amount;

		if (currentHealth > 100.0f) {
			currentHealth = 100.0f;
		}

		healthSlider.value = currentHealth;
	}

	public void UseEnergy(float amount){
		currentHealth -= amount;
		healthSlider.value = currentHealth;
	}


	public void Death ()
	{
		isDead = true;
	}       
}