using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.guis
{
    public class AdditionalMouseEvents : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
    {
        public List<Action<PointerEventData>> OnMouseDownListeners = new List<Action<PointerEventData>>();
        public List<Action<PointerEventData>> OnMouseUpListeners = new List<Action<PointerEventData>>();

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var callback in OnMouseDownListeners)
            {
                callback(eventData);
            }
        }

        public void AddOnMouseDownListener(Action<PointerEventData> action)
        {
            OnMouseDownListeners.Add(action);
        }

        public void RemoveOnMouseDownListener(Action<PointerEventData> action)
        {
            OnMouseDownListeners.Remove(action);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var callback in OnMouseUpListeners)
            {
                callback(eventData);
            }
        }

        public void AddOnMouseUpListener(Action<PointerEventData> action)
        {
            OnMouseUpListeners.Add(action);
        }
        public void RemoveOnMouseUpListener(Action<PointerEventData> action)
        {
            OnMouseUpListeners.Remove(action);
        }
    }
}
