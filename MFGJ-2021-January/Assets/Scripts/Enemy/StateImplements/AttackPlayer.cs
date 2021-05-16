using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StateImplements
{
    public class AttackPlayer : IEnemyState
    {
        private readonly TargetFinder _targetFinder;
        private readonly GameObject _enemy;
        private readonly ITarget _enemySelf;
        private float _timeBtwShots, _timeBtwShotsDelta;

        public AttackPlayer(TargetFinder targetFinder, GameObject enemy, ITarget enemySelf,float timeBtwShots)
        {
            _targetFinder = targetFinder;
            _enemy = enemy;
            _enemySelf = enemySelf;
            _timeBtwShots = timeBtwShots;
            _timeBtwShotsDelta = _timeBtwShots;
        }

        public async Task<StateResult> DoAction(object data)
        {
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            if (!_targetFinder.PlayerIsCloseForShoot(_enemy.transform.position))
            {
                return new StateResult(EnemyStatesConfiguration.MovingToTargetState);
            }
            Shoot();
            return new StateResult(EnemyStatesConfiguration.AttackingTargetState);
        }

        private void Shoot()
        {
            if (_timeBtwShotsDelta <= 0)
            {
                Debug.Log("PUW");
                _timeBtwShotsDelta = _timeBtwShots;
            }
            else
            {
                _timeBtwShotsDelta -= Time.deltaTime;
            }
        }
    }
}