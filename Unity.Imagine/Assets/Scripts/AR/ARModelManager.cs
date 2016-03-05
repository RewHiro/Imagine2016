
using System.Collections.Generic;

public class ARModelManager : SingletonBehaviour<ARModelManager> {

  readonly List<ARModel> _models = new List<ARModel>();
  public List<ARModel> models { get { return _models; } }
}
