using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
	public static Obstacle obs; 						// So other classes can reference the Obstacle class
	
	public GameObject[] obstacles = new GameObject[3];
	public GameObject checkpoint;
	public Transform respawnParticles;
	public Transform Parent;
	public float distance = 0f;
	[HideInInspector] 
	public bool canSpawn = true;

	private int score;
	private float exactScore;
	private int minRange = 0;							// Determines the minimum range of obstacles to spawn (the higher the number, the harder the obstacle)
	private int maxRange = 6;							// Determines the highest difficulty obstacle to spawn
	private float spawnDistance;
	private Vector3 previousPosition = Vector3.zero;
	private Camera cam;
	private GameObject playerObj;

	void Awake ()
	{
		obs = this;
		cam = Camera.main;
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		spawnDistance = 20f;
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	IEnumerator Start ()
	{
		while (true) {
			float waitTime = Random.Range (0.8f, 1.9f);
			yield return new WaitForSeconds (waitTime);
			//SpawnPillar (playerObj.transform);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Calculates the distance travelled in the x direction and updates variables accordingly.
		distance += XDifference (playerObj.transform.position, previousPosition);
		exactScore += XDifference (playerObj.transform.position, previousPosition);
		score = (int)exactScore;
		previousPosition = playerObj.transform.position;

		// Checkpoint reached
		if (score == 250) 
		{
			exactScore += 1f;
			score += 1;
			NextLevel (playerObj.transform, 0);
		}

		// Spawns a Pillar if a certain distance is acheived
		if (distance >= spawnDistance) {
			SpawnObstacle (playerObj.transform);
			distance = 0f;
		}
	}

	public float XDifference (Vector3 vec1, Vector3 vec2)
	{
		return vec1.x - vec2.x;
	}

	// Spawns a random obstacle
	public void SpawnObstacle (Transform player)
	{
		// Random number and Vector3 coordinates
		int rand = Random.Range (minRange, maxRange);
		GameObject obj = null;
		float xCoord, yCoord, zCoord;
		float cameraExtend = cam.orthographicSize * Screen.width / Screen.height;
		xCoord = player.position.x + cameraExtend + (cameraExtend / 10f);
		yCoord = -1;
		zCoord = player.position.z;

		// Obstacle combinations

		// EASY OBSTACLES
		if (canSpawn) {
			if (rand == 0) {					// Single small pillar
				obj = Instantiate (obstacles [0], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (12f, 13.5f);		
			}

			if (rand == 1) {					// Single medium pillar
				obj = Instantiate (obstacles [1], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (17f, 19.5f);		
			}

			if (rand == 2) {					// Single large pillar
				obj = Instantiate (obstacles [2], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (18f, 19.5f);		
			}

			if (rand == 3) {					// Large pillar followed by small pillar closeby
				obj = Instantiate (obstacles [2], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				obj.transform.parent = Parent;
				obj = Instantiate (obstacles [0], new Vector3 (xCoord + 7.5f, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (19f, 20.5f);		
			}

			if (rand == 4) {					// Small pillar followed by large pillar closeby
				obj = Instantiate (obstacles [0], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				obj.transform.parent = Parent;
				obj = Instantiate (obstacles [2], new Vector3 (xCoord + 7.5f, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (19f, 20.5f);		
			}

			if (rand == 5) {					// 2 small pillars
				obj = Instantiate (obstacles [0], new Vector3 (xCoord, yCoord, zCoord), Quaternion.identity) as GameObject;
				obj.transform.parent = Parent;
				obj = Instantiate (obstacles [0], new Vector3 (xCoord + 3f, yCoord, zCoord), Quaternion.identity) as GameObject;
				spawnDistance = Random.Range (16f, 18.5f);		
			}


			// MEDIUM OBSTACLES

			// Put the obstacle into a 'obstacle container'
			obj.transform.parent = Parent;

		}
	}

	public void NextLevel (Transform player, int difficulty) 
	{
		// Camera and player positions
		float xCoord, yCoord, zCoord;
		float cameraExtend = cam.orthographicSize * Screen.width / Screen.height;
		xCoord = player.position.x + cameraExtend + (cameraExtend / 10f);
		yCoord = -1;
		zCoord = player.position.z;
		PlayerMovement pm = player.GetComponent<PlayerMovement>();

		// Stops obstacle production and creates and checkpoint prefab
		canSpawn = false;
		GameObject obj = Instantiate (checkpoint, new Vector3 (xCoord + 25f, yCoord, zCoord), Quaternion.identity) as GameObject;
		obj.transform.parent = Parent;
		distance = 0f;
		spawnDistance = 50f;

		// Changes the player speed and which obstacles can now be spawned
		if (difficulty == 0) 
		{
			minRange = 0;
			maxRange = 6;
		}
	}
}








