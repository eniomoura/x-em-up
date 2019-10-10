using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

	public GameObject player;
	public Vector3 caster;

	// Use this for initialization
	void Start () {
		player =GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.name.Contains("Fireball")){
			Debug.DrawLine(transform.position, caster, Color.red);
			if(caster.x>transform.position.x){
				transform.rotation=Quaternion.Euler(0,180,0);
			}else{
				transform.rotation=Quaternion.Euler(0,0,0);
			}
		}else{
			if(player.transform.position.x>transform.position.x){
				transform.rotation=Quaternion.Euler(0,0,0);
			}else{
				transform.rotation=Quaternion.Euler(0,180,0);
			}
		}
	}
}
