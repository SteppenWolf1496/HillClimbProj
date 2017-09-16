using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class WindowQueue
{
    public enum Type
    {
        Default = 0,
        New = 1,
        Top = 2
    }

    public sealed class Item
    {
        public BaseWindow Window { get; private set; }
        public WindowShowParameters ShowParams { get; private set; }

        public Item(BaseWindow window, WindowShowParameters showParams)
        {
            this.Window = window;
            this.ShowParams = showParams;
        }
    }

    private List<Item> _items = new List<Item>();

    public void Add(BaseWindow window, WindowShowParameters parameters)
    {
        _items.Add(new Item(window, parameters));
    }

    public void Remove(BaseWindow window)
    {
        Item foundItem = _items.Find((Item item) => {
            return item.Window == window;
        });
        if (foundItem == null) {
            return;
        }
        _items.Remove(foundItem);
    }

    public bool IsInQueue(BaseWindow window)
    {
        for (int i = 0; i < _items.Count; ++i) {
            if (_items[i].Window == window) {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        _items.Clear();
    }

    public int Count
    {
        get { return _items.Count; }
    }

    public Item GetLastItem()
    {
        if (_items.Count == 0) {
            return null;
        }
        _items.Sort(Comparator);
        for (int i = _items.Count; i != 0; --i) {
            if (_items[i - 1].Window.isShowAvailable()) {
                return _items[i - 1];
            }
        }
        return null;
    }

    private static int Comparator(Item item1, Item item2)
    {
        if (item1.Window.IsShow && !item2.Window.IsShow) {
            return 1;
        }
        if (item2.Window.IsShow && !item1.Window.IsShow) {
            return -1;
        }

        if (item1.Window.Params.priority > item2.Window.Params.priority) {
            return 1;
        }
        else if (item1.Window.Params.priority < item2.Window.Params.priority) {
            return -1;
        }
        return 0;
    }
}
