using Game.AttributeSystem.Buffs;
using Game.Weapons.TargetSelection;

namespace Game.Weapons
{
    public class AttackContext
    {
        public static AttackContext Empty = new(default, new TargetInfo[0], null);
        
        public TargetInfo Target { get; }
        public TargetInfo[] AllTargets { get; }
        public BuffHolder AttackerBuffHolder { get; }
        
        public AttackContext(TargetInfo targetInfo, TargetInfo[] allAllTargets, BuffHolder attackerBuffHolder)
        {
            Target = targetInfo;
            AllTargets = allAllTargets;
            AttackerBuffHolder = attackerBuffHolder;
        }
    }
}