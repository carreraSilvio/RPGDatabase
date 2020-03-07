using BrightLib.RPGDatabase.Runtime;

namespace RPGDatabase.Demo
{
    public class DemoActor
    {
        private ActorData _actorData;
        private ActorClassData _classData;

        public int xp;
        private int _level;

        public int hp;
        public int mp;

        public DemoActor(ActorData data, ActorClassData classData)
        {
            _actorData = data;
            _classData = classData;
            _level = _actorData.initialLevel;
        }

        public void SetAttributes(int hp, int mp)
        {
            this.hp = hp;
            this.mp = mp;
        }

        public string Name { get { return _actorData.name; } }
        public string ClassName { get { return _classData.name; } }

        public int Level { get => _level; set => _level = value; }
    }
}