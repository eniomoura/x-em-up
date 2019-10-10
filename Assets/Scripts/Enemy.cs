using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	// Use this for initialization
	public Vector3 playerPos;
	public Vector3 followPos;
	public float timer;
	public float enemySpeed;
	public float fixedDistance;
	public float hitEffectDuration;
	public Sprite spriteAttack;
	public Sprite spriteWindup;
	public Sprite spriteStanding;
	public Sprite hitSpritePlayer;
	public Sprite enemyDead;
	public GameObject powSprite;
	public float windupTimer; //5
	public float attackTimer; //8
	public Rigidbody rbEnemy;
	public GameObject fireball;
	public bool attacking=false;
	public string enemyAI;
	public float punchDamage;
	void Start () {
		rbEnemy = GetComponent<Rigidbody>();

	}


	void Update(){
		timer += Time.deltaTime;
		if(enemyAI=="Yellow"){
			if(timer < windupTimer){ //se o timer atingir 3
				playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
				followPos=playerPos;
				transform.LookAt(followPos);
				if(GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead){
					GetComponentInChildren<SpriteRenderer>().sprite = spriteWindup;
				}
			}else{ //se o  timer atingir 4
				if(attacking=true&&timer>attackTimer){
					attacking=false;
					timer=0;
				}else{
					attacking = true;
					rbEnemy.position += enemySpeed*transform.forward*Time.deltaTime;
					if(GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead){
						GetComponentInChildren<SpriteRenderer>().sprite = spriteAttack;	
					}
				}
			}
		}else if(enemyAI=="Red"){
			if(timer < windupTimer){ //se o timer atingir 3
				playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
				followPos=playerPos;
				transform.LookAt(followPos);
				if(GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead){
					GetComponentInChildren<SpriteRenderer>().sprite = spriteWindup;
				}
			}else{ //se o  timer atingir 4
				if(attacking=true&&timer>attackTimer){
					attacking=false;
					timer=0;
				}else if(attacking==false){
					attacking = true;
					GameObject newFireball=Instantiate(fireball, transform.position, transform.rotation);
					newFireball.transform.position += transform.forward*enemySpeed*Time.deltaTime;
					newFireball.GetComponentInChildren<EnemyAnimation>().caster=gameObject.transform.position;
					if(GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead){
						GetComponentInChildren<SpriteRenderer>().sprite = spriteAttack;	
					}
				}
			}
		}
		Debug.DrawLine(transform.position, followPos);
		//Boundary
		if(rbEnemy.position.z<-5.6f){
			Destroy(gameObject);
		}else if(rbEnemy.position.z>4){
			rbEnemy.position=new Vector3(rbEnemy.position.x, rbEnemy.position.y, 4f);
		}
	}

	void LateUpdate(){
		transform.position.Set(transform.position.x,1.5f,transform.position.z);
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			if(PlayerMovement.isParrying){
				Camera.main.GetComponent<Sound>().PlaySound("parry");
				timer=3;
				HpBar.hp+=0.3f;
				if(PlayerMovement.horizontalDirection == 1 && PlayerMovement.verticalDirection == 0){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == -1 && PlayerMovement.verticalDirection == 0){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == 0 && PlayerMovement.verticalDirection == 1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == 0 && PlayerMovement.verticalDirection == -1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == 1 && PlayerMovement.verticalDirection == -1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 145.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == -1 && PlayerMovement.verticalDirection == -1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 225.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == 1 && PlayerMovement.verticalDirection == 1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
				}
				if(PlayerMovement.horizontalDirection == -1 && PlayerMovement.verticalDirection == 1){
					rbEnemy.rotation = Quaternion.Euler(0.0f, 315.0f, 0.0f);
				}
			}else if(GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead){
				GameObject player=GameObject.FindGameObjectWithTag("Player");
				player.GetComponent<Animator>().enabled=false;
				GameObject hitEffect = Instantiate(powSprite, player.transform.position+Vector3.forward*-0.0001f, player.transform.rotation);
				Destroy(hitEffect, hitEffectDuration);
				HpBar.hp-=punchDamage;
				if(HpBar.hp<=0){
					Camera.main.GetComponent<Sound>().PlaySound("playerDeath");
					Camera.main.GetComponent<AudioSource>().clip=null;
				}else{
					if(transform.position.x<col.transform.position.x){
						print("flipou");
						player.GetComponent<SpriteRenderer>().sprite=hitSpritePlayer;
						player.GetComponent<SpriteRenderer>().flipX=true;
						player.GetComponent<PlayerMovement>().resetAnimation=0.3f;
					}else{
						player.GetComponent<SpriteRenderer>().sprite=hitSpritePlayer;
						player.GetComponent<SpriteRenderer>().flipX=false;
						player.GetComponent<PlayerMovement>().resetAnimation=0.3f;
					}
				}
				Camera.main.GetComponent<Sound>().PlaySound("punch");
			}
		}else if(col.name.Contains("Fireball")){
				Camera.main.GetComponent<Sound>().PlaySound("parryPunch");
				Destroy(col.gameObject,1);
		}else{
			if(attacking&&GetComponentInChildren<SpriteRenderer>().sprite!=enemyDead&&enemyAI=="Yellow"){
				Camera.main.GetComponent<Sound>().PlaySound("parryPunch");
				col.GetComponentInChildren<SpriteRenderer>().sprite = col.GetComponent<Enemy>().enemyDead;
				Destroy(col.gameObject,1);
			}
		}
	}

	void OnDestroy(){
		EnemySpawn.enemies.Remove(gameObject);
	}
}
