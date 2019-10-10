using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	public float timer;
	public float spawnTime;
	public GameObject enemyYellow;
	public GameObject enemyRed;
	public PlayerMovement movement;
	public static List<GameObject> enemies;
	public float maxEnemies;
	public float redChance;
	// Use this for initialization
	void Start () {
		movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		enemies = new List<GameObject>();
		timer=spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer>spawnTime && enemies.Count<maxEnemies){
			bool collides=true;
			float spawnAttempts=0;
			do{
				float spawnX=Random.Range(-movement.boundaries.z,movement.boundaries.z);
				float spawnZ=Random.Range(movement.boundaries.y,movement.boundaries.x);
				collides = Physics.CheckSphere(new Vector3(spawnX, .5f, spawnZ), 1, ~0, QueryTriggerInteraction.Collide)==false;
				if(collides){
					GameObject newEnemy;
					if(Random.value<redChance){
						newEnemy = Instantiate(enemyRed, new Vector3(spawnX,1.5f,spawnZ), Quaternion.identity);
					}else{
						newEnemy = Instantiate(enemyYellow, new Vector3(spawnX,1.5f,spawnZ), Quaternion.identity);
					}
					enemies.Add(newEnemy);
				}else{
					spawnAttempts++;
				}
			}while(!collides||spawnAttempts>100);
			if(spawnAttempts>100){
				Debug.LogError("ERROR SPAWNING ENEMY");
			}
			timer=0;
		}
		timer+=Time.deltaTime;
	}
}
