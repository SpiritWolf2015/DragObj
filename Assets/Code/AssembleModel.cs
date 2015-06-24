using UnityEngine;
using System.Collections;
using PathologicalGames;

public class AssembleModel : MonoBehaviour
{ 
    //读取创建
    public Transform Spawner(string prefabs, string SpawnPools = "prefabs")
    {
        Transform inst;
        SpawnPool shapesPool = PoolManager.Pools[SpawnPools];
        inst = shapesPool.Spawn(Resources.Load<Transform>(prefabs));
        return inst;
    }
    //克隆创建
    public Transform SpawnerClone(Transform prefabs, string SpawnPools = "prefabs")
    {
        Transform inst;
        SpawnPool shapesPool = PoolManager.Pools[SpawnPools];
        inst = shapesPool.Spawn(prefabs);
        return inst;
    } 
    //全体删除
    public void Despawner(string SpawnPools = "prefabs")
    {
        SpawnPool shapesPool = PoolManager.Pools[SpawnPools];
        shapesPool.DespawnAll();
    }

    //单个删除 
    public void Despawn(Transform transform, string SpawnPools = "prefabs")
    {
        SpawnPool shapesPool = PoolManager.Pools[SpawnPools];
        shapesPool.Despawn(transform);
    }
}
