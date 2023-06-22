using Game.Weapons.TargetSelection;

namespace Game.Weapons
{
    public class AttackContext
    {
        public static AttackContext Empty = new(new TargetInfo[0]);
        
        public TargetInfo[] Targets { get; }
        
        public AttackContext(TargetInfo[] targets)
        {
            Targets = targets;
        }
    }
}