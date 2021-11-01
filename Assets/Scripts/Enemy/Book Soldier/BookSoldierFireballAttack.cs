using Amlos.Core;

namespace Amlos
{
    public class BookSoldierFireballAttack : Attack
    {

        public override bool Precondition(Health health)
        {
            return (Simulation.GetModel<Player>().health == health);
        }
    }
}