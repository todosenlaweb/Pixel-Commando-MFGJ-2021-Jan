using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StateImplements
{
    public class MoveToPlayer : IEnemyState
    {
        private readonly TargetFinder _targetFinder;
        private readonly GameObject _enemy;
        private readonly ITarget _enemySelf;

        public MoveToPlayer(TargetFinder targetFinder, GameObject enemy, ITarget enemySelf)
        {
            _targetFinder = targetFinder;
            _enemy = enemy;
            _enemySelf = enemySelf;
        }

        public async Task<StateResult> DoAction(object data)
        {
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            var positionPlayer = _targetFinder.GetPositionPlayer();
            var diff = positionPlayer - (Vector2)_enemy.transform.position;
            var target = (Vector2)_enemy.transform.position + diff;
            _enemySelf.MoveTo(target);

            if (!_targetFinder.PlayerIsclose(_enemy.transform.position))
            {
                return new StateResult(EnemyStatesConfiguration.IdleState);
            }

            if (_targetFinder.PlayerIsCloseForShoot(_enemy.transform.position))
            {
                return new StateResult(EnemyStatesConfiguration.AttackingTargetState);
            }
            
            return new StateResult(EnemyStatesConfiguration.MovingToTargetState);
        }
    }
}