﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event {}

public class ButtonPressed : Event {
	public string buttonTitle;
	public int playerNum;
	public ButtonPressed(string name, int num){
		buttonTitle = name;
		playerNum = num;
	}
}

public class AxisPressed : Event {
    public int direction;
    public int playerNum;
    public AxisPressed(int dir, int num)
    {
        direction = dir;
        playerNum = num;
    }
}

public class SceneTransition : Event
{
    public string sceneName;
    public SceneTransition(string name)
    {
        sceneName = name;
    }
}

public class AbilityEnded : Event
{
    public Ability ability;
    public AbilityEnded(Ability ab)
    {
        ability = ab;
    }
}


public class FightAdvance : Event
{
    public int roundNum;
    public FightAdvance(int num)
    {
        roundNum = num;
    }
}

public class PlayerInputPaused : Event {
	public Player player;
	public PlayerInputPaused(Player pl){
		player = pl;
	}
}

public class PlayerHooked : Event {
	public Player hookedPlayer;
	public PlayerHooked(Player player){
		hookedPlayer = player;
	}
}

public class Reset : Event {}

public class PlayerFall : Event
{
    public Player fallenPlayer;
    public PlayerFall(Player fallenPl)
    {
        fallenPlayer = fallenPl;
    }
}

public class GameOver : Event {
	public Player losingPlayer;
	public GameOver(Player loser){
		losingPlayer = loser;
	}
}

public class DialoguePicked : Event {
	public Dialogue dialogue;
	public int pickedByPlayerNum;
	public GameObject optionObject;
	public DialoguePicked(Dialogue dia, int playerNum){
		dialogue = dia;
		pickedByPlayerNum = playerNum;
	}
}

public class PlayerDamaged : Event
{
    public int playerNum;
    public PlayerDamaged(int num)
    {
        playerNum = num;
    }
}

public class EventManager {

	public delegate void EventDelegate<T>(T e) where T: Event;
	private delegate void EventDelegate(Event e);

	private Dictionary <System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
	private List<Event> queuedEvents = new List<Event> ();
	private object queueLock = new object();

	public void Register<T> (EventDelegate<T> del) where T: Event {
		if (delegateLookup.ContainsKey (del)) {
			return;
		}

		EventDelegate internalDelegate = (e) => del ((T)e);
		delegateLookup [del] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue (typeof(T), out tempDel)) {
			delegates [typeof(T)] = tempDel + internalDelegate;
		} else {
			delegates [typeof(T)] = internalDelegate;
		}
	}

	public void Unregister<T> (EventDelegate<T> del) where T: Event {
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue (del, out internalDelegate)) {
			EventDelegate tempDel;
			if (delegates.TryGetValue (typeof(T), out tempDel)) {
				tempDel -= internalDelegate;
				if (tempDel == null) {
					delegates.Remove (typeof(T));
				} else {
					delegates [typeof(T)] = tempDel;
				}
			}
			delegateLookup.Remove (del);
		}
	}

	public void Clear(){
		lock (queueLock) {
			if (delegates != null) {
				delegates.Clear ();
			}
			if (delegateLookup != null) {
				delegateLookup.Clear ();
			}
			if (queuedEvents != null) {
				queuedEvents.Clear ();
			}
		}
	}

	public void Fire(Event e){
		EventDelegate del;
		if (delegates.TryGetValue (e.GetType (), out del)) {
			del.Invoke (e);
		}
	}

	public void ProcessQueuedEvents(){
		List<Event> events;
		lock (queueLock) {
			if (queuedEvents.Count > 0) {
				events = new List<Event> (queuedEvents);
				queuedEvents.Clear ();
			} else {
				return;
			}
		}

		foreach (Event e in events) {
			Fire (e);
		}
	}

	public void Queue(Event e){
		lock (queueLock) {
			queuedEvents.Add (e);
		}
	}

}
