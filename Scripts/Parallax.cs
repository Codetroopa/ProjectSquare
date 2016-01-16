using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;
	public int length;

	private Transform cam;
	private Vector3 previousCamPos;

	// Is called before Start
	void Awake () {
		// References
		cam = Camera.main.transform;
		length = backgrounds.Length;
	}

	// Use this for initialization
	void Start () {
		// Previous Frame
		previousCamPos = cam.position;

		// Assigning parallax scales
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// Set a target x position
			float targetPosX = backgrounds[i].position.x + parallax;

			Vector3 targetPos = new Vector3(targetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// Fade between current pos and new pos
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, targetPos, smoothing * Time.deltaTime);

		}
		previousCamPos = cam.position;
	}
}
