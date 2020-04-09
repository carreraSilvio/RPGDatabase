using BrightLib.RPGDatabase.Runtime;
using UnityEngine;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class Actor
    {
        private ActorData _actorData;
        private ActorClassData _classData;

        public int exp;
        private int _level;

        public int hp;
        public int mp;

        public int str;
        public int intl;

        public int dex;
        public int agi;

        public int lck;

        public int def;
        public int res;

        public string wpnName;

        public Actor(ActorData data, ActorClassData classData)
        {
            _actorData = data;
            _classData = classData;
            _level = _actorData.initialLevel;
        }

        public void SetAttributes(int hp, int mp, int str, int intl, int dex, int agi, int lck, int def, int res)
        {
            this.hp = hp;
            this.mp = mp;

            this.str = str;
            this.intl = intl;

            this.dex = dex;
            this.agi = agi;

            this.lck = lck;

            this.def = def;
            this.res = res;
        }

        public Sprite FaceImage { get { return _actorData.graphics.face; } }


        public string Name { get { return _actorData.name; } }
        public string ClassName { get { return _classData.name; } }

        public int Level { get => _level; set => _level = value; }
    }
}