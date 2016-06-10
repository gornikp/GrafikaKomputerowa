using System.Collections.Generic;
using System.Drawing;

namespace GrafikaKomputerowa
{
    public class CustomAlmostStack<T>
    {
        private readonly List<T> _items = new List<T>();
        private readonly int _v;

        public CustomAlmostStack(int v)
        {
            this._v = v;
        }

        public CustomAlmostStack()
        {
        }

        public void Push(T item)
        {
            _items.Add((item));
            if (_items.Count > _v)
                _items.RemoveAt(1);
        }

        public int Count()
        {
            return _items.Count;
        }

        public T Pop()
        {
            if (_items.Count > 0)
            {
                var temp = _items[_items.Count - 1];
                _items.RemoveAt(_items.Count - 1);
                return temp;
            }
            return default(T);
        }
    }
}