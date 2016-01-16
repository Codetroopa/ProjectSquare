using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	Obstacle obs;
	Vector3 center;

	// Use this for initialization
	void Start () 
	{
		obs = Obstacle.obs;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		// Gets the player who entered and increases its speed
		PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
		pm.Speed += 1f;

		// Re-enables Obstacle spawning
		obs.canSpawn = true;

		// Checkpoint Effects
		center = (other.bounds.center + new Vector3(1f, 0f,0f));
		Transform clone = Instantiate (obs.respawnParticles, center, Quaternion.identity) as Transform;
		Destroy (clone.gameObject, 5f);
	}
}
