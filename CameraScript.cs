using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float yOffset = 1f;
	public float xOffset = 1f;
	public Transform myTransform;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
			transform.position = new Vector3(myTransform.position.x + xOffset, transform.position.y, transform.position.z);
	}
}
