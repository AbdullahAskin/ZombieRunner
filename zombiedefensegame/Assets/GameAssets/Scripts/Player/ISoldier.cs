namespace TheyAreComing
{
    public interface ISoldier
    {
        public void OnAttack(EnemyBase enemyBase);
        public void OnIdle();
    }
}