using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoActorInfoWindow : MonoBehaviour
{
    [SerializeField] private DemoActorInfoUIElement[] _actorInfoUIElements = default;

    public void UpdateDisplay(DemoActor actorData, int index)
    {
        _actorInfoUIElements[index].UpdateDisplay(actorData);
    }
}
