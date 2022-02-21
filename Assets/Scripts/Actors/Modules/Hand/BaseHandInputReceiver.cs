namespace Sheldier.Actors.Hand
{
    public class BaseHandInputReceiver : IHandInputReceiver
    {
        public float GetHandRotation(float angle)
        {
            return angle;
        }

        public void Dispose()
        {
        }
    }
}