using GachaSystem.Base;

namespace GachaSystem.Genshin
{
    public class Gacha : GachaBase
    {
        /// <summary>
        /// This Gacha System based on this article:
        /// https://zhuanlan.zhihu.com/p/522246996
        /// </summary>

        public Gacha(uint gachaTime = 90, uint gachaRaiseTime = 73)
        {
            base._GACHA_TARGET_TOP_ROLES_1 = new string[] { "娜维娅" };
            base._GACHA_TARGET_TOP_ROLES_2 = new string[] { "神里绫华" };
            base._GACHA_TARGET_LOW_ROLES = new string[] { "砂糖", "罗莎莉亚", "坎蒂丝" };
            base._GACHA_COMMON_TOP_ROLES = new string[] { "七七", "刻晴", "提纳里", "琴", "迪卢克", "莫娜", "迪希雅" };
            base._GACHA_COMMON_LOW_ROLES = new string[] { "砂糖", "芭芭拉", "菲谢尔", "班尼特", "雷泽", "凯亚", "安柏", "诺艾尔", "丽莎", "行秋", "北斗", "重云", "香菱", "凝光", "绮良良", "莱依拉", "米卡", "卡维", "久岐忍", "瑶瑶", "夏洛蒂", "菲米尼", "琳妮特", "珐露珊", "坎蒂丝", "多莉", "柯莱", "云堇", "鹿野院平藏", "九条裟罗", "五郎", "早柚", "托马", "烟绯", "罗莎莉亚", "辛焱", "迪奥娜" };
            base._GACHA_WASTE = new string[] { "黎明神剑", "黑樱枪", "飞天大御剑", "讨龙英杰谭", "反曲弓", "弹弓", "吃虎鱼刀", "冷刃", "沐浴龙血的剑", "异世界行纪", "神射手之誓", "铁影阔剑", "以理服人", "翡玉法球", "魔导绪论" };

            base._GACHA_TOP_RATE = 60;    // 0.6% in 10000
            base._GACHA_RAISE_RATE = 600; // 6% in 10000
            base._GACHA_ALL_RATE = 10000;

            base._FORCE_TARGET = false;

            base._RANDOM = new();

            base._GACHA_POOLS = new Dictionary<int, string[]>();

            base._GACHA_TIME = gachaTime;
            base._GACHA_RAISE_TIME = gachaRaiseTime;
            base._CURRENT_GACHA_TIME = 0;

            _GACHA_POOLS.Add(1, _GACHA_TARGET_TOP_ROLES_1);
            _GACHA_POOLS.Add(2, _GACHA_TARGET_TOP_ROLES_2);
        }

        public override void Clear(bool isForceTarget = false)
        {
            this._GACHA_TOP_RATE = 60;
            this._CURRENT_GACHA_TIME = 0;
            this._FORCE_TARGET = isForceTarget;
        }

        public override string DoGacha(int gachaPool = 1)
        {
            this._RANDOM = new();

            this._CURRENT_GACHA_TIME++;

            var count = _RANDOM.Next((int)_GACHA_ALL_RATE);

            if (_CURRENT_GACHA_TIME > _GACHA_RAISE_TIME && _CURRENT_GACHA_TIME < _GACHA_TIME) // 74~90
            {
                _GACHA_TOP_RATE += _GACHA_RAISE_RATE;
            }

            if(_CURRENT_GACHA_TIME == _GACHA_TIME)
            {
                //Force get up roles
                _GACHA_TOP_RATE = _GACHA_ALL_RATE;
            }

            if (count < _GACHA_TOP_RATE)
            {
                //Get 5-star role
                var secCount = _RANDOM.Next(2);

                if (secCount == 1 || _FORCE_TARGET)
                {
                    this.Clear();
                    //Target
                    foreach (var role in _GACHA_POOLS[gachaPool])
                    {
                        return "★★★★★:" + this.GetGachaTimes() + ":" + role;
                    }
                }
                else
                {
                    //Common
                    this.Clear(isForceTarget: true);

                    var commonCount = _RANDOM.Next(_GACHA_COMMON_TOP_ROLES.Length);

                    return "★★★★★:" + this.GetGachaTimes() + ":" + _GACHA_COMMON_TOP_ROLES[commonCount];
                }
            }

            if (count < 510 || (_CURRENT_GACHA_TIME % 10 == 0 && count >= 510))
            {
                //Get 4-star role
                var secCount = _RANDOM.Next(1);

                if (secCount == 1)
                {
                    //Target
                    var lowCount = _RANDOM.Next(_GACHA_TARGET_LOW_ROLES.Length);

                    return "★★★★:" + this.GetGachaTimes() + ":" + _GACHA_TARGET_LOW_ROLES[lowCount];
                }
                else
                {
                    //Common
                    var commonCount = _RANDOM.Next(_GACHA_COMMON_LOW_ROLES.Length);

                    return "★★★★:" + this.GetGachaTimes() + ":" + _GACHA_COMMON_LOW_ROLES[commonCount];
                }
            }

            //GetJunk
            var shitCount = _RANDOM.Next(_GACHA_WASTE.Length);

            return "★★★:" + this.GetGachaTimes() + ":" + _GACHA_WASTE[shitCount];
        }
    }
}
