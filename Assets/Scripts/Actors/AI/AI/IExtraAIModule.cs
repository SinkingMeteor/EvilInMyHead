namespace Sheldier.Actors.AI
{
    public interface IExtraAIModule
    {
        void Initialize(ActorInternalData data);
        void Dispose();
    }
}