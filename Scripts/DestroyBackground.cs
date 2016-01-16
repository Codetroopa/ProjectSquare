using UnityEngine;
using System.Collections;

public class DestroyBackground : MonoBehaviour {

	bool visible = true;
	public bool canBeDeleted = false;
	Renderer ren;

	// Use this for initialization
	void Awake () 
	{
		ren = GetComponent<Renderer>();
	}

	IEnumerator Start () 
	{
		yield return new WaitForSeconds (12f);
		canBeDeleted = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ren) 
		{
			visible = ren.isVisible;
		}

		if (!visible && canBeDeleted)
		{
			Destroy(this.gameObject, 15f);
		}
	}
}
