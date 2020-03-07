using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DemoActorInfoUIElement : MonoBehaviour
{
   [SerializeField] private Text _actorName = default;
   [SerializeField] private Text _actorClass = default;
   [SerializeField] private Text _actorLvl = default;

   [SerializeField] private Text _actorHP = default;
   [SerializeField] private Text _actorMP = default;


    public void UpdateDisplay(DemoActor actor)
    {
        _actorName.text = actor.name;
        _actorClass.text = actor.className;
        _actorLvl.text = $"Lvl: {actor.level}";

        _actorHP.text = $"HP: {actor.hp}";
        _actorMP.text = $"MP: {actor.mp}";
    }
}
