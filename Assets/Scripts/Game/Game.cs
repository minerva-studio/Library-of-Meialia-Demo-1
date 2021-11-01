using Amlos.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Amlos
{
    [Serializable]
    public class Game
    {
        public Configuration configuration;

        [Header("Progress")]
        public bool hasStarted;
        public bool isEnemyCleared;
        public string roomID;
        public Level currentLevelData;
        public List<MonoBehaviour> enemies = new List<MonoBehaviour>();

        public Player player => Simulation.GetModel<Player>();

        public string Language => configuration is null ? "en_us" : configuration.Language;

        static Game()
        {
            Game instance = new Game();
            instance.hasStarted = false;
            instance.roomID = RoomID.mainHall;
            instance.isEnemyCleared = true;
            instance.SetModel();
        }

        public void LoadConfiguration()
        {
            configuration = ConfigurationLoader.GetConfiguration();
            configuration.Sound.SetModel();
        }

        public void EnterMainHall()
        {
            this.hasStarted = true;
            Simulation.GetModel<Player>().SetPlayerMovementBehaviours(true);
            currentLevelData = GameData.instance.mainHall;
            isEnemyCleared = true;
            roomID = RoomID.mainHall;
        }

        public void BackToMainHall()
        {
            KillAllEnemy();
            KillAllFireball();
            player.SetPlayerMovementBehaviours(false);
            player.SetPlayerAttackBehaviours(false);
            player.ResetHealth();
            this.currentLevelData = GameData.instance.mainHall;
            this.hasStarted = false;
            this.isEnemyCleared = true;
            this.roomID = RoomID.mainHall;
            MapLoader.Instance.Load(currentLevelData.levelMap);
            UIControl.Instance.OpenGameStartUI();
            SetPlayerToMainGate();
        }

        public void EnterLevel(string roomID)
        {
            this.hasStarted = true;
            this.currentLevelData = GameData.instance.GetLevel(roomID);
            this.roomID = roomID;
            this.isEnemyCleared = false;

            Simulation.GetModel<Player>().ResetHealth();

            StartLevel();
        }

        public void StartLevel()
        {
            MapLoader.Instance.Load(currentLevelData.levelMap);
            currentLevelData.spawnPoints.SpawnAll();
            Simulation.GetModel<Player>().SetPlayerAttackBehaviours(true);
            SetPlayerToMainGate();
        }

        private void SetPlayerToMainGate()
        {
            GateBehaviour gateBehaviour = GateBehaviour.FindGate(currentLevelData.startGateId);
            Simulation.GetModel<Player>().SetPosition(gateBehaviour.transform.position);
        }




        public void EnemySpawned(MonoBehaviour enemy)
        {
            enemies.Add(enemy);
        }
        public void EnemyDied(MonoBehaviour enemy)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0) isEnemyCleared = true;
        }
        public void KillAllEnemy()
        {
            foreach (var item in enemies)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
            enemies.Clear();
            if (enemies.Count == 0) isEnemyCleared = true;
        }
        public void KillAllFireball()
        {
            List<PlayerFireBallController> fireBalls = Simulation.GetModel<PlayerFireballs>().fireBalls;
            foreach (var item in fireBalls)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
            fireBalls.Clear();
        }
    }
}