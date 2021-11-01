using Amlos.Core;
using System.Collections;
using UnityEngine;

namespace Amlos
{
    public abstract class EnemyControllerBase : MonoBehaviour
    {
        public string enemyName;

        protected virtual void Awake()
        {
            Simulation.GetModel<Game>().EnemySpawned(this);
            Debug.Log("Enemy " + name + "awake");
        }

        protected virtual void OnDestroy()
        {
            Simulation.GetModel<Game>().EnemyDied(this);
        }

        public virtual void EnemyDied()
        {
            Simulation.GetModel<GameEvent>().TriggerEnemyDiedUIEvent(this);
            Destroy(gameObject);
        }
    }
}