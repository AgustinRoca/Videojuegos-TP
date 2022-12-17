//namespace DefaultNamespace;

public interface IDamageable
{
    int MaxHp { get; }
    int Hp { get; }

    void TakeDamage(int damage);

    void Die();
}