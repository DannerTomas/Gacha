using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GachaSystem.Genshin
{
    public class Gacha
    {
        /// <summary>
        /// This Gacha System based on this article:
        /// https://zhuanlan.zhihu.com/p/522246996
        /// </summary>
        /// 
        private Dictionary<int, string[]> _GACHA_POLLS = new Dictionary<int, string[]>();

        private string[] _GACHA_TARGET_TOP_ROLES_1 = { "娜维娅" };
        private string[] _GACHA_TARGET_TOP_ROLES_2 = { "神里绫华" };
        private string[] _GACHA_TARGET_LOW_ROLES = { "砂糖", "罗莎莉亚", "坎蒂丝" };

        private string[] _GACHA_COMMON_TOP_ROLES = { "七七", "刻晴", "提纳里", "琴", "迪卢克", "莫娜", "迪希雅" };
        private string[] _GACHA_COMMON_LOW_ROLES = { "砂糖", "芭芭拉", "菲谢尔", "班尼特", "雷泽", "凯亚", "安柏", "诺艾尔", "丽莎", "行秋", "北斗", "重云", "香菱", "凝光", "绮良良", "莱依拉", "米卡", "卡维", "久岐忍", "瑶瑶", "夏洛蒂", "菲米尼", "琳妮特", "珐露珊", "坎蒂丝", "多莉", "柯莱", "云堇", "鹿野院平藏", "九条裟罗", "五郎", "早柚", "托马", "烟绯", "罗莎莉亚", "辛焱", "迪奥娜" };

        private string[] _GACHA_WASTE = { "黎明神剑" , "黑樱枪", "飞天大御剑", "讨龙英杰谭", "反曲弓", "弹弓", "吃虎鱼刀", "冷刃", "沐浴龙血的剑", "异世界行纪", "神射手之誓", "铁影阔剑", "以理服人", "翡玉法球", "魔导绪论" };

        private uint _GACHA_TIME = 90;
        private uint _GACHA_RAISE_TIME = 73;

        private uint _CURRENT_GACHA_TIME = 0;

        private uint _GACHA_TOP_RATE = 60;// 0.6% in 10000
        private uint _GACHA_RAISE_RATE = 600; // 6% in 10000
        private uint _GACHA_ALL_RATE = 10000;

        private bool _FORCE_TARGET = false;

        private Random _RANDOM = new();

        public Gacha(uint gachaTime = 90, uint gachaRaiseTime = 73)
        {
            this._GACHA_TIME = gachaTime;
            this._GACHA_RAISE_TIME = gachaRaiseTime;
            this._CURRENT_GACHA_TIME = 0;

            _GACHA_POLLS.Add(1, _GACHA_TARGET_TOP_ROLES_1);
            _GACHA_POLLS.Add(2, _GACHA_TARGET_TOP_ROLES_2);
        }

        public void Clear(bool isForceTarget = false)
        {
            this._GACHA_TOP_RATE = 60;
            this._CURRENT_GACHA_TIME = 0;
            this._FORCE_TARGET = isForceTarget;
        }

        public string DoGacha(int gachaPool = 1)
        {
            this._RANDOM = new();

            this._CURRENT_GACHA_TIME++;

            var count = _RANDOM.Next((int)_GACHA_ALL_RATE);

            if(_CURRENT_GACHA_TIME > _GACHA_RAISE_TIME && _CURRENT_GACHA_TIME < _GACHA_TIME) // 74~90
            {
                _GACHA_TOP_RATE += _GACHA_RAISE_RATE;
            }

            if (count < _GACHA_TOP_RATE)
            {
                //Get 5-star role
                var secCount = _RANDOM.Next(2);

                if (secCount == 1 || _FORCE_TARGET)
                {
                    this.Clear();
                    //Target
                    foreach (var role in _GACHA_POLLS[gachaPool])
                    {
                        return "★★★★★:" + this.GetGachaTimes() + ":" + role;
                    }
                }
                else
                {
                    //Common
                    this.Clear(isForceTarget : true);

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

        public string[] DoGacha10Times()
        {
            var result = new string[10];
            
            for(int count = 0; count < 10; count++)
            {
                result[count] = this.DoGacha();
            }

            return result;
        }

        public uint GetGachaTimes()
        {
            return _CURRENT_GACHA_TIME;
        }

        public string GetGachaPoolRoles(int pool)
        {
            var result = string.Empty;
            if(pool == 1)
            {
                foreach (var role in _GACHA_TARGET_TOP_ROLES_1)
                {
                    result += role + " ";
                }

            }
            else if(pool == 2)
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
