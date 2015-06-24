using UnityEngine;
using System.Collections;

public class buildCreate : MonoBehaviour {
	 
	private Color currentColor;
	public int set_target;//是否可以拖拽 或 不能托，或显示炫
	public bool drag;//是否 移动过
 	public Transform []build_date;//地图数据 
	public int []x_old;//地图数据 
	public int []y_old;//地图数据 
	public int Chassis_size;//底座全部数量 
	void Start () {
		set_target = 0;//没被点过
		currentColor=GetComponent<Renderer>().material.color;
		drag = false;  
		start_Clone (1);
	}

	public void start_Clone(int ii)
	{
		Vector3 vr = this.transform.position;
		for (int i = 0; i<Chassis_size; i++) {
			int xx =(int)(vr.x+x_old[i]);
			int yy =(int)(vr.z+y_old[i]);
			GameModel.getInstance().scencemodel.clone[xx,yy]  = ii; 
		} 
	}
	//停用
	void Deactivate(){ 
		iTween.ColorTo(gameObject,currentColor,.4f);
	}
	//激活
	void Activate(){ 
		GetComponent<Renderer>().material.color=Color.red; 
	}

	void Double_Activate(){ 
		GetComponent<Renderer>().material.color=Color.green; 
	}
	void Update () {
		 
	}
 

	//创建周边 底座
	public void CreateChassis(Transform touchTransform)
	{ 
		for (int i = 0; i<Chassis_size; i++) {
			Transform Chassis_build = touchTransform.GetComponent<InstantiationPrefabs> ().CreateChassis (this.transform.position);
			build_date[i] = Chassis_build; 
		} 
	}
	//删除周边底座
	public void DeleChassis(Transform touchTransform)
	{
		for (int i = 0; i<Chassis_size; i++) {
			touchTransform.GetComponent<InstantiationPrefabs> ().DespawnChassis (build_date[i]); 
		 }
	}

	//可以被拖拽
	public void over_TargetSet_build(GameObject UpGame)
	{  
		set_target = 2;//第二次点 可以被拖拽
		Double_Activate ();
	//	print ("输出 over_TargetSet_build 最顶层"+UpGame.name);
	}

	//取消之前内炫并取消拖拽
	public void before_TargetSet(GameObject UpGame)
	{  
		set_target = 0;//没被点
		drag = false;
		Deactivate ();
	//	print ("输出 before_TargetSet 最顶层"+UpGame.name);
	}

	//增加现在模块内炫的
	public void over_Shader_Set(GameObject UpGame)
	{  
		set_target = 1;//第一次点
		Activate ();
	//	print ("输出 over_Shader_Set 最顶层"+UpGame.name);
	}
	public Vector3 OnDrag_build(Vector3 vectors,Vector3 cursorWorldPoints,Vector3 cursorWorldPointUp,Transform get_Chassis_build)
	{ 
		drag = true;
		Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(vectors); 
		cursorWorldPoints.y =0.0f;
		cursorWorldPointUp.y = 0.0f;
		cursorWorldPoint.y = 1.0f;
		//底座
		Vector3 get_Chassis_builds = transform.position = cursorWorldPoint-cursorWorldPoints + cursorWorldPointUp;  
		get_Chassis_builds.y = .1f;

		get_Chassis_builds.x = ((int)get_Chassis_builds.x * 2) / 2;
		get_Chassis_builds.z = ((int)get_Chassis_builds.z * 2) / 2;
		get_Chassis_build.position = get_Chassis_builds; //底座最终坐标
		int xx = (int)get_Chassis_builds.x;
		int yy = (int)get_Chassis_builds.z;
		bool targets = true;
		for (int i = 0; i<Chassis_size; i++) {
			build_date[i].position = new Vector3 (xx+x_old[i],.1f,yy+y_old[i]);  
			int x1 =(int)(xx+x_old[i]);
			int y1 =(int)(yy+y_old[i]);
			int clone =	GameModel.getInstance().scencemodel.clone[x1,y1];
			if (clone == 1) {
				targets = false;
			}  
	     }
		if (targets) {
						for (int i = 0; i<Chassis_size; i++) {
								build_date [i].GetComponent<ChassisCreates> ().Deactivate ();	
								get_Chassis_build.GetComponent<ChassisCreates> ().Deactivate ();
						}
				} else {
						for (int i = 0; i<Chassis_size; i++) {
								build_date [i].GetComponent<ChassisCreates> ().Activate ();	
								get_Chassis_build.GetComponent<ChassisCreates> ().Activate (); 
						}
				}
		///////////////
		//int data = GameModel.getInstance().scencemodel.clone[xx,yy];
	//	if (data == 1) {
	//		get_Chassis_build.GetComponent<ChassisCreates> ().Activate (); 
	//	} else 
	//	{
	//		get_Chassis_build.GetComponent<ChassisCreates> ().Deactivate();
	//	} 
		return get_Chassis_builds;
	}

}

