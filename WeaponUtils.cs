using System.Linq;
using ThunderRoad;

namespace PyroAPI
{
    public class WeaponUtils
    {
        public static Item Imbue(Item item, string imbue)
        {
            item.imbues.ForEach(iImbue => iImbue.Transfer(Catalog.GetData<SpellCastCharge>(imbue), iImbue.maxEnergy));
            return item;
        }

        public static Item UnImbue(Item item)
        {
            item.imbues.ForEach(imbue => imbue.Transfer(Catalog.GetData<SpellCastCharge>("Fire"), 0.0f));
            return item;
        }
        
        public static bool IsImbued(Item item)
        {
            return item.imbues.Where(imbue => imbue.energy > 0).ToList().Count > 0;
        }
        
        public static bool PlayerControlled(Item item)
        {
            return (bool) item.mainHandler?.creature?.isPlayer;
        }
    }
}