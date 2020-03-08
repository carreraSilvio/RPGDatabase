# RPG Database
Create and manage the data for your RPG. Allows you to create the basic set-up for your grand adventure!

![Imgur](https://i.imgur.com/4b4oGt8.gif)

## Features
* Actors: Create the characters that will be part of your adventure.
* Classes: Define how each actor class attributes will grow, weapons allowed and skills unlocks.
* Skills: Robust options to create many types of skills .
* Items: Configure the items in your game. Easily create key story items.
* Weapons: Manage all the weapons in game and what how they wil affect the actors.

## Prerequisites
Unity 2018.3 and up

## Install

### Unity 2019.3
1. Open the package manager and point to the rep url

![Imgur](https://i.imgur.com/iYGgINz.png)

### Before Unity 2019.3

#### Option A
1. Open the manifest
2. Add the repo url either via https or ssh

		{
    		"dependencies": {
        		"com.brightlib.rpgdatabase": "https://github.com/carreraSilvio/rpgdatabase.git"
    		}
		}

#### Option B
1. Clone or download the project zip
2. Inside your project Assets folder create a folder called RPGDatabase
3. Copy the repo there

## Usage

### Edit Database
1. Go to Tools/Database
2. Pick a tab you want to access the data
3. Alter the data as you see fit for your game
4. Do a "File/Save" or close the window to ensure the data is saved

### Read Database

```csharp
//Loading database
var _database = new RPGDatabaseManager();
_database.Load();

//Fetching data 
var actorList = _database.FetchEntry<ActorDataList>();
foreach(var actor in actorList)
{
    Debug.Log($"Actor name is {actor.name}");
    Debug.Log($"Actor class is {_database.FetchClassData(actorData.classId).name}");
    Debug.Log($"Actor HP at level 10 is {_database.FetchAmount(actor.Id, 10, ActorAttributeType.HP)}");
}
```
