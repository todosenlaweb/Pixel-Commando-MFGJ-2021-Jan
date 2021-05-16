using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StateImplements
{
    public class PatrolState : IEnemyState
    {
        private Vector3 randomStep;
        private float waitTime;
        private readonly float _randomStepSize;
        private readonly Transform _tarnsform;
        private float _startWaitTime;
        private readonly TargetFinder _finder;
        private readonly ITarget _enemySelf;

        public PatrolState(float randomStepSize, GameObject enemy, float startWaitTime, TargetFinder finder, ITarget enemySelf)
        {
            _tarnsform = enemy.transform;
            _randomStepSize = randomStepSize;
            _startWaitTime = startWaitTime;
            _finder = finder;
            _enemySelf = enemySelf;
        }

        public async Task<StateResult> DoAction(object data)
        {
            Vector2 patrolTarget = Vector2.zero;
            randomStep = new Vector3(Random.Range(-_randomStepSize, _randomStepSize), Random.Range(-_randomStepSize, _randomStepSize), 0);
            patrolTarget = _tarnsform.position + randomStep;
            waitTime = _startWaitTime;
            await Task.Delay(TimeSpan.FromSeconds(.2f));
            if (_finder.PlayerIsclose(_tarnsform.position))
            {
                return new StateResult(EnemyStatesConfiguration.MovingToTargetState);
            }
            _enemySelf.MoveTo(patrolTarget);
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            return new StateResult(EnemyStatesConfiguration.IdleState);
        }
    }
}