using RPGDatabase.Runtime.Core;
using System.Linq;
using UnityEngine;

public class DemoRPGDatabase : MonoBehaviour
{
    public DemoActorInfoWindow ui;

    private DemoActor[] _actors;

    private RPGDatabaseManager _database;

    void Start()
    {
        //Load the database
        _database = new RPGDatabaseManager();
        _database.Load();

        //Fetch the values and inject them into your runtime classes
        var actorList = _database.FetchEntry<ActorDataList>();
        var classesList = _database.FetchEntry<ActorClassDataList>();
        _actors = new DemoActor[3];
        for (int i = 0; i < 3; i++)
        {
            var actorData = actorList.entries[i];
            var classData = classesList.entries.First<ActorClassData>(l => l.Id == actorData.classId);
            _actors[i] = new DemoActor(actorData, classData);

            var hp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.HP);
            var mp = _database.FetchAmount(actorData.Id, actorData.initialLevel, ActorAttributeType.MP);

            _actors[i].SetAttributes(hp, mp);
        }

        //Display the info in your UI
        for (int i = 0; i < 3; i++)
        {
            ui.UpdateDisplay(_actors[i], i);
        }
    }
}
