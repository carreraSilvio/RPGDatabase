using RPGDatabase.Runtime.Core;
using UnityEngine;

namespace RPGDatabase.Demo
{
    public class DemoRPGDatabase : MonoBehaviour
    {
        public DemoActorInfoWindow ui;

        private DemoActor[] _actors;

        private RPGDatabaseManager _database;

        void Start()
        {
            //Loading database
            _database = new RPGDatabaseManager();
            _database.Load();

            //Fetching data and injecting into runtime classes
            var actorList = _database.FetchEntry<ActorDataList>();
            _actors = new DemoActor[3];

            for (int i = 0; i < 3; i++)
            {
                var actorData = actorList.entries[i];
                var classData = _database.FetchClassData(actorData.classId);
                _actors[i] = new DemoActor(actorData, classData);

                var hp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.HP);
                var mp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.MP);

                _actors[i].SetAttributes(hp, mp);
            }

            //Displaying it on the UI
            for (int i = 0; i < 3; i++)
            {
                ui.UpdateDisplay(_actors[i], i);
            }
        }
    }
}