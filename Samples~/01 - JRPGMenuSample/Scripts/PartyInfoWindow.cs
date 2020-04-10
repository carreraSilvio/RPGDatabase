using System;
using UnityEngine;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class PartyInfoWindow : Window
    {
        public Action<int> onActorClicked;

        [SerializeField] 
        private ActorInfoUIElement[] _actorInfoUIElements = default;

        public void UpdateDisplay(Actor actor, int index)
        {
            _actorInfoUIElements[index].UpdateDisplay(actor);
        }

        public override void Open()
        {
            for (int i = 0; i < _actorInfoUIElements.Length; i++)
            {
                var index = i;
                _actorInfoUIElements[i].OnClick.AddListener(()=> { onActorClicked?.Invoke(index); });
            }
            base.Open();
        }
        public override void Close()
        {
            for (int i = 0; i < _actorInfoUIElements.Length; i++)
            {
                _actorInfoUIElements[i].OnClick.RemoveAllListeners();
            }
            base.Close();
        }
    }
}