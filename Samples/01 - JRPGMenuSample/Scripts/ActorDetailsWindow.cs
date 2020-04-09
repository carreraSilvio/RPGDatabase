using System;
using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorDetailsWindow : Window
    {
        public Action onBackBtnClicked;

        private ActorInfoUIElement _actorInfo;

        protected override void PosBaseAwake()
        {
            _actorInfo = GetComponentInChildren<ActorInfoUIElement>();
           _buttons["backBtn"].onClick.AddListener(()=> { onBackBtnClicked?.Invoke(); });
        }

        public void UpdateDisplay(Actor actor)
        {
            _actorInfo.Button.interactable = false;
            _actorInfo.UpdateDisplay(actor);

            //Attrs
            _texts["strValue"].text = $"{actor.str}";
            _texts["intValue"].text = $"{actor.intl}";

            _texts["dexValue"].text = $"{actor.dex}";
            _texts["agiValue"].text = $"{actor.agi}";

            _texts["lckValue"].text = $"{actor.lck}";

            _texts["defValue"].text = $"{actor.def}";
            _texts["resValue"].text = $"{actor.res}";

            //Equipment
            _texts["wpnValue"].text = $"{actor.wpnName}";
        }
    }
}