A system event implemented using an abstract base classes found in abstract baseEvent. 
A system event is defined by:
- an event (scriptable object)
- a data carried by this event (scriptable object). Each event subtype carry a unique data type.
- an event listener that listen for a particular subtype of the event base class. (monobehaviour)

The DataCatcher is an eventListeners hub, that listen for a unique event subtype. Depending on the event received
it can: transmit the data to a IBaseEventDataUser, disable a IBaseEventDataUser or suspend the data transmission
to a IBaseEventDataUser. 

IEventSender is an interface with a SendEvent method.

From these abstract elements we can define various event system that are totally independent and can not interfere 
with each other. A generic event system is define in GenericGameEvent where the data carried by the GameEvent
are simply a scriptable object, so can be pretty much anything. It's a good thing to perform a cast at the receiver
to be sure that we indeed receive the correct type of data. The kCaster script contains a static method for that. 

An other event system is define in InputEvent. The idea of these events is not to carry data but to be send 
depending on the user input. The InputEventListener is slightly modified so that when the listener is trigger,
it checks in its parents to see if there is a InputEventController that has the hand on the program. If
not the listener does not trigger the event. This allows to determine which "global" system should receive the
information from the user input. 
Note: if no InputEventController is found the listener trigger the event anyway, make it independent