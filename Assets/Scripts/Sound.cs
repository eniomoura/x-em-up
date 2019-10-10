using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	public AudioClip dash;
	public AudioClip footstep;
	public AudioClip parry;
	public AudioClip punch;
	public AudioClip parryPunch;
	public AudioClip playerDeath;
	public AudioClip FriendlyFire;

	public void PlaySound(string sound){
		switch(sound){
			case "dash":
				GetComponents<AudioSource>()[1].PlayOneShot(dash);
			break;
			case "footstep":
				GetComponents<AudioSource>()[1].PlayOneShot(footstep);
			break;
			case "parry":
				GetComponents<AudioSource>()[1].PlayOneShot(parry);
			break;
			case "punch":
				GetComponents<AudioSource>()[1].PlayOneShot(punch);
			break;
			case "parryPunch":
				GetComponents<AudioSource>()[1].PlayOneShot(parryPunch);
			break;
			case "playerDeath":
				GetComponents<AudioSource>()[1].PlayOneShot(playerDeath);
			break;
			case "FriendlyFire":
				GetComponents<AudioSource>()[1].PlayOneShot(FriendlyFire);
			break;
		}
	}
}
