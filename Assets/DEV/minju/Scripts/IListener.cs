using UnityEngine;
using System.Collections;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum EVENT_TYPE {GAME_INIT, 
						GAME_END,
						AMMO_CHANGE,
						HEALTH_CHANGE,
						DEAD};
//-----------------------------------------------------------
//Listener interface to be implemented on Listener classes
public interface IListener
{
	//Notification function to be invoked on Listeners when events happen
	void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}
//-----------------------------------------------------------