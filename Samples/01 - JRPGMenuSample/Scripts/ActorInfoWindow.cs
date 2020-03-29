using UnityEngine;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorInfoWindow : MonoBehaviour
    {
        [SerializeField] 
        private ActorInfoUIElement[] _actorInfoUIElements = default;

        public void UpdateDisplay(Actor actorData, int index)
        {
            _actorInfoUIElements[index].UpdateDisplay(actorData);
        }
    }
}