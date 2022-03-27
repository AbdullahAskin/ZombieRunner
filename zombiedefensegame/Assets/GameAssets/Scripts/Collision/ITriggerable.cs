namespace TheyAreComing
{
    public interface ITriggerable<T> where T : CollisionManagerBase
    {
        public void TriggerEnter(T t);
    }
}