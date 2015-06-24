using UnityEngine;
using System.Collections;

/// <summary>
/// 实例化 场景各类元素
/// </summary>
///    

public class InstantiationPrefabs : AssembleModel
{
    GameObject currentTarget = null;
	Transform Chassis;
    public int rate = 10;
    public bool ballSet = true;
    void Start()
    { 
        Transform Grid = Spawner("Grid", "Grid");//创建网格
        Grid.name = "Grid";
        Grid.position = new Vector3(-0.68f, 0, -0.82f);
        CreateGameBoard(22, 22);//创建地形 和建筑 
    }

    void Update()
    {

    }

	//创建底座
	public Transform CreateChassis(Vector3 ve)
	{
		Chassis = Spawner("Chassis", "Chassis");  
		Chassis.name = "Chassis";
		ve.y = .1f;
		Chassis.position = ve;
		return Chassis;
	}

	//销毁底座
	public void DespawnChassis(Transform Chassis)
	{
		Despawn(Chassis,"Chassis"); 
	}
     
    //创建地形
    void CreateGameBoard(uint cols, uint rows)
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
				Transform Terrain = Spawner("Terrain", "Terrain");
               

                Terrain.position = new Vector3(i, 0, j);
                Terrain.name = "Terrain: " + i + "," + j;

				if (GameModel.getInstance().scencemodel.date[i,j] == 1)//测试用，正是用数组
				{Transform build;
					if(i<j)
					 build = Spawner("Build", "Build");
					else{ 
					  build = Spawner("Build1", "Build");
					}
                    build.position = new Vector3(i, 1, j);
                    build.name = "build: " + i + "," + j;
                } 
                Color blockColor;
                if ((j + i) % 2 == 0)
                {
                    blockColor = Color.black;
                }
                else
                {
                    blockColor = Color.white;
                }
                Terrain.GetComponent<Renderer>().material.color = blockColor;
            }
        }
    } 
}
