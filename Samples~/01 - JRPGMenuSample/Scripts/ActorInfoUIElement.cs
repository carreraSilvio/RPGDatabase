using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorInfoUIElement : MonoBehaviour
    {
        [SerializeField] 
        private Text _actorLvl = default;

        [SerializeField] 
        private Text _actorHP = default;
        [SerializeField] 
        private Text _actorMP = default;

        private Button _button;

        public ButtonClickedEvent OnClick
        {
            get
            {
                return _button.onClick;
            }
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }


        public void UpdateDisplay(Actor actor)
        {
            _actorLvl.text = $"{actor.Level}";

            _actorHP.text = $"{actor.hp}/{actor.hp}";
            _actorMP.text = $"{actor.mp}/{actor.mp}";
        }
    }
}