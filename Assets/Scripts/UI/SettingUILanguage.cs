using Amlos;
using Amlos.Core;
using Minerva.Localization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Amlos
{

    public class SettingUILanguage : MonoBehaviour
    {
        public Dropdown language;
        public Toggle optionTemplate;


        public void Start()
        {
            AddAllOption();
            try
            {
                language.value = (int)Enum.Parse(typeof(LocalizationCountry), Simulation.GetModel<Game>().configuration.Language);
            }
            catch
            {
                language.value = 0;
            }
        }

        public void AddAllOption()
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            foreach (LocalizationCountry item in Enum.GetValues(typeof(LocalizationCountry)))
            {
                var data = new Dropdown.OptionData();
                data.text = item.ToString();
                //data.image = optionTemplate.image.sprite;
                options.Add(data);
            }
            language.AddOptions(options);
        }

        public void ChangeLang(int id)
        {
            LocalizationCountry language = (LocalizationCountry)id;
            Simulation.GetModel<Game>().configuration.Language = language.ToString();
            LanguageLoaderBase.ReloadAll();
        }
    }
}