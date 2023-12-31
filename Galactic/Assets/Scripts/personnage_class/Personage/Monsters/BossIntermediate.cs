
using System.Collections.Generic;
using UnityEngine;

namespace personnage_class.Personage.Monsters
{
    public class BossIntermediate : Monster
    {
        public override EnumType Type() => EnumType.IntermediateMonster;

        public BossIntermediate(string name, int life = 10, int maxlife = 20, int damage = 5, int boost = 1,
            int inventorySize = 8, int level = 0) : base(name, (int)(maxlife * 2.5), (int)(maxlife * 2.5),
            (int)(damage * 1.5), boost, inventorySize, (int)(level * 1.5)) // life *2.5 damage *1.5 level *1.5 
        {
            inFight = false;
            Inventory[0] = new Gun(100, Damage, Boost);
            Change_Weapon_Equipped(0);

            
            int i = 1;
            while (i > 0)
            {
                Inventory[i] = new Kit_Heal(1, 100 * level * Boost);
                i--;
            }

            int nb_Food = Random.Range(2, 7), nb_Boost = inventorySize - nb_Food;
            while (nb_Boost > 0)
            {
                Inventory[nb_Boost - 1] =
                    new Potion_Boost("Potion_Stamina", 50, false, 0, EnumsItem.Boost, Boost * level);
                nb_Boost--;
            }

            while (nb_Food > 0)
            {
                Inventory[nb_Boost + nb_Food] = new Food("Hamburger", 50, life * level);
                nb_Food--;
            }

        }


        public override int Target(List<Personnage> heros , int find)
        {
            var target = heros[0];
            int pos = -1;
            // trouver le médecin 
            if (find is >= 0 and < 25)
                for (int i = 1; i < heros.Count - 1; i++)
                {
                    if (heros[i].Get_damage() > target.Get_damage())
                    {
                        target = heros[i];
                        pos = i;
                    }
                }
            else
                for (int i = 1; i < heros.Count - 1; i++)
                {
                    if (heros[i].Getlife < target.Getlife)
                    {
                        target = heros[i];
                        pos = i;
                    }
                }

            return pos;
        }
    }

}