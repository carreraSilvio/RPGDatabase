using UnityEngine;
using UnityEngine.UI;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class ActorDetailsWindow : MonoBehaviour
    {
        [SerializeField]
        private Text _name = default;

        [SerializeField]
        private Text _lv = default;

        [SerializeField]
        private Text _hp = default;

        [SerializeField]
        private Text _mp = default;

        [SerializeField]
        private Text _exp = default;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void UpdateDisplay(Actor actor)
        {
            _name.text = $"{actor.Name}";
            _lv.text = $"{actor.Level}";
            _hp.text = $"{actor.hp}/{actor.hp}";
            _mp.text = $"{actor.mp}/{actor.mp}";
            _exp.text = $"{actor.exp}/{actor.exp}";
        }
    }
}