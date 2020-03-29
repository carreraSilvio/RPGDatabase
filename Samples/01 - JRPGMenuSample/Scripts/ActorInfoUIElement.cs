using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorInfoUIElement : MonoBehaviour
    {
        [SerializeField] 
        private Text _actorName = default;
        [SerializeField] 
        private Text _actorClass = default;
        [SerializeField] 
        private Text _actorLvl = default;

        [SerializeField] 
        private Text _actorHP = default;
        [SerializeField] 
        private Text _actorMP = default;


        public void UpdateDisplay(Actor actor)
        {
            //_actorName.text = actor.Name;
            //_actorClass.text = actor.ClassName;
            _actorLvl.text = $"{actor.Level}";

            _actorHP.text = $"{actor.hp}/{actor.hp}";
            _actorMP.text = $"{actor.mp}/{actor.mp}";
        }
    }
}