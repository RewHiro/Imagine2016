using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSelect : MonoBehaviour {

    private const int _PLAYER_MAX = 2;

    private Component[] _components;

    private List<GameObject> _objList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        _components = new Component[_PLAYER_MAX];
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void ComponentDestroy()
    {
        foreach(var obj in _objList)
        {
            Destroy(obj);
        }
        foreach(var component in _components)
        {
            Destroy(component);
        }
        _components = new Component[_PLAYER_MAX];
        _objList = new List<GameObject>();
    }

    void OnGUI()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length != _PLAYER_MAX) { return; }
       
        if(GUI.Button(new Rect(20, 20, 100, 50), "Game1"))
        {
            ComponentDestroy();
            for (int i = 0; i < _PLAYER_MAX; ++i)
            {
                _components[i] = players[i].AddComponent(typeof(GameTest1));
            }
            GetComponent<KeyAction>().InitOn();
        }

        if (GUI.Button(new Rect(140, 20, 100, 50), "Game2"))
        {
            ComponentDestroy();
            for (int i = 0; i < _PLAYER_MAX; ++i)
            {
                _components[i] = players[i].AddComponent(typeof(Pendulum));
            }
            GameObject obj = Instantiate(Resources.Load("MiniGame/Pendulum/Ball") as GameObject);
            obj.GetComponent<Ball>().SetPlayers(players);
            Vector3 pos = players[1].transform.position - players[0].transform.position;
            obj.transform.position = players[0].transform.position + pos * 0.5f;
            obj.name = "Ball";
            _objList.Add(obj);
            GetComponent<KeyAction>().InitOn();
        }

        if (GUI.Button(new Rect(260, 20, 100, 50), "Game3"))
        {

        }
    }
}
