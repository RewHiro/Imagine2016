
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

static class ComponentExtension {

  /// <summary> <see cref="Component.GetComponentsInChildren{T}"/>
  /// で取得できる配列から親オブジェクト以外を取り出す </summary>
  public static IEnumerable<T> GetComponentsAllChildren<T>(this Component parent) where T : Component {
    var components = parent.GetComponentsInChildren<T>();
    return components.Where(component => component.transform != parent.transform);
  }

  /// <summary> <see cref="Component.GetComponentsInChildren{T}"/>
  /// で取得できる配列から、直下にある子オブジェクトのみを取り出す </summary>
  public static IEnumerable<T> GetComponentsOnlyChildren<T>(this Component parent) where T : Component {
    var children = parent.GetComponentsInChildren<T>();
    return children.Where(child => child.transform.parent == parent.transform);
  }
}
