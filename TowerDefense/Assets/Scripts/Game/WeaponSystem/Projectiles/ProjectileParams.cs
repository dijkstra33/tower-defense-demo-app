namespace Game.Weapons.Projectiles
{
    public readonly struct ProjectileParams
    {
        public float Damage { get; }
        public float MoveSpeed { get; }
        public float MinExplodeDistance { get; }

        public ProjectileParams(float damage, float moveSpeed, float minExplodeDistance)
        {
            Damage = damage;
            MoveSpeed = moveSpeed;
            MinExplodeDistance = minExplodeDistance;
        }
    }
}