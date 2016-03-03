using UnityEngine;
using System;

/*
2 / 29
14 : 40 野本　変更

    前回のをかえて、
    Listに処理をいれ関数を一々作るのを辞めました

    各ボタンごとに番号をしていてやっています
    Test
*/

public class ChangeCharacterPattern : MonoBehaviour
{
    [SerializeField]
    GameObject[] _panels = null;

    //Typeのモデル
    [SerializeField]
    GameObject[] _typePrefabs = null;

    //Costumeのモデル
    [SerializeField]
    GameObject[] _costumePrefabs = null;

    //Decorationのモデル
    [SerializeField]
    GameObject[] _decorationPrefabs = null;


    //TypeButtonを押したら
    public void PushButtonOfType()
    {
        _panels[0].SetActive(true);
        _panels[1].SetActive(false);
        _panels[2].SetActive(false);
    }

    //CostumeButtonを押したら
    public void PushButtonOfCostume()
    {
        _panels[0].SetActive(false);
        _panels[1].SetActive(true);
        _panels[2].SetActive(false);
    }

    //DecorationButtonを押したら
    public void PushButtonOfDecoration()
    {
        _panels[0].SetActive(false);
        _panels[1].SetActive(false);
        _panels[2].SetActive(true);
    }

    public void PushOfDecideButton()
    {
        //決定ボタンを押したら
    }

    //
    public void PushOfBackTitle()
    {
        //右上のButtonを押したら
    }

    public void SetType(int index)
    {
        InitModel((uint)index, _typePrefabs, "Character");
    }

    public void SetCostume(int index)
    {
        InitModel((uint)index, _costumePrefabs, "Costume");
    }

    public void SetDecoration(int index)
    {
        InitModel((uint)index, _decorationPrefabs, "Decoration");
    }

    void InitModel(uint index, GameObject[] prefabs, string tag)
    {
        if (index > prefabs.Length) throw new IndexOutOfRangeException("out of range");

        var gameObjectWithTag = GameObject.FindGameObjectWithTag(tag);
        if (gameObjectWithTag == null) throw new NullReferenceException(tag + " tag is nothing");

        var _parent = gameObjectWithTag.transform.parent;

        Destroy(gameObjectWithTag);

        var model = Instantiate(prefabs[index]);
        model.name = prefabs[index].name;
        model.transform.SetParent(_parent);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
    }
}
