using System;
using StateImplements;
using UnityEngine;

namespace StateImplements
{
    public class EnemyStates : MonoBehaviour, ITarget
    {
        [SerializeField] private float moveSpeed, timeForShoot;
        [SerializeField] private TargetFinder finder;
        public Vector3 CurrentPosition => transform.position;
        private EnemyStatesConfiguration _enemyStatesConfiguration;
        private TargetFinder _targetsFinder;
        private bool _inGame;

        private void Awake()
        {
            _enemyStatesConfiguration = new EnemyStatesConfiguration();
            _enemyStatesConfiguration.AddInitialState(EnemyStatesConfiguration.IdleState,
                new PatrolState(2,gameObject,2,finder, this)
            );
            _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.MovingToTargetState,
                new MoveToPlayer(finder, gameObject, this));
            _enemyStatesConfiguration.AddState(EnemyStatesConfiguration.AttackingTargetState,
                new AttackPlayer(finder, gameObject, this,timeForShoot));

            
            _inGame = true;
        }

        private void Start()
        {
            StartState(_enemyStatesConfiguration.GetInitialState());
        }

        private async void StartState(IEnemyState state, object data = null)
        {
            while (_inGame)
            {
                var resultData = await state.DoAction(data);
                var nextState = _enemyStatesConfiguration.GetState(resultData.NextStateId);
                state = nextState;
                data = resultData.ResultData;
            }
        }

        private void OnDestroy()
        {
            _inGame = false;
        }

        public void MoveTo(Vector2 patrolTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolTarget, moveSpeed * Time.deltaTime);
        }
    }
}