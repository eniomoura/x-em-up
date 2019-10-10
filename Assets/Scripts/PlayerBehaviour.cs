using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	public GameObject gameOverText;
	public float gameOverTimer;
	public Sprite deadSprite;
	public static bool isDead;
	public AudioClip ost;

	// Update is called once per frame
	void Update () {
		if(HpBar.hp<=0){
			isDead=true;
			GetComponent<Animator>().enabled=false;
			GetComponent<SpriteRenderer>().sprite=deadSprite;
			gameOverText.SetActive(true);
			gameOverTimer+=Time.deltaTime;
			HpBar.hp=0;
			StaminaBar.stamina=0;
			ResetGame(2);
		}
	}

	void ResetGame(float seconds){
		foreach(GameObject enemy in EnemySpawn.enemies){
			Destroy(enemy);
		}
		if(gameOverTimer>seconds){
			isDead=false;
			gameOverTimer=0;
			transform.position=Vector3.up*1.25f;
			GetComponent<Animator>().enabled=true;
			HpBar.hp=1;
			StaminaBar.stamina=1;
			Camera.main.GetComponent<AudioSource>().clip=ost;
			Camera.main.GetComponent<AudioSource>().Play();
			gameOverText.SetActive(false);
		}
	}
}
