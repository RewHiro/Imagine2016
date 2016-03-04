using UnityEngine;

public class ParameterBar : MonoBehaviour
{
    [SerializeField]
    RectTransform _speedGauge = null;

    [SerializeField]
    RectTransform _attackGauge = null;

    [SerializeField]
    RectTransform _defenceGauge = null;

    CharaCreater _charaCreater = null;

    void Start()
    {
        _charaCreater = FindObjectOfType<CharaCreater>();
    }

    public ParameterBar ChangeParameterGauge()
    {
        var characterParameter = _charaCreater.getCharacterParamter;
        _speedGauge.localScale = new Vector3(characterParameter.speed * 0.2f, 1, 1);
        _attackGauge.localScale = new Vector3(characterParameter.attack * 0.2f, 1, 1);
        _defenceGauge.localScale = new Vector3(characterParameter.defense * 0.2f, 1, 1);
        return this;
    }
}