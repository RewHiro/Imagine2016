
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// NOTICE:
// SourceObject を利用、経由して
// AudioSource の再生、停止の一部を代行する
//
//------------------------------------------------------------

public class AudioPlayer : MonoBehaviour {

  /// <summary> <see cref="SourceObject"/> の管理方法の一覧 </summary>
  public enum SourceManageMode {
    /// <summary> <see cref="AudioManager"/> に管理を任せる </summary>
    None,
    /// <summary> <see cref="SourceObject"/> に、
    /// 再生可能な <see cref="AudioSource"/> がなければ自動で追加する </summary>
    Additive,
    /// <summary> 生成した <see cref="SourceObject"/> を自身で管理する </summary>
    Control,
    /// <summary> <see cref="Additive"/>、<see cref="Control"/> の全てを実行 </summary>
    Full,
  }

  [SerializeField]
  [Tooltip("再生する AudioSource の管理方法を指定")]
  SourceManageMode _manageMode = SourceManageMode.None;

  bool isAdditive { get { return ((int)_manageMode % 2) > 0; } }
  bool isControl { get { return _manageMode > SourceManageMode.Additive; } }

  [SerializeField]
  [Tooltip("再生が終了した AudioSource を自動的に開放する")]
  bool _autoRelease = false;

  new AudioManager audio { get { return AudioManager.instance; } }
  readonly Queue<AudioSource> _sources = new Queue<AudioSource>();

  void Start() {
    Debug.Log(name + ": add = " + isAdditive);
    Debug.Log(name + ": ctrl = " + isControl);
  }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  public void Play(int index) {
  }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  public void Play(int index, bool isLoop) {
  }

  /// <summary> 自身が再生中の <see cref="AudioSource"/> を全て停止する </summary>
  public void Stop() {
  }

  /// <summary> 指定した ID を再生中の <see cref="AudioSource"/> を停止する </summary>
  public bool Stop(int index) {
    return false;
  }

  /// <summary> 関連付けられた <see cref="SourceObject"/> を解放する </summary>
  public void Clear() {
  }

  // TIPS: AudioManager.CreateSource() のラッパー
  AudioSource CreateSource() {
    return null;
    //    return audio.CreateSource(isControl ? transform : null);
  }

  // TIPS: フラグの状態で取得する AudioSource を切り替える
  AudioSource GetSource() {
    return null;
  }
}
