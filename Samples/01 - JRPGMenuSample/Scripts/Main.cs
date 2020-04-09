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
        public enum State { PartyInfo, ActorDetails };
        private State _state;

        private int _selectedActorIndex;

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

                var str = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Strength);
                var intl = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Intelligence);

                var dex = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Dextery);
                var agi = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Agility);

                var lck = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Luck);

                var def = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Defense);
                var res = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.Resistance);

                _actors[i].wpnName = _database.FetchWeapon(actorData.initialWeapon).name;

                _actors[i].SetAttributes(hp, mp, str, intl, dex, agi, lck, def, res);
            }

            //Displaying it on the UI
            for (int i = 0; i < actorList.Count; i++)
            {
                partyInfoWnd.UpdateDisplay(_actors[i], i);
            }

            TransitionTo(State.PartyInfo);
        }

        private void TransitionTo(State state)
        {
            if(state == State.PartyInfo)
            {
                partyInfoWnd.Open();
                partyInfoWnd.onActorClicked += HandleActorClicked;
            }
            else if (state == State.ActorDetails)
            {
                actorDetailsWnd.Open();
                actorDetailsWnd.UpdateDisplay(_actors[_selectedActorIndex]);
                actorDetailsWnd.onBackBtnClicked += HandleBackButtonClicked;
            }

            _state = state;
        }

        private void Update()
        {
            if(_state == State.ActorDetails)
            {
                if(Input.GetButtonDown("Cancel"))
                {
                    actorDetailsWnd.Close();
                    TransitionTo(State.PartyInfo);
                }
            }
        }

        #region Event Handlers
        private void HandleActorClicked(int index)
        {
            _selectedActorIndex = index;
            partyInfoWnd.onActorClicked -= HandleActorClicked;
            partyInfoWnd.Close();
            TransitionTo(State.ActorDetails);
        }

        private void HandleBackButtonClicked()
        {
            actorDetailsWnd.Close();
            TransitionTo(State.PartyInfo);
        }

        #endregion
    }
}