using Amlos.Core;
using MeialianFonts;
using Minerva.Localization;
using Minerva.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{
    public class PlayerWordLine : SingletonScript<PlayerWordLine>
    {
        public float cooldown;
        public string eventName;
        public string wordline;

        public Text text;
        public MeialianText meialianText;

        private void OnValidate()
        {
            SetText(wordline, wordline);
        }

        protected override void Awake()
        {
            base.Awake();
            Simulation.GetModel<GameEvent>().enemyDiedEvent += EnemyDiedEvent;
            Simulation.GetModel<GameEvent>().fireballCloseToPlayer += PlayerAlmostHitEvent;
            Simulation.GetModel<GameEvent>().playerHurtEvent += PlayerHitEvent;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (cooldown > 0) cooldown -= Time.deltaTime;
        }

        public void SetEvent(string eventName)
        {
            if (cooldown > 0) return;
            SetEventEmergency(eventName);
        }

        public void SetEventEmergency(string eventName)
        {
            gameObject.SetActive(true);
            this.eventName = eventName;

            var local = GetWordLine(eventName);
            var eng = GetEnglishWordLind(eventName);

            SetText(local, eng);
        }

        private void SetText(string local, string eng)
        {
            this.text.text = local;
            wordline = local;
            meialianText.LoadText(eng);
        }

        public void SetText(string eventName, float time)
        {
            gameObject.SetActive(true);
            StartCoroutine(SetTextInSecond(eventName, time));
        }
        public void SetTextEmergency(string eventName, float time)
        {
            gameObject.SetActive(true);
            StartCoroutine(SetTextEnemyInSecond(eventName, time));
        }

        IEnumerator SetTextInSecond(string eventName, float time)
        {
            SetEvent(eventName);
            cooldown = time;
            yield return new WaitForSeconds(time);
            if (this.eventName == eventName)
            {
                ClearText();
                gameObject.SetActive(false);
            }
        }

        IEnumerator SetTextEnemyInSecond(string eventName, float time)
        {
            SetEventEmergency(eventName);
            cooldown = time;
            yield return new WaitForSeconds(time);
            if (this.eventName == eventName)
            {
                ClearText();
                gameObject.SetActive(false);
            }
        }

        public void ClearText()
        {
            SetEvent("");
            gameObject.SetActive(false);
        }





        private void EnemyDiedEvent(EnemyControllerBase obj)
        {
            string eventName = "Hit" + obj.enemyName;
            SetText(eventName, 1);
        }

        private void PlayerAlmostHitEvent(FireBallControllerBase fireBallControllerBase)
        {
            string eventName = "AlmostHurt";
            SetText(eventName, 1);
        }

        private void PlayerHitEvent(Health health)
        {
            if (health == Simulation.GetModel<Player>().health)
            {
                string eventName = "Hurt";
                SetTextEmergency(eventName, 1);
            }
        }





        private string GetWordLine(string eventName)
        {
            int v = UnityEngine.Random.Range(0, GameData.instance.playerWordLineData.GetEventCount(eventName));
            string v1 = this.Lang(eventName, v.ToString());
            Debug.LogError(v1);
            return v1;
        }

        private string GetEnglishWordLind(string eventName)
        {
            int v = UnityEngine.Random.Range(0, GameData.instance.playerWordLineData.GetEventCount(eventName));
            string v1 = this.Lang(eventName, true, v.ToString());
            Debug.LogError(v1);
            return v1;
        }
    }


    [Serializable]
    public class PlayerWordLineData
    {
        [SerializeField] List<WordLindData> WordLindDatas;

        public int GetEventCount(string eventName)
        {
            WordLindData wordLindData = WordLindDatas.Find(c => c.eventName == eventName);
            if (wordLindData != null) return wordLindData.count;
            return 0;
        }


        [Serializable]
        class WordLindData
        {
            public string eventName;
            public int count;
        }
    }
}
