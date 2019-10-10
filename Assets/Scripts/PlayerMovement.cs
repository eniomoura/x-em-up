using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[Header("Variables")]	public float playerSpeed; //velocidade base do player
	public float dashSpeed; //velocidade extra do dash
	public Rigidbody rdCharacter;
	public float timerDash;//contador do dash
	public float timerParry;//contador do parry
	public float timerCooldown;//tempo de espera até poder fazer dash ou parry
	public static bool isParrying;
	public static bool isDashing;
	public static float horizontalDirection;
	public Sprite spriteParry;
	public Sprite spriteParrying;
	public Sprite spriteIdle;
	public Sprite spriteHit;
	public Sprite deadSprite;
	public static float verticalDirection;
	public float parryCooldown;
	public float dashStaminaCost;
	public float parryStaminaCost;
	public float parryAnimationHalf;
	public bool isFinishingParry=false;
	public float parryAnimationFinish;
	public RuntimeAnimatorController idleAnimation;
	public RuntimeAnimatorController walkAnimation;
	public bool canDieOnHoles;
	public Vector3 boundaries;
	public float resetAnimation;

	void Start(){
		rdCharacter = GetComponent<Rigidbody>();
		resetAnimation=-1;
	}

	void Update() {
		if(!PlayerBehaviour.isDead){
			if(resetAnimation>0){
				resetAnimation -= Time.deltaTime;
			if(resetAnimation==0){
				resetAnimation-=0.01f;
			}
		}else if(resetAnimation<0){
				Camera.main.transform.position=new Vector3(0,6,-8.93f);
				GetComponent<Animator>().enabled=true;				
		}
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			if(moveHorizontal<0){
				horizontalDirection=-1;
			}else if(moveHorizontal>0){
				horizontalDirection=1;
			}else if(moveHorizontal==0){
				horizontalDirection=0;
			}

			if(moveVertical<0){
				verticalDirection=-1;
			}else if(moveVertical>0){
				verticalDirection=1;
			}else if(moveVertical==0){
				verticalDirection=0;
			}
			if(verticalDirection==0&&horizontalDirection==0&&resetAnimation<=0){
				GetComponent<Animator>().enabled=true;
				GetComponent<Animator>().runtimeAnimatorController=idleAnimation;
			}else if(resetAnimation<=0){
				GetComponent<Animator>().enabled=true;
				GetComponent<Animator>().runtimeAnimatorController=walkAnimation;
			}
			
			
			if(isFinishingParry==false){ //player movement
			rdCharacter.position += moveHorizontal*playerSpeed*Vector3.right;
			rdCharacter.position += moveVertical*playerSpeed*Vector3.forward;
			}
			if(horizontalDirection<0){
				GetComponent<SpriteRenderer>().flipX=true;
			}else{
				GetComponent<SpriteRenderer>().flipX=false;
			}

			if(StaminaBar.stamina>=dashStaminaCost){//player dash
				if(Input.GetKeyDown(KeyCode.LeftShift)){
					StaminaBar.stamina = StaminaBar.stamina - dashStaminaCost;
					Camera.main.GetComponent<Sound>().PlaySound("dash");
						if(moveHorizontal!=0){
							if(moveVertical!=0){
								while(timerDash<timerCooldown){
									rdCharacter.position += dashSpeed*playerSpeed*new Vector3(1*horizontalDirection,0,1*verticalDirection);
									timerDash += Time.deltaTime;
									isDashing=true;
							}
								
							}else{
								while(timerDash<timerCooldown){
									rdCharacter.position += dashSpeed*playerSpeed*horizontalDirection*Vector3.right;
									timerDash += Time.deltaTime;
									isDashing=true;
								}
							}
						}else if(moveVertical!=0){
							while(timerDash<timerCooldown){
								rdCharacter.position += dashSpeed*playerSpeed*verticalDirection*Vector3.forward;
								timerDash += Time.deltaTime;
								isDashing=true;
							}
						}
					isDashing=false;	
					timerDash = 0;	
				}
			}//fim player Dash

			//parry
			
			if(Input.GetKeyDown(KeyCode.Space)){
				if(StaminaBar.stamina>=parryStaminaCost && !isFinishingParry){
					isParrying=true;
					isFinishingParry=true;
					GetComponent<Animator>().enabled=false;
					GetComponent<SpriteRenderer>().sprite=spriteParry;
					resetAnimation=100;
					StaminaBar.stamina = StaminaBar.stamina - parryStaminaCost;
				}
			}
			if(isFinishingParry){
				timerParry += Time.deltaTime;
				if(timerParry > parryAnimationHalf && timerParry<parryAnimationFinish){ 
					isParrying=false;
					GetComponent<Animator>().enabled=false;
					GetComponent<SpriteRenderer>().sprite=spriteParrying;
					resetAnimation=100;
				}
				if(timerParry > parryAnimationFinish){
					isFinishingParry=false;
					GetComponent<Animator>().enabled=false;
					GetComponent<SpriteRenderer>().sprite=spriteParrying;
					resetAnimation=-1;
					timerParry=0;
				}
			}
		}else{ // is dead
			GetComponent<Animator>().enabled=false;
			GetComponent<SpriteRenderer>().sprite=deadSprite;
		}
		//boundary x-upper boundary y-lower boundary z-side boundary
		if(rdCharacter.position.z>boundaries.x){
			rdCharacter.position= new Vector3(rdCharacter.position.x, rdCharacter.position.y, boundaries.x);
		}else if(rdCharacter.position.z<boundaries.y){
			if(canDieOnHoles){
				HpBar.hp=-100;
			}else{
				rdCharacter.position=new Vector3(rdCharacter.position.x,rdCharacter.position.y,boundaries.y);
			}
		}
		if(rdCharacter.position.x>boundaries.z){
				rdCharacter.position=new Vector3(boundaries.z, rdCharacter.position.y, rdCharacter.position.z);
		}else if(rdCharacter.position.x<boundaries.z*-1){
				rdCharacter.position=new Vector3(boundaries.z*-1, rdCharacter.position.y, rdCharacter.position.z);
		}
		rdCharacter.position= new Vector3(rdCharacter.position.x,1.5f,rdCharacter.position.z);
	}
}
	

