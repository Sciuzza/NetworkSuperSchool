using UnityEngine;
using UnityEngine.Networking;

public class TestSpawning : MonoBehaviour {

    public GameObject spawnablePrefab;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject go = Instantiate(spawnablePrefab);
            go.transform.position = Vector3.zero;
            NetworkServer.Spawn(go);
        }
	}
}
