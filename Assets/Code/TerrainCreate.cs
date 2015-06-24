using UnityEngine;
using System.Collections;

public class TerrainCreate : MonoBehaviour {

	private Color currentColor;
	void Start () {
		currentColor=GetComponent<Renderer>().material.color;
	}
	
	 
	void Update () {
	
	}

	//停用
	void Deactivate(){ 
		iTween.ColorTo(gameObject,currentColor,.4f);
	}
	//激活
	void Activate(){ 
		GetComponent<Renderer>().material.color=Color.red; 
	}
 
	//地形拖拽
	public void  over_TargetSet_terrain(Transform transform_builds)
	{  
		if (transform_builds == this.transform) {
				Activate ();
			print ("地面");//这里开始做 通知  ui弹出
		 	}
	  
	}
	//换一个地形拖拽
	public void  before_TargetCancel(Transform transform_terrains_old)
	{ 
		Deactivate ();
	   // print ("输出 before_TargetCancel 最顶层"+this.transform.name);
	}
}
