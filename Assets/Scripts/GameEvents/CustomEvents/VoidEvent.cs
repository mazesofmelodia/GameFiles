using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    //Raise a new void event when the event is raised
    public void Raise() => Raise(new Void());
}
