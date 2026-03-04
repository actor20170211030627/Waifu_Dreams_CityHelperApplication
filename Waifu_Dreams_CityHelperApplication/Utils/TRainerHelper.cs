using System;
using System.Threading;
using System.Threading.Tasks;
using Memory;

namespace Waifu_Dreams_CityHelperApplication.Utils {
    public static class TRainerHelper {
        // public const string ProcessName = "Waifu_Dreams_City";
        //上面那个获取不到值
        public const string ProcessName = "Waifu_Dreams_City-Win64-Shipping";
        public const string ModuleName = "Waifu_Dreams_City-Win64-Shipping.exe";
        
        //钱💰
        public const string Money = ModuleName + "+0x060E2910,0x0,0xBB0";
        //人物等级
        public const string Level = ModuleName + "+0x060E2910,0x0,0xBC0";
        //性饥渴进度
        private const string Sexual_Hunger_Progress = ModuleName + "+0x060E2910,0x0,0xBEC";
        //魅力值(Max=99999)
        public const string Charm_Value = ModuleName + "+0x060E2910,0x0,0xC20";


        private const string XAxis  = ModuleName + "+0x00532A28,0x2B8,0x7C4";
        private const string YAxis  = ModuleName + "+0x00532A28,0x2B8,0x7C8";
        public const string ZAxis   = ModuleName + "+0x00532A28,0x2B8,0x7CC";
        
        //人物朝向的角度
        private const string DegreePersonFront  = ModuleName + "+0x00532A28,0x2B8,0x7D0";
        //鼠标左右移动的角度
        private const string DegreeMouseLeftRight = ModuleName + "+0x00532A28,0xA0,0x3AC";
        
        //灯光
        private const string LampLight = ModuleName + "+0x00633CC4,0x34";
        
        //环境亮度
        private const string Brightness_Ground_Green2 = ModuleName + "+0x00633D9C,0x14,0x1C8";
        private const string Brightness_Ground_Purper2 = ModuleName + "+0x00633D9C,0x14,0x1CC";
        private const string Brightness_Ground_Yellow2 = ModuleName + "+0x00633D9C,0x14,0x1D0";
        private const string Brightness_Ground_Green = ModuleName + "+0x00633D9C,0x14,0x1D4";
        private const string Brightness_Ground_Purper = ModuleName + "+0x00633D9C,0x14,0x1D8";
        private const string Brightness_Ground_Yellow = ModuleName + "+0x00633D9C,0x14,0x1DC";


        public const string StrAbout = "游戏操作说明:\n" +
                                       "Q     \t       : 菜单(可用于暂停)\n" +
                                       "WSAD  \t       : 前后左右\n" +
                                       "R     \t       : 切换武器(显示/隐藏)\n" +
                                       "X     \t       : 个人资料\n" +
                                       "Z     \t       : 拍照工具 或 恋爱扫描器\n" +
                                       "Shift + WSAD: 快跑\n" +
                                       "\n" + 
                                       "1.如果你使用了另外的修改器, 可以和其他修改器混用.\n" +
                                       "2.有问题请在加QQ群反馈: 206483634, (我有空的时候会去看看).\n" +
                                       "3.杀毒软件报毒: 请自己添加进白名单.\n" +
                                       "4.作者 actor2015\n" +
                                       "5.版本 20260304 & v1.0.0\n" +
                                       "\n" +
                                       "Game Operation instructions:\n" +
                                       "Q     \t       : Menu(can be used to pause)\n" +
                                       "WSAD  \t       : Top, Down, Left, Right\n" +
                                       "R     \t       : Switch weapons (Show/Hide)\n" +
                                       "X     \t       : Personal setting\n" +
                                       "Z     \t       : Take photo or Love Scanner\n" +
                                       "Shift + WSAD: Run faster\n" +
                                       "\n" +
                                       "1.If you use other trainers, you can use this with others.\n" +
                                       "2.If you have any issues, Pls add QQ group to feedback: 206483634.(I will chat sometimes if i not busy.)\n" +
                                       "3.If the antivirus software reports an error, Pls add this to whitelist.\n" +
                                       "4.Author actor2015\n" +
                                       "5.Version 20260304 & v1.0.0";

        public const string StrBrightness = "亮度修改说明:\n" +
                                            "前提: 游戏在白天/黑夜转换的时候也在修改亮度, 所以:\n" +
                                            "    1.开灯后, 有时候灯会闪动, 这是正常现象.\n" +
                                            "    2.设置亮度后, 人物周围亮度会突然改变, 这是正常现象.\n" +
                                            "所以:\n" +
                                            "    当人物周围亮度突然改变后, 等游戏将亮度修改完, 再自行点击下方修改亮度.\n" +
                                            "另外:\n" +
                                            "    地图7(蘑菇沼泽🍄)无法修改亮度, 因为这个地图里亮度一直在变, 无法插手.\n" +
                                            "\n" +
                                            "Brightness modification instructions:\n" +
                                            "Premise: The game also modifies the brightness when time to switch the day / night, so:\n" +
                                            "    1.After turning on the light, sometimes it flashes. This is a normal phenomenon.\n" +
                                            "    2.After setting the brightness, the brightness around the character will suddenly change. This is a normal phenomenon.\n" +
                                            "So:\n" +
                                            "    When the brightness around the character suddenly changes, wait for the game to modify the brightness, and then click the button below to modify the brightness by yourself.\n" +
                                            "Also:\n" +
                                            "    Map 7(Mushroom Marsh🍄) cannot have its brightness modified, because the brightness in this map is constantly changing.";

        //Map1(Missionary Beach 传教士海滩)→Map2(Firm Wood Forest 阔叶林)
        public static readonly float[] CoordinateMissionaryBeach2FirmWoodForest = { 1066.876f, -347.3445f, 50.521f };


        /// <summary>
        ///                             大晚上亮度   傍晚亮度   大白天亮度
        /// Lamp_Light                  3F800000    00000000    00000000
        /// SkyEdge_Color               FF1A0000    FF803B73    FFD9A6A6    //天上亮度, 最主要参数
        /// SkyEdge_Light               00000000    3F400000    3F800000
        /// Object_Light                00000000    00000000    3F800000    //晚上改的时候, 右侧参数会亮一些
        /// SkyEdge_Inner_Green_Light   00000000    3EE66666    3F266666    //视野范围内远处山区(大晚上改成白天: 远方下阵雨的效果)
        /// SkyEdge_Inner_Blue_Light    00000000    3E6B851F    3F266666
        /// SkyEdge_Inner_Yellow_Light  3DCCCCCD    3F000000    3F59999A
        /// Ground_Green2               3DF5C28F    3F19999A    3F333333    //方圆100米颜色亮度(改了后效果比较好)
        /// Ground_Purper2              3E428F5C    3F0CCCCD    3F333333
        /// Ground_Yellow2              3F266666    3F0CCCCD    3F266666
        /// Ground_Green                3D75C28F    3EE66666    3F19999A    //地面颜色亮度(改了后效果一般)
        /// Ground_Purper               3DE147AE    3E4CCCCD    3F19999A
        /// Ground_Yellow               3E99999A    3E800000    3F000000
        /// </summary>
        public static readonly long[] LampBrightness = { 0x00000000, 0x3F800000 };
        private static readonly long[] Ground_Green2 = { 0x3DF5C28F, 0x3F19999A, 0x3F333333 };
        private static readonly long[] Ground_Purper2 = { 0x3E428F5C, 0x3F0CCCCD, 0x3F333333 };
        private static readonly long[] Ground_Yellow2 = { 0x3F266666, 0x3F0CCCCD, 0x3F266666 };
        private static readonly long[] Ground_Green = { 0x3D75C28F, 0x3EE66666, 0x3F19999A };
        private static readonly long[] Ground_Purper = { 0x3DE147AE, 0x3E4CCCCD, 0x3F19999A };
        private static readonly long[] Ground_Yellow = { 0x3E99999A, 0x3E800000, 0x3F000000 };



        private static readonly System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer();
        //程序集名称: Waifu_Dreams_CityHelperApplication
        private static readonly string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        //是否播放Ang
        public static bool IsPlayAng = true;


        public static void MoneyAdd(int value) {
            int money = MemoryDllUtils.ReadInt(Money);
            //钱💰最大值: 999_999_999
            bool isSuccess = MemoryDllUtils.WriteInt(Money, Math.Min(money + value, 999_999_999));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"钱+{value}失败!");
            }
        }

        public static void LevelAdd(int value) {
            int level = MemoryDllUtils.ReadInt(Level);
            //最大等级: 200
            bool isSuccess = MemoryDllUtils.WriteInt(Level, Math.Min(level + value, 200));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"等级+{value}失败!");
            }
        }

        public static void CharmAdd(int value) {
            int level = MemoryDllUtils.ReadInt(Charm_Value);
            //最大魅力值: 99_999
            bool isSuccess = MemoryDllUtils.WriteInt(Charm_Value, Math.Min(level + value, 99_999));
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine($"魅力值+{value}失败!");
            }
        }


        public static void XAxisEdit(bool isEast, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(XAxis, isEast ? x + value : x - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isEast ? "向东+5失败!" : "向西+5失败!");
            }
        }

        public static void YAxisEdit(bool isNorth, int value) {
            float y = MemoryDllUtils.ReadFloat(YAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(YAxis, isNorth ? y + value : y - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isNorth ? "向北+5失败!" : "向南+5失败!");
            }
        }

        public static void ZAxisEdit(bool isUp, int value) {
            float z = MemoryDllUtils.ReadFloat(ZAxis);
            bool isSuccess = MemoryDllUtils.WriteFloat(ZAxis, isUp ? z + value : z - value);
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine(isUp ? "高度+5失败!" : "高度-5失败!");
            }
        }

        /// <summary>
        /// 是否冻结钱💰
        /// </summary>
        /// <param name="isFreeze"></param>
        public static void FreezeMoney(bool isFreeze) {
            if (isFreeze) {
                int value = MemoryDllUtils.ReadInt(Money);
                bool isSuccess = MemoryDllUtils.FreezeValue(Money, "int", value);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结钱💰 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Money);
            }
        }

        /// <summary>
        /// 是否冻魅力值💃
        /// </summary>
        /// <param name="isFreeze"></param>
        public static void FreezeCharm(bool isFreeze) {
            if (isFreeze) {
                int value = MemoryDllUtils.ReadInt(Charm_Value);
                bool isSuccess = MemoryDllUtils.FreezeValue(Charm_Value, "int", value);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结魅力值💃 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Charm_Value);
            }
        }

        /// <summary>
        /// 是否冻结性饥渴
        /// </summary>
        /// <param name="isFreeze"></param>
        public static void FreezeSexual_Hunger(bool isFreeze) {
            if (isFreeze) {
                float value = MemoryDllUtils.ReadFloat(Sexual_Hunger_Progress);
                bool isSuccess = MemoryDllUtils.FreezeValue(Sexual_Hunger_Progress, "float", value);
                if (isSuccess) {
                    PlayAng();
                } else {
                    Console.WriteLine("冻结性饥渴 失败!");
                }
            } else {
                MemoryDllUtils.UnfreezeValue(Sexual_Hunger_Progress);
            }
        }


        /// <summary>
        /// 人物向左平移 or 向右平移
        /// </summary>
        /// <param name="isRight"></param>
        /// <param name="value"></param>
        public static void GoRightOrLeft(bool isRight, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            float y = MemoryDllUtils.ReadFloat(YAxis);
            // double degreePersonFront = GetDegreePersonFront();
            float degreeMouseLeftRight = GetDegreeMouseLeftRight();
            bool isSetDegreePersonFrontSuccess = SetDegreePersonFront(degreeMouseLeftRight);
            x = isRight ? x + (float)Math.Cos(degreeMouseLeftRight) * value : x - (float)Math.Cos(degreeMouseLeftRight) * value;
            y = isRight ? y - (float)Math.Sin(degreeMouseLeftRight) * value : y + (float)Math.Sin(degreeMouseLeftRight) * value;
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, x);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, y);
            if (isSuccessX && isSuccessY) {
                PlayAng();
            } else {
                Console.WriteLine(isRight ? "人物向右平移失败!" : "人物向左平移失败!");
            }
        }

        /// <summary>
        /// 人物向前平移 or 往后退
        /// </summary>
        /// <param name="isFront"></param>
        /// <param name="value"></param>
        public static void GoFrontOrBack(bool isFront, int value) {
            float x = MemoryDllUtils.ReadFloat(XAxis);
            float y = MemoryDllUtils.ReadFloat(YAxis);
            // float degreePersonFront = GetDegreePersonFront();
            float degreeMouseLeftRight = GetDegreeMouseLeftRight();
            bool isSetDegreePersonFrontSuccess = SetDegreePersonFront(degreeMouseLeftRight);
            x = isFront ? x + (float)Math.Sin(degreeMouseLeftRight) * value : x - (float)Math.Sin(degreeMouseLeftRight) * value;
            y = isFront ? y + (float)Math.Cos(degreeMouseLeftRight) * value : y - (float)Math.Cos(degreeMouseLeftRight) * value;
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, x);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, y);
            if (isSuccessX && isSuccessY) {
                PlayAng();
            } else {
                Console.WriteLine(isFront ? "人物向前平移失败!" : "人物向后平移失败!");
            }
        }

        /// <summary>
        /// 灯光设置
        /// </summary>
        /// <param name="isOpen">是否打开</param>
        /// <param name="isFreeze">是否冻结值</param>
        public static void LampLightSet(bool isOpen, bool isFreeze, bool isPlayAng) {
            bool isSuccess;
            if (isFreeze) {
                //一直闪, 应该是修改间隔25ms还是太久了
                isSuccess = MemoryDllUtils.FreezeValue(LampLight, "long", LampBrightness[isOpen ? 1 : 0]);
            } else {
                MemoryDllUtils.UnfreezeValue(LampLight);
                isSuccess = MemoryDllUtils.WriteLong(LampLight, LampBrightness[isOpen ? 1 : 0]);
            }
            if (isSuccess) {
                if (isPlayAng) PlayAng();
            } else {
                Console.WriteLine("灯光亮度设置 失败!");
            }
        }


        /// <summary>
        /// 环境亮度设置
        /// </summary>
        /// <param name="position">第几个亮度[0~2]</param>
        /// <param name="isFreeze">是否冻结值</param>
        public static void BrightnessSet(int position, bool isFreeze, bool isPlayAng) {
            bool isSuccess;
            if (isFreeze) {
                //一直闪, 应该是修改间隔25ms还是太久了
                bool isSuccess0 = MemoryDllUtils.FreezeValue(Brightness_Ground_Green2, "long", Ground_Green2[position]);
                bool isSuccess1 = MemoryDllUtils.FreezeValue(Brightness_Ground_Purper2, "long", Ground_Purper2[position]);
                bool isSuccess2 = MemoryDllUtils.FreezeValue(Brightness_Ground_Yellow2, "long", Ground_Yellow2[position]);
                bool isSuccess3 = MemoryDllUtils.FreezeValue(Brightness_Ground_Green, "long", Ground_Green[position]);
                bool isSuccess4 = MemoryDllUtils.FreezeValue(Brightness_Ground_Purper, "long", Ground_Purper[position]);
                bool isSuccess5 = MemoryDllUtils.FreezeValue(Brightness_Ground_Yellow, "long", Ground_Yellow[position]);
                isSuccess = isSuccess0 && isSuccess1&& isSuccess2 && isSuccess3 && isSuccess4 && isSuccess5;
            } else {
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Green2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Purper2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Yellow2);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Green);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Purper);
                MemoryDllUtils.UnfreezeValue(Brightness_Ground_Yellow);
                bool isSuccess0 = MemoryDllUtils.WriteLong(Brightness_Ground_Green2, Ground_Green2[position]);
                bool isSuccess1 = MemoryDllUtils.WriteLong(Brightness_Ground_Purper2, Ground_Purper2[position]);
                bool isSuccess2 = MemoryDllUtils.WriteLong(Brightness_Ground_Yellow2, Ground_Yellow2[position]);
                bool isSuccess3 = MemoryDllUtils.WriteLong(Brightness_Ground_Green, Ground_Green[position]);
                bool isSuccess4 = MemoryDllUtils.WriteLong(Brightness_Ground_Purper, Ground_Purper[position]);
                bool isSuccess5 = MemoryDllUtils.WriteLong(Brightness_Ground_Yellow, Ground_Yellow[position]);
                isSuccess = isSuccess0 && isSuccess1&& isSuccess2 && isSuccess3 && isSuccess4 && isSuccess5;
            }
            if (isSuccess) {
                if (isPlayAng) PlayAng();
            } else {
                Console.WriteLine("环境亮度设置 失败!");
            }
        }
        public static void BrightnessSet2(int position) {
            bool isSuccess;
            // byte[] lpBuffer = new byte[4];
            // int nSize = 8;
            // lpBuffer = BitConverter.GetBytes(Convert.ToInt64(write));
            byte[] lpBuffer = BitConverter.GetBytes(Ground_Green2[position]);
            UIntPtr code1 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Green2, "");
            if (code1 == UIntPtr.Zero || code1.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code1, lpBuffer, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer2 = BitConverter.GetBytes(Ground_Purper2[position]);
            UIntPtr code2 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Purper2, "");
            if (code2 == UIntPtr.Zero || code2.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code2, lpBuffer2, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer3 = BitConverter.GetBytes(Ground_Yellow2[position]);
            UIntPtr code3 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Yellow2, "");
            if (code3 == UIntPtr.Zero || code3.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code3, lpBuffer3, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer4 = BitConverter.GetBytes(Ground_Green[position]);
            UIntPtr code4 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Green, "");
            if (code4 == UIntPtr.Zero || code4.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code4, lpBuffer4, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer5 = BitConverter.GetBytes(Ground_Purper[position]);
            UIntPtr code5 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Purper, "");
            if (code5 == UIntPtr.Zero || code5.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code5, lpBuffer5, (UIntPtr) 8, IntPtr.Zero);

            byte[] lpBuffer6 = BitConverter.GetBytes(Ground_Yellow[position]);
            UIntPtr code6 = MemoryDllUtils.Memory.GetCode(Brightness_Ground_Yellow, "");
            if (code6 == UIntPtr.Zero || code6.ToUInt64() < 65536UL /*0x010000*/)
                return /*false*/;
            Imps.WriteProcessMemory(MemoryDllUtils.Memory.mProc.Handle, code6, lpBuffer6, (UIntPtr) 8, IntPtr.Zero);

            isSuccess = true;
            
            if (isSuccess) {
                PlayAng();
            } else {
                Console.WriteLine("环境亮度设置 失败!");
            }
        }


        /// <summary>
        /// 瞬移到坐标
        /// </summary>
        /// <param name="coordinate"></param>
        public static void Teleport(float[] coordinate, string failureStr) {
            bool isSuccessX = MemoryDllUtils.WriteFloat(XAxis, coordinate[0]);
            bool isSuccessY = MemoryDllUtils.WriteFloat(YAxis, coordinate[1]);
            bool isSuccessZ = MemoryDllUtils.WriteFloat(ZAxis, coordinate[2]);
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
        /// 获取人物正前方角度 (从N开始, N右侧最小, N左侧最大)
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        /// <returns>返回弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</returns>
        private static float GetDegreePersonFront() {
            return MemoryDllUtils.ReadFloat(DegreePersonFront);
        }
        
        /// <summary>
        /// 设置人物朝向 (从N开始, N右侧最小, N左侧最大)
        /// </summary>
        /// <param name="degree">弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</param>
        /// <returns></returns>
        private static bool SetDegreePersonFront(float degree) {
            return MemoryDllUtils.WriteFloat(DegreePersonFront, degree);
        }

        /// <summary>
        /// 获取鼠标左右旋转角度 (从N开始, N右侧最小, N左侧最大)
        /// </summary>
        /// <returns>返回弧度: (0 ~ 2π), 不是角度: (0° ~ 360°)</returns>
        private static float GetDegreeMouseLeftRight() {
            return MemoryDllUtils.ReadFloat(DegreeMouseLeftRight);
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
        

        /// <summary>
        /// 获取人物正前方角度
        /// minDegree = 1.49568E-05(0.0000149568 ‌), degreeI = 930803445
        /// maxDegree =             6.283183,           degreeI = 1086918614
        /// </summary>
        public static void PrintDegreePersonFront() {
            float minDegree = 100, maxDegree = -100;
            Task.Factory.StartNew((Action)(() => {
                while (true) {
                    if (minDegree == 0) {
                        minDegree = 100;
                        Console.WriteLine($"minDegree to 100!");
                    }

                    float degree = MemoryDllUtils.ReadFloat(DegreePersonFront);
                    int degreeI = MemoryDllUtils.ReadInt(DegreePersonFront);
                    if (degree < minDegree) {
                        minDegree = degree;
                        Console.WriteLine($"minDegree = {minDegree}, degreeI = {degreeI}");
                    } else if (degree > maxDegree) {
                        maxDegree = degree;
                        Console.WriteLine($"maxDegree = {maxDegree}, degreeI = {degreeI}");
                    }

                    Thread.Sleep(10);
                }
            }));
        }
    }
}