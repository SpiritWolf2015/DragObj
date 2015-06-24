using UnityEngine;
using System.Collections;

public class GamePieceCSharp : MonoBehaviour
{
	private Color currentColor;
	private Vector3 currentPosition;
    InstantiationPrefabs gameBoard;
	bool isActive;
	
	void Start(){
		currentColor=GetComponent<Renderer>().material.color;
		currentPosition=transform.position;
	}

    void SetGameboard(InstantiationPrefabs gameBoard)
    {
		this.gameBoard=gameBoard;
	}
	
	void Deactivate(){
		isActive=false;
		iTween.ColorTo(gameObject,currentColor,.4f);
	}
	
	void Activate(){
		isActive=true;
		GetComponent<Renderer>().material.color=Color.red;
		SendMessageUpwards("SetTarget",gameObject);	 
	}
	
	void OnMouseEnter(){
		if(!isActive){
			if(!gameBoard.ballSet){
				GetComponent<Renderer>().material.color=Color.yellow;
			}else{
				GetComponent<Renderer>().material.color=Color.green;
			} 
		}
	}
	
	void OnMouseExit(){
		if(!isActive){
			iTween.ColorTo(gameObject,currentColor,.4f); 
		}
	}
	
	void OnMouseDown(){
		print (gameBoard.ballSet);
		if(gameBoard.ballSet){
			Activate();
		}
	}
}

