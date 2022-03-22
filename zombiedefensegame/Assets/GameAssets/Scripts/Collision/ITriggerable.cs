namespace TheyAreComing
{
    public interface ITriggerable<Base> where Base: CollisionManagerBase
    {
        public void TriggerEnter(Base t);
        public void TriggerExit(Base t);
    }
}