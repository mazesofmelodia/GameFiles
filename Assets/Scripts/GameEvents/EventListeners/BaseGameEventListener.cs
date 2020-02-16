using UnityEngine;
using UnityEngine.Events;

//Event listener for generic event types, checks to soo if it's a unity event
public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, 
    IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E gameEvent;               //Game Event to listen for

    //Getter/Setter for the game event
    public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

    [SerializeField] private UER unityEventResponse;    //Responder to unity event

    private void OnEnable()
    {
        //If the gameEvent is null ignore the function
        if(gameEvent == null)
        {
            return;
        }

        //Register the event listener
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        //If the gameEvent is null ignore the function
        if (gameEvent == null)
        {
            return;
        }

        //UnRegister the event listener
        GameEvent.UnRegisterListener(this);
    }

    public void OnEventRaised(T item)
    {
        //If the event response isn't null
        if(unityEventResponse != null)
        {
            //Invoke the response
            unityEventResponse.Invoke(item);
        }
    }
}
