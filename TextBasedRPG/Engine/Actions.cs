using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPG
{
    static class Actions
    {
        static public readonly DestroyActions destroy = new DestroyActions();
        static public readonly DisarmActions disarm = new DisarmActions();
        static public readonly AttackActions attack = new AttackActions();
        static public readonly SneakAttackActions sneakAttack = new SneakAttackActions();

        static public readonly InspectActions inspect = new InspectActions();
        static public readonly NavigateActions navigate = new NavigateActions();
        static public readonly OpenActions open = new OpenActions();
        static public readonly UseActions use = new UseActions();

        static public readonly LevelUpActions levelUp = new LevelUpActions();

        static public readonly EquipActions Equip = new EquipActions();
    }
}
