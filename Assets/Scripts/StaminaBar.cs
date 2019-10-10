using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {
	public static float stamina;
	public float staminaRegen;
	public float maxStamina;
	public RectTransform staminaBar;
	// Use this for initialization
	void Start () {
		maxStamina = 1f;
		stamina = 1f;
		staminaBar = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		staminaBar.localScale = new Vector3(stamina, 1, 1);
		if(PlayerMovement.horizontalDirection==0&&PlayerMovement.verticalDirection==0){
			stamina+=staminaRegen*Time.deltaTime*2;
		}else{
			stamina+=staminaRegen*Time.deltaTime;
		}
		if(stamina>maxStamina){
			stamina=maxStamina;
		}
	}
}
