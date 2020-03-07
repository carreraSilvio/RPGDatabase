# RPG Database
Create and manage the data for your RPG. Allows you to create the basic set-up for your grand adventure!

## Features
* Actors: Create the characters that will be part of your adventure.
* Classes: Define how each actor class attributes will grow, weapons allowed and skills unlocks.
* Skills: Robust options to create many types of skills .
* Items: Configure the items in your game. Easily create key story items.
* Weapons: Manage all the weapons in game and what how they wil affect the actors.

![Imgur2](https://i.imgur.com/9QZj5uc.gif)

## Prerequisites
Unity 2018.3 and up

## Install

### Unity 2019.3
* Open the package manager and point to the rep url
* ![Imgur](https://i.imgur.com/iYGgINz.png)

### Before Unity 2019.3
* Open the manifest and manually add

		{
    		"dependencies": {
        		"com.brightlib.rpgdatabase": "https://github.com/carreraSilvio/rpgdatabase.git"
    		}
		}


## Usage
1. Go to Tools/Database
2. Pick a tab you want to access the data
3. Alter the data as you see fit for your game
4. Do a "File/Save" or close the window to ensure the data is saved
5. Instanciate an object of type DatabaseManager and fetch the data you need