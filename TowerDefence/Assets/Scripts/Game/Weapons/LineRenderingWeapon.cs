using System;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class LineRenderingWeapon : AbstractWeapon
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private float lineShowDuration;

        private float hideLineAfter;
        
        protected override void Update()
        {
            base.Update();

            hideLineAfter = Math.Max(hideLineAfter - Time.deltaTime, 0);
            if (Mathf.Approximately(hideLineAfter, 0f))
            {
                lineRenderer.enabled = false;
            }
        }
        
        protected override void Attack(TargetInfo target)
        {
            ShowLineToTarget(target);
            target.Health.ReceiveDamage(GetAttackDamage(), ownerHealth);
        }

        private void ShowLineToTarget(TargetInfo target)
        {
            var laserPosition = lineRenderer.transform.position;
            var targetPosition = target.Transform.position;
            
            var direction = (targetPosition - laserPosition).normalized;
            lineRenderer.transform.rotation = Quaternion.LookRotation(direction);
            
            var distanceToTarget = Vector3.Distance(laserPosition, targetPosition);
            lineRenderer.transform.localScale = new Vector3(1, 1, distanceToTarget);
            
            hideLineAfter = lineShowDuration;
            lineRenderer.enabled = true;
        }
    }
}