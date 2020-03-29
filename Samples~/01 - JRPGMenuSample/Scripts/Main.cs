using UnityEngine;
using BrightLib.RPGDatabase.Runtime;
using System;

namespace BrightLib.RPGDatabase.Samples.JRPGMenuSample
{
    public class Main : MonoBehaviour
    {
        public PartyInfoWindow partyInfoWnd;
        public ActorDetailsWindow actorDetailsWnd;

        private Actor[] _actors;

        private RPGDatabaseManager _database;

        void Start()
        {
            //Loading database
            _database = new RPGDatabaseManager();
            _database.Load();

            //Fetching data and injecting into runtime classes
            var actorList = _database.FetchEntry<ActorDataList>();
            _actors = new Actor[actorList.Count];

            for (int i = 0; i < actorList.Count; i++)
            {
                var actorData = actorList.entries[i];
                var classData = _database.FetchClassData(actorData.classId);
                _actors[i] = new Actor(actorData, classData);

                var hp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.HP);
                var mp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.MP);

                _actors[i].SetAttributes(hp, mp);
            }

            //Displaying it on the UI
            for (int i = 0; i < actorList.Count; i++)
            {
                partyInfoWnd.UpdateDisplay(_actors[i], i);
            }
            partyInfoWnd.Open();
            partyInfoWnd.onClose += HandlePartyInfoWindowClose;
        }

        private void HandlePartyInfoWindowClose()
        {
            partyInfoWnd.onClose -= HandlePartyInfoWindowClose;

            actorDetailsWnd.Open();
            actorDetailsWnd.UpdateDisplay(_actors[0]);
        }
    }
}