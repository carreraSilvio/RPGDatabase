using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class Window : MonoBehaviour
    {

        protected Dictionary<string, Text> _texts;

        private void Awake()
        {
            _texts = new Dictionary<string, Text>();
            var rawTexts = GetComponentsInChildren<Text>();
            foreach(var rawText in rawTexts)
            {
                if (rawText.GetComponentInParent<UIElement>() != null) continue;
                if(_texts.ContainsKey(rawText.name))
                {
                    Debug.LogWarning($"{name}:{rawText.name} already added. Please rename it.");
                    continue;
                }
                _texts.Add(rawText.name, rawText);
            }
            PosBaseAwake();
        }

        protected virtual void PosBaseAwake()
        {

        }

        protected void ForceClose()
        {
            Deactivate();
        }

        public virtual void Open()
        {
            Activate();
        }

        public virtual void Close()
        {
            Deactivate();
        }

        protected void Activate()
        {
            gameObject.SetActive(true);
        }

        protected void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}