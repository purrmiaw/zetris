using UnityEngine;
using System.Collections;

public interface IGameObjectManager {

    int Add(float x, float y, float z);
    int AddTileGameObject(int x, int y);
    void Remove(int gameObjectInstanceId);
    void Move(int fromGameObjectInstanceId, int toGameObjectInstanceId);

}
