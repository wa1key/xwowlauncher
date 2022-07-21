using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Launcher.Model;

public class ViewModelBase : DynamicObject, INotifyPropertyChanged
{
    private readonly Dictionary<string, object> _data = new();

    private readonly object _lock = new();

    public object this[string name]
    {
        get
        {
            object value;
            lock (_lock)
            {
                value = _data.ContainsKey(name) ? _data[name] : null;
            }

            return value;
        }
        set
        {
            lock (_lock)
            {
                _data[name] = value;
            }

            OnPropertyChanged(name);
        }
    }

    #region DynamicObject

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = this[binder.Name];
        return result != null;
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        if (this[binder.Name] == null || this[binder.Name] != value)
            this[binder.Name] = value;
        return true;
    }

    #endregion

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;


    protected virtual void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}