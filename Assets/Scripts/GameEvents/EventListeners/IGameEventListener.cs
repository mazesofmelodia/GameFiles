//Interface for game event listeners
public interface IGameEventListener<T>
{
    //Takes in a generic value
    void OnEventRaised(T item);
}
