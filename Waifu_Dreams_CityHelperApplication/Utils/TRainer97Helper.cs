using System;

namespace Waifu_Dreams_CityHelperApplication.Utils {
    public static class TRainer97Helper {
        // public const string ProcessName = "Waifu_Dreams_City";
        //上面那个获取不到值
        public const string ProcessName = "Waifu_Dreams_City-Win64-Shipping";
        public const string ModuleName = "Waifu_Dreams_City-Win64-Shipping.exe";
        
        //钱💰
        public const string Money = ModuleName + "+0x060E2910,0x0,0xBB0";
        //人物等级
        public const string Level = ModuleName + "+0x060E2910,0x0,0xBC0";
        //性饥渴进度
        private const string Sexual_Hunger_Progress = ModuleName + "+0x060E2910,0x0,0xBE8";
        private const string Sexual_Hunger_Max      = ModuleName + "+0x060E2910,0x0,0xBF0";
        //魅力值(Max=99999)
        public const string Charm_Value             = ModuleName + "+0x060E2910,0x0,0xC20";

        private const string YAxis  = ModuleName + "+0x06345BE0,0x16ED0,0x210,0x0,0x260";
        private const string XAxis  = ModuleName + "+0x06345BE0,0x16ED0,0x210,0x0,0x268";
        private const string ZAxis  = ModuleName + "+0x06345BE0,0x16ED0,0x210,0x0,0x270";


        public const string StrAbout = "游戏操作说明:\n" +
                                       "Esc/Q \t       : 菜单(可用于暂停)\n" +
                                       "C     \t       : 布料\n" +
                                       "E     \t       : 确认, 退出, 手淫保持\n" +
                                       "H     \t       : 帮助\n" +
                                       "R     \t       : 切换武器(显示/隐藏)\n" +
                                       "X     \t       : 个人资料\n" +
                                       "Y     \t       : 第1/3人称\n" +
                                       "Z     \t       : 工具(拍照工具 或 恋爱扫描器)\n" +
                                       "WSAD  \t       : 前后左右\n" +
                                       "Shift + WSAD: 快跑\n" +
                                       "\n" + 
                                       "1.如果你使用了另外的修改器, 可以和其他修改器混用.\n" +
                                       "2.有问题请在加QQ群反馈: 206483634, (我有空的时候会去看看).\n" +
                                       "3.杀毒软件报毒: 请自己添加进白名单.\n" +
                                       "4.作者 actor2015\n" +
                                       "5.版本 20260403 & v1.1.0\n" +
                                       "\n" +
                                       "Game Operation instructions:\n" +
                                       "Esc/Q \t       : Menu(can be used to pause)\n" +
                                       "C     \t       : Cloth\n" +
                                       "E     \t       : Action\n" +
                                       "H     \t       : Help\n" +
                                       "R     \t       : Toggle gun\n" +
                                       "X     \t       : Player\n" +
                                       "Y     \t       : FP\n" +
                                       "Z     \t       : Tool(Take photo or Love Scanner)\n" +
                                       "WSAD  \t       : Top, Down, Left, Right\n" +
                                       "Shift + WSAD: Run faster\n" +
                                       "\n" +
                                       "1.If you use other trainers, you can use this with others.\n" +
                                       "2.If you have any issues, Pls add QQ group to feedback: 206483634.(I will chat sometimes if i not busy.)\n" +
                                       "3.If the antivirus software reports an error, Pls add this to whitelist.\n" +
                                       "4.Author actor2015\n" +
                                       "5.Version 20260403 & v1.1.0";


        private static readonly System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer();
        //程序集名称: Waifu_Dreams_CityHelperApplication
        private static readonly string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        //是否播放Ang
        public static bool IsPlayAng = true;


        public static void MoneyAdd(int value, string code = Money) {
            int money = MemoryDllUtils.ReadInt(code);
            //钱💰最大值: 999_999_999
            bool isSuccess = MemoryDllUtils.WriteInt(code, Math.Min(money + value, 999_999_999));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"钱+{value}失败!");
            }
        }

        public static void LevelAdd(int value, string code = Level) {
            int level = MemoryDllUtils.ReadInt(code);
            //最大等级: 200
            bool isSuccess = MemoryDllUtils.WriteInt(code, Math.Min(level + value, 200));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"等级+{value}失败!");
            }
        }

        /// <summary>
        /// 添加魅力值, Max = 99_999
        /// </summary>
        /// <param name="value"></param>
        public static void CharmAdd(int value, string code = Charm_Value) {
            int level = MemoryDllUtils.ReadInt(code);
            bool isSuccess = MemoryDllUtils.WriteInt(code, Math.Min(level + value, 99_999));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"魅力值+{value}失败!");
            }
        }


        public static void ZAxisEdit(bool isUp, int value, string code = ZAxis) {
            float z = MemoryDllUtils.ReadFloat(code);
            bool isSuccess = MemoryDllUtils.WriteFloat(code, isUp ? z + value : z - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isUp ? "高度+5失败!" : "高度-5失败!");
            }
        }

        /// <summary>
        /// 是否冻结性饥渴
        /// </summary>
        /// <param name="isFreeze"></param>
        public static void FreezeSexual_Hunger(bool isFreeze, string code = Sexual_Hunger_Progress, string codeMax = Sexual_Hunger_Max) {
            if (isFreeze) {
                //随等级和加点增加而增加?
                double sexualHungerMax = MemoryDllUtils.ReadDouble(codeMax);
                if (sexualHungerMax <= 0.0) sexualHungerMax = 99.0;
                bool isSuccess = MemoryDllUtils.FreezeValue(code, "double", sexualHungerMax);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结性饥渴 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(code);
            }
        }

        /// <summary>
        /// 瞬移到坐标
        /// </summary>
        /// <param name="coordinate"></param>
        public static void Teleport(float[] coordinate, string failureStr,
            string codeX = XAxis, string codeY = YAxis, string codeZ = ZAxis
            ) {
            bool isSuccessX = MemoryDllUtils.WriteFloat(codeX, coordinate[0]);
            bool isSuccessY = MemoryDllUtils.WriteFloat(codeY, coordinate[1]);
            bool isSuccessZ = MemoryDllUtils.WriteFloat(codeZ, coordinate[2]);
            if (isSuccessX && isSuccessY && isSuccessZ) {
                PlayAng();
            } else {
                Console.WriteLine(failureStr);
            }
        }


        //播放ang
        public static void PlayAng() {
            // if (!IsPlayAng) return;
            // Uri uri = new Uri("Resources/Medias/ang.wav", UriKind.Relative);
            // SoundPlayerUtils.Stream(SoundPlayer, uri);
            // SoundPlayerUtils.Play(SoundPlayer);
        }

        //播放click
        public static void PlayClick() {
            Uri uri = new Uri($"Resources/Medias/click.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(SoundPlayer, uri);
            SoundPlayerUtils.Play(SoundPlayer);
        }

        //播放activate
        public static void PlayActivate(bool isTRainerOpen) {
            Uri uri = isTRainerOpen ? new Uri($"Resources/Medias/activate.wav", UriKind.Relative)
                :  new Uri($"Resources/Medias/deactivate.wav", UriKind.Relative);
            SoundPlayerUtils.Stream(SoundPlayer, uri);
            SoundPlayerUtils.Play(SoundPlayer);
        }


        /// <summary>
        /// 获取开关图片
        /// </summary>
        /// <param name="isOn">是否打开</param>
        /// <returns></returns>
        public static Uri GetSwitchUri(bool isOn) {
            if (isOn) {
                return new Uri($"pack://application:,,,/{AssemblyName};component/Resources/Images/icon_switch_green2.png");
            }
            return new Uri($"pack://application:,,,/{AssemblyName};component/Resources/Images/icon_switch_lightyellow.png");
        }
    }
}