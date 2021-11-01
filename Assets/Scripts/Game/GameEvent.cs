using System;

namespace Amlos
{
    public class GameEvent
    {
        public Action<EnemyControllerBase> enemyDiedEvent;

        public Action<Level> levelStartEvent;
        public Action<Level> levelPassedEvent;

        public Action<FireBallControllerBase> fireballCloseToPlayer;
        public Action<Health> playerHurtEvent;

        public void TriggerEnemyDiedUIEvent(EnemyControllerBase enemyControllerBase)
        {
            enemyDiedEvent?.Invoke(enemyControllerBase);
        }

        public void TriggerLevelStartUIEvent(Level level)
        {
            levelStartEvent?.Invoke(level);
        }

        public void TriggerLevelPassedUIEvent(Level level)
        {
            levelPassedEvent?.Invoke(level);
        }

        public void TriggerFireballCloseToPlayer(FireBallControllerBase fireBallControllerBase)
        {
            fireballCloseToPlayer?.Invoke(fireBallControllerBase);
        }

        public void TriggerPlayerHurtEvent(Health health)
        {
            playerHurtEvent?.Invoke(health);
        }
    }
}