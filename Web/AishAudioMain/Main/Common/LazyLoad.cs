using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Common
{
    public class LazyLoad<T>
    {
        private Func<T> _valueFunc;
        private T _value;
        public T Value
        {
            get
            {
                if (_value == null)
                    _value = _valueFunc();
                return _value;
            }
        }

        public static implicit operator T (LazyLoad<T> data)
        {
              return data.Value;
        }

        public LazyLoad(Func<T> value_func)
        {
            _valueFunc = value_func;
        }

        public static explicit operator LazyLoad<T>(Func<T> func)
        {
            return new LazyLoad<T>(func);
        }
    }
}