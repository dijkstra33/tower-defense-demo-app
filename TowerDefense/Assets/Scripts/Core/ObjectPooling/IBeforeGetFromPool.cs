namespace Core.ObjectPooling
{
    public interface IBeforeGetFromPool
    {
        void Execute();
    }
}