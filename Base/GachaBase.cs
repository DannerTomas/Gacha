namespace GachaSystem.Base
{
    public class GachaBase
    {
        //The wish system gacha pools
        public Dictionary<int, string[]> _GACHA_POOLS;

        //Up 5-star roles and 4-star roles
        public string[] _GACHA_TARGET_TOP_ROLES_1;
        public string[] _GACHA_TARGET_TOP_ROLES_2;
        public string[] _GACHA_TARGET_LOW_ROLES;

        //Common 5-star roles and 4-star roles
        public string[] _GACHA_COMMON_TOP_ROLES;
        public string[] _GACHA_COMMON_LOW_ROLES;

        //Common 3-star wastes
        public string[] _GACHA_WASTE;

        //Some values of gacha times.
        public uint _GACHA_TIME = 90;
        public uint _GACHA_RAISE_TIME = 73;

        public uint _CURRENT_GACHA_TIME = 0;

        //Some values of gacha rates.
        public uint _GACHA_TOP_RATE = 60;    // 0.6% in 10000
        public uint _GACHA_RAISE_RATE = 600; // 6% in 10000
        public uint _GACHA_ALL_RATE = 10000;

        //If next time will we get Up roles.
        public bool _FORCE_TARGET = false;

        public Random _RANDOM = new();

        public virtual void Clear(bool isForceTarget = false)
        {

        }

        public virtual string DoGacha(int gachaPool = 1)
        {
            return string.Empty;
        }

        public virtual string[] DoGacha10Times()
        {
            var result = new string[10];

            for (int count = 0; count < 10; count++)
            {
                result[count] = DoGacha();
            }

            return result;
        }

        public virtual uint GetGachaTimes()
        {
            return _CURRENT_GACHA_TIME;
        }

        public virtual string GetGachaPoolRoles(int pool)
        {
            var result = string.Empty;
            if (pool == 1)
            {
                foreach (var role in _GACHA_TARGET_TOP_ROLES_1)
                {
                    result += role + " ";
                }

            }
            else if (pool == 2)
            {
                foreach (var role in _GACHA_TARGET_TOP_ROLES_2)
                {
                    result += role + " ";
                }
            }

            return result;
        }
    }
}
