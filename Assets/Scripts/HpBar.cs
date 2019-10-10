using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {
	public static float hp;
	public float maxHp;
	public RectTransform hpBar;
	// Use this for initialization
	void Start () {
		hp=1;
		hpBar = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.localScale = new Vector3(hp, 1f, 1f);
		if(hp>maxHp){
			hp=maxHp;
		}
	}
}
