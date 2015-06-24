using UnityEngine;
using System.Collections;

public class ChassisCreates : MonoBehaviour {
	private Color currentColor; 
	public bool set_target;
	void Start () {
		set_target = false;//不可以移动的
		currentColor=GetComponent<Renderer>().material.color;
	}
	
	//停用
	public void Deactivate(){  
		set_target = true;
		iTween.ColorTo (gameObject, currentColor, .4f);
			  
	}
	//激活
	public void Activate(){  
		set_target = false;//有碰到 不可以用
		GetComponent<Renderer>().material.color=Color.red; 
	}

	void Update () {
	
	}
}
