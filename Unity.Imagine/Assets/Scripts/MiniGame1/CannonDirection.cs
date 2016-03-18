using UnityEngine;
using System.Collections;

public class CannonDirection : MonoBehaviour {

    private enum SelectPlayer
    {
        Player1,
        Player2
    }

    [SerializeField]
    private SelectPlayer _selectPlayer;

    
    private GameObject _enemy;

    void Start()
    {
        _enemy = _selectPlayer == SelectPlayer.Player1 ? 
            GameObject.Find("Player2") : GameObject.Find("Player1");
    }

    void Update()
    {
        transform.LookAt(_enemy.transform.position);
    }

}
