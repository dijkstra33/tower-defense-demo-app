using Game.Weapons.TargetSelection;

namespace Game.Weapons
{
    public class AttackContext
    {
        public static AttackContext Empty = new(default, new TargetInfo[0], null);
        
        public TargetInfo Target { get; }
        public TargetInfo[] AllTargets { get; }
        public AbstractWeapon AttackerWeapon { get; }
        
        public AttackContext(TargetInfo targetInfo, TargetInfo[] allAllTargets, AbstractWeapon attackerWeapon)
        {
            Target = targetInfo;
            AllTargets = allAllTargets;
            AttackerWeapon = attackerWeapon;
        }
    }
}