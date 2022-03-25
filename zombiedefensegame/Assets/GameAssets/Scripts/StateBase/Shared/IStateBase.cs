namespace TheyAreComing
{
    public interface IStateBase
    {
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}