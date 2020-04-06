using System;
using UnityEngine;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class PartyInfoWindow : MonoBehaviour
    {
        public Action onClose;

        [SerializeField] 
        private ActorInfoUIElement[] _actorInfoUIElements = default;

        public void UpdateDisplay(Actor actor, int index)
        {
            _actorInfoUIElements[index].UpdateDisplay(actor);
        }

        public void Open()
        {
            foreach(var element in _actorInfoUIElements)
            {
                element.OnClick.AddListener(HandleActorClicked);
            }
            gameObject.SetActive(true);
        }

        public void Close()
        {
            foreach (var element in _actorInfoUIElements)
            {
                element.OnClick.RemoveListener(HandleActorClicked);
            }
            onClose?.Invoke();
            gameObject.SetActive(false);
        }

        private void HandleActorClicked()
        {
            Close();
        }
    }
}