using System;

namespace Waifu_Dreams_CityHelperApplication.Utils {
    public static class TRainer101Helper {
        //上面那个获取不到值
        public const string ProcessName = TRainer97Helper.ProcessName;
        private const string ModuleName = TRainer97Helper.ModuleName;
        
        //钱💰
        public const string Money = ModuleName + "+0x06493A40,0x30,0x338,0xA28";
        //人物等级
        public const string Level = ModuleName + "+0x06493A40,0x30,0x338,0xA38";
        //性饥渴进度
        private const string Sexual_Hunger_Progress = ModuleName + "+0x06493A40,0x30,0x338,0xA60";
        private const string Sexual_Hunger_Max      = ModuleName + "+0x06493A40,0x30,0x338,0xA68";
        //魅力值(Max=99999)
        public const string Charm_Value             = ModuleName + "+0x06493A40,0x30,0x338,0xA98";

        private const string YAxis  = ModuleName + "+0x060E2910,0x0,0x198,0x260";
        private const string XAxis  = ModuleName + "+0x060E2910,0x0,0x198,0x268";
        private const string ZAxis  = ModuleName + "+0x060E2910,0x0,0x198,0x270";


        public const string StrAbout = TRainer97Helper.StrAbout;


        public static void MoneyAdd(int value) {
            TRainer97Helper.MoneyAdd(value, Money);
        }

        public static void LevelAdd(int value) {
            TRainer97Helper.LevelAdd(value, Level);
        }

        /// <summary>
        /// 添加魅力值, Max = 99_999
        /// </summary>
        /// <param name="value"></param>
        public static void CharmAdd(int value) {
            TRainer97Helper.CharmAdd(value, Charm_Value);
        }


        public static void ZAxisEdit(bool isUp, int value) {
            TRainer97Helper.ZAxisEdit(isUp, value, ZAxis);
        }

        /// <summary>
        /// 是否冻结性饥渴
        /// </summary>
        /// <param name="isFreeze"></param>
        public static void FreezeSexual_Hunger(bool isFreeze) {
            TRainer97Helper.FreezeSexual_Hunger(isFreeze, Sexual_Hunger_Progress, Sexual_Hunger_Max);
        }


        //播放click
        public static void PlayClick() {
            TRainer97Helper.PlayClick();
        }

        //播放activate
        public static void PlayActivate(bool isTRainerOpen) {
            TRainer97Helper.PlayActivate(isTRainerOpen);
        }


        /// <summary>
        /// 获取开关图片
        /// </summary>
        /// <param name="isOn">是否打开</param>
        /// <returns></returns>
        public static Uri GetSwitchUri(bool isOn) {
            return TRainer97Helper.GetSwitchUri(isOn);
        }
    }
}