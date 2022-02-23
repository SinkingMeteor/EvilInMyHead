using Sheldier.Actors.Data;

namespace Sheldier.Gameplay.Effects
{
    public class FreezeMovementEffect : BaseMovementWrapper
    {
        public FreezeMovementEffect(EffectConfig config) : base(config)
        {
        }

        public override IMovementEffect Clone() => new FreezeMovementEffect(Config);

        protected override MovementDataPackage GetInternalMovement(MovementDataPackage data)
        {
            var dataPackage = _wrappedEntity.GetMovementData(data);
            dataPackage.Speed /= 2;
            return dataPackage;
        }
    }
}