using System.Collections.Generic;
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

        private Dictionary<string, Text> _texts;

        public ButtonClickedEvent OnClick
        {
            get
            {
                return _button.onClick;
            }
        }

        private void Awake()
        {
            _texts = new Dictionary<string, Text>();

            _button = GetComponent<Button>();
            
            var rawTexts = GetComponentsInChildren<Text>();
            foreach(var text in rawTexts)
            {
                _texts.Add(text.name, text);
            }
        }


        public void UpdateDisplay(Actor actor)
        {
            _texts["nameValue"].text = $"{actor.Name}";
            _actorLvl.text = $"{actor.Level}";

            _actorHP.text = $"{actor.hp}/{actor.hp}";
            _actorMP.text = $"{actor.mp}/{actor.mp}";
        }
    }
}