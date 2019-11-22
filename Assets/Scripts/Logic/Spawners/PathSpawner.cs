using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{

    public List<GameObject> paths;
    public Transform spawnPoint;
    public GameObject prevType;

    void Start(){
        paths = new List<GameObject>();
        Load();
        prevType = paths[6];
    }

    protected List<string> getFiles(){
        List<string> files = new List<string>();
        files.Add("Prefabs/EmptyPath");
        files.Add("Prefabs/CoinPath");
        files.Add("Prefabs/LimitedPath");
        files.Add("Prefabs/DoubleLimitedPath");
        files.Add("Prefabs/LeftLimitedPath");
        files.Add("Prefabs/RightLimitedPath");
        files.Add("Prefabs/ChaosPath");
        return files;
    }

    private GameObject getType(int rawIndex) {
        int index = rawIndex % this.paths.Count;
        if( rawIndex == 7 ){
            return prevType;
        }
        prevType = paths[index];
        return paths[index];
    }

    private void Load(){
        List<string> files = this.getFiles();
        foreach( string file in files ){
            paths.Add(Resources.Load(file, typeof(GameObject)) as GameObject);
        }
    }

    void OnTriggerEnter(Collider hit){
        int type = GameManager.Instance.GetNextSegment();
        GameObject next = Instantiate(getType(type), spawnPoint);
        this.enabled = false;
    }

    void OnParentAboutToBeDestroyed(){
        Debug.Log("Snap");
        transform.DetachChildren();
    }
}
