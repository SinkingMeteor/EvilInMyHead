namespace Sheldier.Actors.Hand
{
    public interface IHandInputReceiver
    {
        float GetHandRotation(float angle);
        void Dispose();
        
    }
}