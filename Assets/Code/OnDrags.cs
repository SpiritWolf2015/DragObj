using UnityEngine;
using System.Collections;

public class OnDrags : MonoBehaviour
{
	/*****
    public GameObject obj;
    public UILabel label;
    GameObject currentTarget = null;
    //SendMessage ("函数名",参数，SendMessageOptions) //GameObject自身的Script
    //BroadcastMessage ("函数名",参数，SendMessageOptions)  //自身和子Object的Script
    //SendMessageUpwards ("函数名",参数，SendMessageOptions)  //自身和父Object的Script
    void SetTarget(GameObject target)//地板
    {
        if (currentTarget != null && target != currentTarget)
        {
            currentTarget.SendMessage("Deactivate");
        }
        currentTarget = target;
    }
    GameObject currentTargets = null;
    void SetTargets(GameObject target)//物体
    {
        if (currentTargets != null && target != currentTargets)
        {
            currentTargets.SendMessage("Deactivates");
        }
        currentTargets = target;
    }
    void Awake()
    {
        //设置初始化状态
        GameModel.getInstance().scencemodel.dragState = 0;//没拖拽
        GameModel.getInstance().scencemodel.clickState = 0;//没点击
    }

    Vector3 cursorWorldPoints;
    void OnFingerDown(FingerDownEvent e) {  
        if (GameModel.getInstance().scencemodel.dragState != 2)
        {
            dragStart = true;
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .0f);
            cursorWorldPoints = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
            cursorWorldPoints.z = 0f;
        }
    }

    bool dragStart = false;
    Vector3 cursorWorldPointUp;
    void OnFingerUp(FingerUpEvent e) {  
        if (GameModel.getInstance().scencemodel.dragState != 2)
        {
            cursorWorldPointUp = obj.transform.position;
            cursorWorldPointUp.z = 0;
            dragStart = false;
        }
    }
    //拖拽手势识别器
    void OnDrag(DragGesture gesture)
    {   
        // 当前识别器阶段 (Started/Updated/Ended)
        ContinuousGesturePhase phase = gesture.Phase;
        // 最后一帧的拖拽/移动数据
        Vector2 deltaMove = gesture.DeltaMove;
        //完整的拖拽数据
        Vector2 totalMove = gesture.TotalMove;
        if (GameModel.getInstance().scencemodel.dragState != 2)
        {
            if (dragStart)
            {
                Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f);
                Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
                cursorWorldPoint.z = obj.transform.position.z;
                obj.transform.position = -cursorWorldPoints + cursorWorldPoint + cursorWorldPointUp;
            }

            GameModel.getInstance().scencemodel.dragState = 1;
        }
    }
    void Update()
    {
        label.text ="碰到了"+ GameModel.getInstance().scencemodel.name;
    } 
    //轻击手势识别器
    void OnTap(TapGesture gesture)
    {  
     //   BroadcastMessage("OnMyMounseDown", "", SendMessageOptions.DontRequireReceiver);  //自身和子Object的Script
    }
    
    //长按手势识别器
    void OnLongPress(LongPressGesture gesture)
    {  
        // 长按持续时间
        float elapsed = gesture.ElapsedTime;
    
      //  GameModel.getInstance().scencemodel.dragState = 1;
    }
    

    
    //滑动手势识别器
    void OnSwipe(SwipeGesture gesture)
    {
 
            GameModel.getInstance().scencemodel.dragState = 3;
            //  完整的滑动数据
            Vector2 move = gesture.Move;
            // 滑动的速度
            float velocity = gesture.Velocity;
            // 大概的滑动方向
            FingerGestures.SwipeDirection direction = gesture.Direction; 
    } 
*********/
}
