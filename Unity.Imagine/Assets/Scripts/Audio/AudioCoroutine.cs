
using System.Collections;

public static class AudioCoroutine {

  /// <summary> 再生終了したら <see cref="SourceObject"/> を削除する </summary>
  public static IEnumerator AutoRelease(this SourceObject source, System.Action UnBind) {
    while (source.IsPlayingWithLoop()) { yield return null; }
    UnBind();
  }
}
