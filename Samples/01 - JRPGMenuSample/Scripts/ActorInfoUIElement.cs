using System.Collections.Generic;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorInfoUIElement : UIElement
    {
        
        public Image faceImage = default;

        private Button _button;

        private Dictionary<string, Text> _texts;

        public ButtonClickedEvent OnClick
        {
            get
            {
                return _button.onClick;
            }
        }

        public Button Button { get => _button; set => _button = value; }

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
            faceImage.sprite = actor.FaceImage;

            _texts["nameValue"].text = $"{actor.Name}";

            _texts["lvlValue"].text = $"{actor.Level}";

            _texts["hpValue"].text = $"{actor.hp}/{actor.hp}";
            _texts["mpValue"].text = $"{actor.mp}/{actor.mp}";
        }
    }
}