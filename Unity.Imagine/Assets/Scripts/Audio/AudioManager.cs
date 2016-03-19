
using UnityEngine;
using System.Collections.Generic;

//------------------------------------------------------------
// NOTICE:
// SourceObject の管理を行う
//
//------------------------------------------------------------
// TIPS:
// CreateSource() について
//
// SourceObject を生成、管理下に置いた上でインスタンスを返します
//
//------------------------------------------------------------

public class AudioManager : SingletonBehaviour<AudioManager> {

  [SerializeField]
  AudioClip[] _clips = null;
  public IEnumerable<AudioClip> clips { get { return _clips; } }

  void Start() { DontDestroyOnLoad(gameObject); }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を取得 </summary>
  public AudioClip GetClip(int index) {
    return _clips[Mathf.Clamp(index, 0, _clips.Length - 1)];
  }

  readonly List<SourceObject> _sources = new List<SourceObject>();

  /// <summary> 管理下にある <see cref="SourceObject"/> を全て取得 </summary>
  public IEnumerable<SourceObject> sources { get { return _sources; } }

  /// <summary> 外部の <see cref="SourceObject"/> を
  /// <see cref="AudioManager"/> の管理下に登録する </summary>
  public void RegisterSource(SourceObject source) {
    source.transform.SetParent(transform);
    var exist = _sources.Contains(source);
    if (!exist) { _sources.Add(source); }
  }

  /// <summary> <see cref="SourceObject"/> を生成する </summary>
  /// <param name="parent"> このオブジェクトの管理下にする </param>
  public SourceObject CreateSource(Transform parent) {
    var source = SourceObject.Create();
    source.transform.SetParent(parent == null ? transform : parent);
    _sources.Add(source);
    return source;
  }

  /// <summary> <see cref="SourceObject"/> を生成する </summary>
  public SourceObject CreateSource() {
    var source = SourceObject.Create();
    RegisterSource(source);
    return source;
  }

  /// <summary> 管理下の <see cref="SourceObject"/> を全て解放する </summary>
  public void ClearSources() {
    foreach (var source in _sources) {
      source.AllStop();
      Destroy(source.gameObject);
    }
    _sources.Clear();
  }

  /// <summary> 指定した <see cref="SourceObject"/> を削除する </summary>
  public bool RemoveSource(SourceObject source) {
    source.AllStop();
    var success = _sources.Remove(source);
    if (success) { Destroy(source.gameObject); }
    return success;
  }
}
