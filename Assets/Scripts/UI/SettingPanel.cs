using Amlos.Core;
using Minerva.Module;
using UnityEngine;

namespace Amlos
{
    public class SettingPanel : SingletonScript<SettingPanel>
    {
        public void SaveAndQuit()
        {
            gameObject.SetActive(false);
            ConfigurationLoader.Save(Simulation.GetModel<Game>().configuration);
        }
    }
}