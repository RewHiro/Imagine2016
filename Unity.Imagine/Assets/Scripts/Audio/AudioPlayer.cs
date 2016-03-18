
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// NOTICE:
// SourceObject が管理している AudioSource に対して再生、停止の命令を行う
//
//------------------------------------------------------------
// TIPS:
// 1: manageMode (SourceManageMode) について
//
// Additive は SourceObject の所有権を AudioManager 側に委ねます
// Control は AudioSource の追加を行いません
// 追加、管理どちらも行う場合は、Full を指定してください
//
// 2: autoRelease (SourceObject.AutoRelease()) について
//
// 再生を完了したときに、自動で SourceObject を削除します
// 
// ループ中の場合、再生中の AudioSource を手動で取得したうえで停止するか、
// ループ設定を解除するまで SourceObject の削除が行われないことに注意
//
//------------------------------------------------------------

public class AudioPlayer : MonoBehaviour {

  /// <summary> <see cref="SourceObject"/> の管理方法の一覧 </summary>
  public enum SourceManageMode {
    /// <summary> <see cref="AudioManager"/> に全ての管理を任せる </summary>
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
  [Tooltip("SourceObject の管理方法を指定")]
  SourceManageMode _manageMode = SourceManageMode.None;

  /// <summary> <see cref="SourceObject"/> の管理方法を指定 </summary>
  public SourceManageMode manageMode {
    get { return _manageMode; }
    set { _manageMode = value; }
  }

  bool isAdditive { get { return ((int)_manageMode % 2) > 0; } }
  bool isControl { get { return _manageMode > SourceManageMode.Additive; } }

  [SerializeField]
  [Tooltip("再生が終了した SourceObject を自動的に開放する")]
  bool _autoRelease = false;

  /// <summary> 再生終了時に自動で
  /// <see cref="SourceObject"/> を解放するか指定 </summary>
  public bool autoRelease {
    get { return _autoRelease; }
    set { _autoRelease = value; }
  }

  new AudioManager audio { get { return AudioManager.instance; } }

  // TIPS: 関連付けられた SourceObject のインスタンス
  SourceObject _sourceObject = null;

  /// <summary> 自身に関連付けられた <see cref="SourceObject"/> の
  /// 所有権が自身にあれば true を返す </summary>
  public bool IsOwnership() {
    return _sourceObject.transform.parent == transform;
  }

  // TIPS: AudioManager.CreateSource() のラッパー
  SourceObject CreateSource() {
    return audio.CreateSource(isControl ? transform : null);
  }

  // TIPS: SourceObject を自身に関連付ける
  void Bind() {
    _sourceObject = audio.sources.FirstOrDefault(source => !source.IsPlaying());
    if (_sourceObject == null) { _sourceObject = CreateSource(); }
  }

  /// <summary> 自身に関連付けられた <see cref="SourceObject"/> に
  /// <see cref="AudioSource"/> を追加する </summary>
  public AudioSource AddSource() { return _sourceObject.AddSource(); }

  /// <summary> 再生中の <see cref="AudioSource"/> を全て取得 </summary>
  public IEnumerable<AudioSource> GetPlayingSources() {
    return _sourceObject.GetSources().Where(source => source.isPlaying);
  }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  /// <param name="isLoop"> true = ループ再生を許可 </param>
  public void Play(int index, bool isLoop) {
    if (!audio.sources.Any()) { Bind(); }

    AudioSource source = null;
    var success = _sourceObject.GetSource(out source);
    if (!success && isAdditive) { source = _sourceObject.AddSource(); }

    if (source == null) { return; }
    source.clip = audio.GetClip(index);
    source.loop = isLoop;
    source.Play();

    if (_autoRelease) { StartCoroutine(_sourceObject.AutoRelease(UnBind)); }
  }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  public void Play(int index) { Play(index, false); }

  /// <summary> <see cref="AudioClip"/> が登録済みの
  /// <see cref="AudioSource"/> を全て再生する </summary>
  public void AllPlay() { _sourceObject.AllPlay(); }

  /// <summary> 再生中の <see cref="AudioSource"/> を全て停止する </summary>
  public void Stop() { _sourceObject.AllStop(); }

  /// <summary> 関連付けられた <see cref="SourceObject"/> を解放する </summary>
  public void UnBind() {
    var success = audio.RemoveSource(_sourceObject);
    if (success) { _sourceObject = null; }
  }
}
