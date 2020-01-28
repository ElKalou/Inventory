
public interface IEventSender<T> where T : EventBase
{
    void SendEvent(T t);
}

