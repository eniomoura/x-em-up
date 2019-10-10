using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public GameObject target;
	public bool fireBallFollowsPlayer;
	public float fireballSpeed;
	public float fireballDamage;
	public float spawnDistance;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 4);
		target=GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(target.transform.position);
		transform.position += transform.forward*spawnDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if(fireBallFollowsPlayer){
			transform.LookAt(target.transform.position);
		}
		transform.position += transform.forward*fireballSpeed*Time.deltaTime;
		if(HpBar.hp<=0){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag=="Player"){
			target.GetComponent<Animator>().enabled=false;
			target.GetComponent<SpriteRenderer>().sprite=target.GetComponent<PlayerMovement>().spriteHit;
			col.GetComponent<PlayerMovement>().resetAnimation=.3f;
			HpBar.hp-=fireballDamage;
			if(HpBar.hp<=0){
				Camera.main.GetComponent<Sound>().PlaySound("playerDeath");
				Camera.main.GetComponent<AudioSource>().clip=null;
			}
			if(!Camera.main.GetComponents<AudioSource>()[1].isPlaying){
				Camera.main.GetComponent<Sound>().PlaySound("FriendlyFire");
			}
			Destroy(gameObject);
		}else{
			if(!Camera.main.GetComponents<AudioSource>()[1].isPlaying){
				Camera.main.GetComponent<Sound>().PlaySound("FriendlyFire");
			}
			col.GetComponentInChildren<SpriteRenderer>().sprite = col.GetComponent<Enemy>().enemyDead;
			Destroy(col.gameObject,1);
		}
	}
}
