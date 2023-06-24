using System.Collections;
using Game.AttributeSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.AttackEffects
{
    public class LineRendererAttackEffect : AbstractWeaponAttackEffect
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private float lineShowDuration;

        protected override void HandleWeaponAttack(TargetInfo[] targets, AbstractAttributeOwner weaponAttributeOwner)
        {
            if (targets == null || targets.Length == 0)
            {
                return;
            }
            
            // TODO: Temporary it works only with one target.
            var firstTarget = targets[0];
            StartCoroutine(ShowLineToTarget(firstTarget));
        }

        private IEnumerator ShowLineToTarget(TargetInfo target)
        {
            var laserPosition = lineRenderer.transform.position;
            var targetPosition = target.Transform.position;
            
            var direction = (targetPosition - laserPosition).normalized;
            lineRenderer.transform.rotation = Quaternion.LookRotation(direction);
            
            var distanceToTarget = Vector3.Distance(laserPosition, targetPosition);
            lineRenderer.transform.localScale = new Vector3(1, 1, distanceToTarget);
            
            lineRenderer.enabled = true;

            yield return new WaitForSeconds(lineShowDuration);
            
            lineRenderer.enabled = false;
        }
    }
}