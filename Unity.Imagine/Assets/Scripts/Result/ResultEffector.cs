using UnityEngine;
using System;
using System.Collections;

/// <summary>
///　リザルトの演出をする機能
/// </summary>
public class ResultEffector : MonoBehaviour
{

    GameObject _winEffect = null;
    GameObject _loseEffect = null;

    /// <summary>
    /// 勝利時に使用する関数
    /// </summary>
    /// <returns></returns>
    public IEnumerator Win()
    {
        var effect = Instantiate(_winEffect);
        yield return null;
    }

    /// <summary>
    /// 敗北時に使用する関数
    /// </summary>
    /// <returns></returns>
    public IEnumerator Lose()
    {
        yield return null;
    }

    void Start()
    {
        _winEffect = Resources.Load<GameObject>("Result/PaperParticle");
        _loseEffect = Resources.Load<GameObject>("Result/Rain/rain");

        if (_winEffect == null) throw new NullReferenceException("win effect null");
        if (_loseEffect == null) throw new NullReferenceException("lose effect null");
    }
}
