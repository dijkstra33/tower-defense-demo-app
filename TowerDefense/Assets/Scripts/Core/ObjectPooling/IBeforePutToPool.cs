namespace Core.ObjectPooling
{
    public interface IBeforePutToPool
    {
        void Execute();
    }
}