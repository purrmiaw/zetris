using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameObjectManager : MonoBehaviour, IGameObjectManager
{

    public GameObject PrefabUnit;
    private ICollection<GameObject> gameObjects = new List<GameObject>();

    /*************************************************************************************************/
    // Implementation. Assume different class.

    void IGameObjectManager.Remove(int gameObjectInstanceId)
    {
        GameObject gameObject = gameObjects.Where(x => x.GetInstanceID() == gameObjectInstanceId).FirstOrDefault();
        gameObjects.Remove(gameObject);
        Destroy(gameObject);
    }

    int IGameObjectManager.AddTileGameObject(int x, int y)
    {
        GameObject tile = new GameObject();
        tile.transform.position = new Vector3(x, y, 0);
        tile.name = "Tile-" + x.ToString() + "-" + y.ToString();

        gameObjects.Add(tile);

        return tile.GetInstanceID();
    }

    int IGameObjectManager.Add(float x, float y, float z)
    {
        Vector3 position = new Vector3(x, y, z);
        GameObject newGameObject = (GameObject)Instantiate(PrefabUnit, position, PrefabUnit.transform.rotation);
        gameObjects.Add(newGameObject);
        return newGameObject.GetInstanceID();
    }

    void IGameObjectManager.Move(int fromGameObjectInstanceId, int toGameObjectInstanceId)
    {
        GameObject from = gameObjects.Where(x => x.GetInstanceID() == fromGameObjectInstanceId).FirstOrDefault();
        GameObject to = gameObjects.Where(x => x.GetInstanceID() == toGameObjectInstanceId).FirstOrDefault();

        from.transform.position = to.transform.position;
    }
}
