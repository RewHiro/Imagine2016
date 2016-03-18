
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
  /// <param name="parent"> インスタンスを指定したオブジェクトの管理下に置く </param>
  public SourceObject CreateSource(Transform parent) {
    var source = SourceObject.Create();
    RegisterSource(source);
    return source;
  }

  public SourceObject CreateSource() {
    return null;
  }
}
