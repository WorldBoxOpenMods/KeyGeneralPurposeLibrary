using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace KeyGeneralPurposeLibrary {
  public abstract class KLibAssetLibrary<T> : KLibComponent {
    private readonly List<T> _assets = new List<T>();
    protected List<T> Assets => _assets.ToList();
    
    protected void AddAsset(T asset, out int index) {
      if (!_assets.Contains(asset)) _assets.Add(asset);
      index = _assets.IndexOf(asset);
    }
    
    [Pure]
    public bool FindAsset(T asset, out int index) {
      index = _assets.IndexOf(asset);
      return index != -1;
    }
    
    public T this[int index] => _assets[index];
  }
}