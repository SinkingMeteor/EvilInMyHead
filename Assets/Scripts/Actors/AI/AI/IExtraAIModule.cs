namespace Sheldier.Actors.AI
{
    public interface IExtraAIModule
    {
        void Initialize(ActorInternalData data, ActorsAIModule aiModule);
        void Dispose();
    }
}