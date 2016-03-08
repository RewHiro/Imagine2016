
using UnityEngine;
using System;
using System.Collections;

public class Fade : Effect {
  public Fade(float time) : base() { _time = time; }

  enum State { FadeOut, FadeIn, Update, }
  State _state = State.Update;
  float _time = 0f;

  public override bool IsPlaying() { return _state != State.Update; }

  public override IEnumerator Sequence(Action action) {
    _state = State.FadeOut;
    yield return Coroutine(FadeUpdate(count => count < _time, 0f, 1));
    action();
    yield return Coroutine(FadeUpdate(count => count > 0f, _time, -1));
  }

  IEnumerator FadeUpdate(Func<float, bool> RoopExpression, float count, int sign) {
    while (RoopExpression(count)) {
      yield return null;
      count += Time.deltaTime * sign;
      ImageUpdate(count);
    }
    ++_state;
  }

  void ImageUpdate(float count) {
    sequencer.image.color = Color.black * (count / _time);
  }
}
