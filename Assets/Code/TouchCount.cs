using UnityEngine;
using System.Collections;

public class TouchCount : MonoBehaviour
{
    public BoxCollider moveArea; //摄像头区域碰点
    public Transform camera;//摄像头 
    Vector3 idealPos;//摄像头坐标

    public float sensitivity = 1.0f;
    public float smoothSpeed = 10;
    float camera_y;
    void Start()
    { 
        idealPos = camera.position;
        camera_y = camera.position.y; 
	//	this.StartCoroutine(Spawner());
    } 
	/**
	private IEnumerator Spawner()
	{ 
		yield return new WaitForSeconds(2f); 
		this.StartCoroutine(Despawner());
	}
	private IEnumerator Despawner()
	{ 
		string a = "";
		for (int i = 0; i< 22; i++) {//遍历操作
			for (int j=0; j<22; j++) {
				a += (GameModel.getInstance ().scencemodel.clone [i, j])+"=";
			}
		}
		print (a); 
		yield return new WaitForSeconds(2f);
	}
**/
    Transform RaycastHitGet(Vector3 e)
    {  
		int mouse_Camera = 0;//鼠标默认点击
		int mouse_terrain = 0;//鼠标默认点击
		int mouse_build = 0;//鼠标默认点击

        Ray ray = Camera.main.ScreenPointToRay(e);
        RaycastHit[] hi = Physics.RaycastAll(ray);
		Transform transform_Camera=null;
		Transform transform_terrain=null;
		Transform transform_build=null;
        if (hi.Length > 0)
        {
            for (int i = 0; i < hi.Length; i++)  
            {   
				switch(hi[i].collider.gameObject.layer)
				{
				case 8:
					mouse_Camera = 1;
					transform_Camera = hi[i].collider.transform;
					//print(hi[i].collider.transform+"-----相机");
					break;
				case 9:
					mouse_terrain = 1;
					transform_terrain = hi[i].collider.transform;
					//print(hi[i].collider.transform+"-----terrain");
					break;
				case 10:
					mouse_build = 1;
					transform_build = hi[i].collider.transform;
					//print(hi[i].collider.transform+"-----build");
					break; 
				default:
					print("未知参数");
					break; 
				} 
            }
        }
	   	On_mouse_down (mouse_Camera,mouse_terrain,mouse_build,transform_Camera,transform_terrain,transform_build);
		 
	    return transform_build; 
    } 

	public Transform transform_build_old = null;
	public Transform transform_terrains_old = null;
	//处理鼠标点下去  判断哪个可以移动
	void On_mouse_down(int mouse_Camera ,int  mouse_terrain ,int mouse_build,Transform transform_Cameras,Transform transform_terrains,Transform transform_builds)
	{ 
		if (transform_build_old == null) {
			transform_build_old = transform_terrains; 
		}

		GameModel.getInstance ().scencemodel.terrain_now_down = mouse_terrain;
		GameModel.getInstance ().scencemodel.build_now_down = mouse_build;
		//如果，之前有点过 建筑
		if (GameModel.getInstance ().scencemodel.build_down == 1) 
		{
			 //如果 现在又点了 建筑
			if (GameModel.getInstance ().scencemodel.build_now_down == 1) 
			  {
				if(transform_build_old.name == transform_builds.name)
				{
					if( get_transform_build.GetComponent<buildCreate>().set_target == 1 )
					{
								//点击以点过的建筑 可以被拖拽
					GameModel.getInstance ().scencemodel.scence = false;
					transform_builds.BroadcastMessage("over_TargetSet_build",this.gameObject);//再次内炫并设置可以拖拽
					}else{
						//防止，之前点过a 之后点b并拖拽， 再次点b 由于tap 没给set_target 为1
						GameModel.getInstance ().scencemodel.scence = true;
						transform_builds.BroadcastMessage("over_Shader_Set",this.gameObject);//内炫
					}
				} 
				else
				{
					//点击新建筑  内炫
					GameModel.getInstance ().scencemodel.scence = true;
					transform_build_old.BroadcastMessage("before_TargetSet",this.gameObject);//取消之前内炫并取消拖拽
					//增加现在模块内炫的
					//transform_builds.BroadcastMessage("over_Shader_Set",this.gameObject);
					transform_terrains_set(transform_terrains,transform_builds);
				}
			  } 
			else 
			  { 
				GameModel.getInstance ().scencemodel.scence = true;
				transform_terrains_set(transform_terrains,transform_builds);
			 	 
				//取消建筑 内炫
				transform_build_old.BroadcastMessage("before_TargetSet",this.gameObject);//取消之前内炫并取消拖拽
				transform_build_old = null;
			  } 
	    } 
		else   //之前没点过建筑
		{ 
			//如果 现在又点了 建筑
			if (GameModel.getInstance ().scencemodel.build_now_down == 1) 
			{ 
				GameModel.getInstance ().scencemodel.scence = true; 
					//增加现在模块内炫的
			//	transform_builds.BroadcastMessage("over_Shader_Set",this.gameObject);//之前没点 建筑，现在点了建筑 
				transform_terrains_set(transform_terrains,transform_builds);
			} 
			else 
			{ 
				GameModel.getInstance ().scencemodel.scence = true;
				transform_build_old = null;

				transform_terrains_set(transform_terrains,transform_builds);
			}  
	    } 
		if(transform_builds != null)
		transform_build_old = transform_builds; 
	} 

	//地形 内旋设置
	void transform_terrains_set(Transform transform_terrains,Transform transform_builds)
	{
		if (transform_terrains != null) 
			//地形渲染
				if (transform_builds == null) {
					transform_builds =transform_terrains; 
				}
			// transform_terrains.BroadcastMessage("over_TargetSet_terrain",transform_builds);//之前没点建筑，现在也没点建筑
		
		if (transform_terrains_old != null) {
			if(transform_terrains_old != transform_terrains)
			{//取消过去的渲染
			 transform_terrains_old.BroadcastMessage ("before_TargetCancel", transform_terrains_old);//给地形拖拽并渲染 
			}
		}
		transform_terrains_old = transform_terrains; 
	}

	//轻击手势识别器
	void OnTap(TapGesture gesture)
	{ /* your code here */
		//   BroadcastMessage("OnMyMounseDown", "", SendMessageOptions.DontRequireReceiver);  //自身和子Object的Script
		if (get_transform_build != null) {   
			if(get_transform_build.GetComponent<buildCreate>().set_target == 0)//被点过 开始执行 底座套用
			{  
				//增加现在模块内炫的
				get_transform_build.BroadcastMessage("over_Shader_Set",this.gameObject);  
			} 
		} 
		 
		if (transform_terrains_old != null) { 
		   if(get_transform_build!=null)
			{ 
 				transform_terrains_old.BroadcastMessage("over_TargetSet_terrain",get_transform_build);//之前没点建筑，现在也没点建筑
			}else
			{ 
				transform_terrains_old.BroadcastMessage("over_TargetSet_terrain",transform_terrains_old);//之前没点建筑，现在也没点建筑
			}
		 } 
	}

	private Transform get_transform_build;
	private buildCreate build_Create;
	private Transform get_Chassis_build;
 
	Vector3 cursorWorldPoints;//点击时候的坐标
    void OnFingerDown(FingerDownEvent e)
	{  
		//获取点击时候的 坐标 ，为了 建筑物 拖拽
		////////////////////////////////////////////////////
		Vector3 cursorScreenPoint = e.Position;
		cursorWorldPoints = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
	    
		//////////////////////////////////////////////////// 
		get_transform_build =  RaycastHitGet (e.Position);//点击的时候获取 建筑


		this.transform.FindChild("GridCreate").FindChild("Grid").GetComponent<GFRectGrid>().renderGrid = false;//网格线的取消和设置
		 
		if (get_transform_build != null) { 
			//底座生成
			get_Chassis_build = this.transform.GetComponent<InstantiationPrefabs> ().CreateChassis (get_transform_build.position);
			//写一个方法，调用 建筑类的 生成底座 
			get_transform_build.GetComponent<buildCreate>().CreateChassis(this.transform);

			build_Create = get_transform_build.GetComponent<buildCreate> ();

			if(get_transform_build.GetComponent<buildCreate>().set_target == 2)//被点过 开始执行 底座套用
			{ 
				set_transform_build(0); 
			}
		 } 
	}
	Vector3 cursorWorldPointUp;//松开时候的坐标
    void OnFingerUp(FingerUpEvent e)
	{ 
		GameModel.getInstance ().scencemodel.build_down = GameModel.getInstance ().scencemodel.build_now_down;//把现在点击给过去
		GameModel.getInstance ().scencemodel.terrain_down = GameModel.getInstance ().scencemodel.terrain_now_down;

		//获取 松开时候的 坐标，为了 建筑拖拽
		////////////////////////////////////////////////////
		if (get_transform_build != null) {  
			if(get_transform_build.GetComponent<buildCreate>().set_target == 2&&get_transform_build.GetComponent<buildCreate>().drag == true)//被点过 开始执行 底座套用
			{
			  Vector3 vr =  UpPostion_build_Create;
		      vr.y = 1.0f;
			  get_transform_build.position = vr ;
 
			} 
			cursorWorldPointUp = get_transform_build.position; 
			/////////////加入数组坐标
		    if(get_transform_build.GetComponent<buildCreate>().set_target == 2)//被点过 开始执行 底座套用
			set_transform_build(1);
			/// 
			//删除底座
			transform.GetComponent<InstantiationPrefabs> ().DespawnChassis(get_Chassis_build); 
			//写一个方法 调用建筑类的 删除底座
			get_transform_build.GetComponent<buildCreate>().DeleChassis(this.transform);

		 }
		////////////////////////////////////////////////////

        this.transform.FindChild("GridCreate").FindChild("Grid").GetComponent<GFRectGrid>().renderGrid = true;
    } 
	int x_old;
	int y_old;
	//记录或清空 数组坐标
	void set_transform_build(int i)
	{  
		int xx = (int)get_transform_build.position.x;
		int yy = (int)get_transform_build.position.z;
		if (i == 0) {
				//记录坐标
			x_old = xx;
			y_old = yy;
			GameModel.getInstance ().scencemodel.date [xx, yy] = i;  
			GameModel.getInstance ().scencemodel.clone [xx, yy] = i;   
			get_transform_build.GetComponent<buildCreate>().start_Clone(i);
		}
		if (i == 1) {
			if(get_Chassis_build.GetComponent<ChassisCreates>().set_target)//如果没有碰撞点
			{ 
				GameModel.getInstance ().scencemodel.date [xx, yy] = i;  
				GameModel.getInstance ().scencemodel.clone [xx, yy] = i;  
				get_transform_build.GetComponent<buildCreate>().start_Clone(i);
			}else{ 
				//恢复到默认初始 坐标 包括 鼠标当初点的位置
				get_transform_build.position = new	Vector3(x_old,1f,y_old);
				Vector3 cursorScreenPoint = get_transform_build.position;
				cursorWorldPoints = Camera.main.ScreenToWorldPoint(cursorScreenPoint); 
				cursorWorldPointUp = get_transform_build.position; 
				get_transform_build.GetComponent<buildCreate>().start_Clone(i);//初始化周围底座坐标
			}
			if(x_old == xx&& y_old == yy)//原地点了几下
			{
				GameModel.getInstance ().scencemodel.date [xx, yy] = i;   
				GameModel.getInstance ().scencemodel.clone [xx, yy] = i;   
				get_transform_build.GetComponent<buildCreate>().start_Clone(i);
			}
		}
	}
	private Vector3 UpPostion_build_Create;
    //拖拽手势识别器
    void OnDrag(DragGesture gesture)
    { 
        DragGesture dragGesture = (gesture.State == GestureRecognitionState.Ended) ? null : gesture;
        
		if (GameModel.getInstance ().scencemodel.scence) {//当屏幕可以移动，才能 拖动 平移
						if (dragGesture != null) {
								if (dragGesture.DeltaMove.SqrMagnitude () > 0) {
										Vector2 screenSpaceMove = sensitivity * dragGesture.DeltaMove;
										Vector3 worldSpaceMove = screenSpaceMove.x * camera.right + screenSpaceMove.y * camera.up;
										idealPos -= worldSpaceMove;
								}
						}
				} 
		else 
		{ 
			//建筑可以移动
			if(get_transform_build != null&&dragGesture != null&&build_Create!=null)
			{  
				UpPostion_build_Create = build_Create.OnDrag_build(dragGesture.Position,cursorWorldPoints,cursorWorldPointUp,get_Chassis_build);   
			} 
		}
    }

 
    void Update()
	{
		if (GameModel.getInstance ().scencemodel.scence) {//当屏幕可以移动，才能滑动平移
						idealPos = ConstrainToMoveArea (idealPos); 
						if (smoothSpeed > 0) {
								Vector3 v = Vector3.Lerp (camera.position, idealPos, Time.deltaTime * smoothSpeed);
								v.y = camera_y;
								camera.position = v;
						} else {
								idealPos.y = camera_y;
								camera.position = idealPos;
						}
				}
    }
    public Vector3 ConstrainToMoveArea(Vector3 p)
    {
        if (moveArea)
        {
            Vector3 min = moveArea.bounds.min;
            Vector3 max = moveArea.bounds.max;

            p.x = Mathf.Clamp(p.x, min.x, max.x);
            p.y = Mathf.Clamp(p.y, min.y, max.y);
            p.z = Mathf.Clamp(p.z, min.z, max.z);
        }
        return p;
    }

    //长按手势识别器
    void OnLongPress(LongPressGesture gesture)
    { /* your code here */
        // 长按持续时间
        float elapsed = gesture.ElapsedTime;

        //  GameModel.getInstance().scencemodel.dragState = 1;
    }



    //滑动手势识别器
    void OnSwipe(SwipeGesture gesture)
    {
        /* your code here */
        //  GameModel.getInstance().scencemodel.dragState = 3;
        //  完整的滑动数据
        Vector2 move = gesture.Move;
        // 滑动的速度
        float velocity = gesture.Velocity;
        // 大概的滑动方向
        FingerGestures.SwipeDirection direction = gesture.Direction;
    }

}
