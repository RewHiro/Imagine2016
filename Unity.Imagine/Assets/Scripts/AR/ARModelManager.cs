
using System.Collections.Generic;

public class ARModelManager : SingletonBehaviour<ARModelManager> {

  readonly List<ARModel> _models = new List<ARModel>();
  public IEnumerable<ARModel> models { get { return _models; } }

  public void Add(ARModel model) { _models.Add(model); }
  public void AddRange(IEnumerable<ARModel> models) { _models.AddRange(models); }
  public bool Remove(ARModel model) { return _models.Remove(model); }
  public void Clear() { _models.Clear(); }
}
