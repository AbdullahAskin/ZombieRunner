namespace TheyAreComing
{
    public interface ITriggerable<in Base>
    {
        public void TriggerEnter(Base t);
        public void TriggerExit(Base t);
    }
}