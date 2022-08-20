using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject doorOpenUI;
	public GameObject TheDoor;
	public AudioSource CreakSound;

	void Update () 
    {
		TheDistance = PlayerRayCast.DistanceFromTarget;
	}

	void OnMouseOver () 
    {
		if (TheDistance <= 2) {
			doorOpenUI.SetActive (true);
		}
		if (Input.GetKeyDown(KeyCode.E) && (TheDistance <= 2)) 
        {
				this.GetComponent<BoxCollider>().enabled = false;
				doorOpenUI.SetActive(false);
				TheDoor.GetComponent<Animation> ().Play ("DoorOpen");
				CreakSound.Play();
		}
	}

	void OnMouseExit() 
    {
		doorOpenUI.SetActive (false);
	}
}