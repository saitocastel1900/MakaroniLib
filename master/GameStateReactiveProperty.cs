using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum GameState
{
    None,
    Opening,
    Gaming,
    Death,
    Result,
}
[System.Serializable]
public class GameStateReactiveProperty : ReactiveProperty<GameStateReactiveProperty>
{
    public GameStateReactiveProperty() { }
    
    public GameStateReactiveProperty(GameStateReactiveProperty initialValue) : base(initialValue) { }
}
