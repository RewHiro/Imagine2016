
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// NOTICE:
// 自身の GameObject に追加された AudioSource コンポーネントの管理を行う
//
//------------------------------------------------------------
// TIPS:
// 基本的に AudioManager.CreateSource() を使用してインスタンスを生成します
//
// GameObject に直接スクリプトを追加したり、
// SourceObject.Create() でもインスタンス生成は可能ですが、
// AudioManager には登録されないため、独自に管理する必要があります
//
// 逆に、AudioManager を必要としない場合は上記の方法で
// インスタンス生成、インスタンスの操作が可能です
//
// 外部から AudioManager の管理下に置きたい場合は、
// AudioManager.RegisterSource() の引数にインスタンスを渡してください
//
//------------------------------------------------------------

public class SourceObject : MonoBehaviour {

  /// <summary> <see cref="AudioSource"/> コンポーネントは存在しないので注意 </summary>
  public static SourceObject Create() {
    var source = new GameObject("Source").AddComponent<SourceObject>();
    return source;
  }

  /// <summary> 自身に追加された <see cref="AudioSource"/> を全て取得 </summary>
  public IEnumerable<AudioSource> GetSources() {
    var sources = GetComponents<AudioSource>();
    foreach (var source in sources) { yield return source; }
  }

  /// <summary> 再生中ではない <see cref="AudioSource"/> を取得 </summary>
  public AudioSource GetSource() {
    return GetSources().FirstOrDefault(source => !source.isPlaying);
  }

  /// <summary> <see cref="GetSource()"/> で取得に成功すれば true を返す </summary>
  public bool GetSource(out AudioSource source) {
    source = GetSource();
    return source != null;
  }

  /// <summary> 新規の <see cref="AudioSource"/> を追加、取得する </summary>
  public AudioSource AddSource() {
    var source = gameObject.AddComponent<AudioSource>();
    source.playOnAwake = false;
    return source;
  }

  /// <summary> <see cref="AudioSource"/> が存在すれば true を返す </summary>
  public bool ExistSource() { return GetSources().Any(); }

  /// <summary> ループ再生中ではない、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlaying() {
    var sources = GetSources().Where(source => !source.loop);
    return sources.Any(source => source.isPlaying);
  }

  /// <summary> ループ中も含めて、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlayingWithLoop() {
    return GetSources().Any(source => source.isPlaying);
  }

  /// <summary> ループ設定の <see cref="AudioSource"/> があれば true を返す </summary>
  public bool ExistLoopSource() {
    return GetSources().Any(source => source.loop);
  }

  /// <summary> ループ設定の <see cref="AudioSource"/> を全て取得 </summary>
  public IEnumerable<AudioSource> GetLoopSources() {
    return GetSources().Where(source => source.loop);
  }

  /// <summary> <see cref="AudioClip"/> が登録された
  /// <see cref="AudioSource"/> を全て同時に再生する </summary>
  public void AllPlay() {
    var sources = GetSources().Where(source => source.clip != null);
    foreach (var source in sources) { source.Play(); }
  }

  /// <summary> <see cref="AudioClip"/> が登録された
  /// <see cref="AudioSource"/> を全て同時に停止する </summary>
  public void AllStop() {
    var sources = GetSources().Where(source => source.clip != null);
    foreach (var source in sources) { source.Stop(); }
  }

  /// <summary> 再生中でなくなれば、リソースを解放する </summary>
  public IEnumerator AutoRelease(System.Action UnBind) {
    System.Func<GameScene> GetActiveScene =
      () => SceneManager.GetActiveScene().ToGameScene();

    var current = GetActiveScene();
    while (IsPlayingWithLoop()) {
      if (current != GetActiveScene()) { break; }
      yield return null;
    }
    UnBind();
  }
}
