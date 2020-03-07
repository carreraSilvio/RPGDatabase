using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.RPGDatabase.Demo
{
    public class DemoActorInfoUIElement : MonoBehaviour
    {
        [SerializeField] private Text _actorName = default;
        [SerializeField] private Text _actorClass = default;
        [SerializeField] private Text _actorLvl = default;

        [SerializeField] private Text _actorHP = default;
        [SerializeField] private Text _actorMP = default;


        public void UpdateDisplay(DemoActor actor)
        {
            _actorName.text = actor.Name;
            _actorClass.text = actor.ClassName;
            _actorLvl.text = $"Lvl: {actor.Level}";

            _actorHP.text = $"HP: {actor.hp}";
            _actorMP.text = $"MP: {actor.mp}";
        }
    }
}