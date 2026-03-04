using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Gma.System.MouseKeyHook;
using Waifu_Dreams_CityHelperApplication.Utils;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace Waifu_Dreams_CityHelperApplication.Pages {
    
    public partial class Build_97_Page : Page {
        
        //进程是否打开
        private bool _isProcOpen = false;
        //修改器是否激活
        private bool _isTRainerOpen = true;
        //冻结钱💰
        private bool isFreezeMoney = false;
        //冻结魅力值
        private bool isFreezeCharm = false;
        //是否冻结性饥渴
        private bool isFreezeSexual_Hunger = false;
        //是否取消冻结所有
        private bool isUnfreezeAll = true;


        private DispatcherTimer _dispatcherTimer;

        private IKeyboardMouseEvents m_GlobalHook;

        public Build_97_Page() {
            InitializeComponent();
            
            this.Loaded += MyPage_Loaded;  // 订阅Loaded事件
            this.Unloaded += MyPage_Unloaded;  // 订阅Unloaded事件
            
            InitializeTimer();
            InitializeMouseKeyHook();

            //钱Money
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //人物等级
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Level, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Level.Text = s;
                });
            });
            //魅力值
            MemoryDllUtils.BindToUI<int>(TRainerHelper.Charm_Value, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Charm.Text = s;
                });
            });
            
            
            /*
             * 监听按键: 只能在窗口获取焦点时才管用
             */
            if (false) {
                this.KeyDown += delegate(object sender, KeyEventArgs e) {
                    // 判断是否同时按下了Ctrl键和S键
                    if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control) {
                        // 执行你的保存逻辑
                        MessageBox.Show("保存命令被触发！");
            
                        // 标记事件为已处理，防止继续传递
                        e.Handled = true;
                    }
                };
            }
        }
        
        private void MyPage_Loaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面加载完成 - 在这里初始化数据");
            // 加载数据、绑定事件等
        }

        private void InitializeTimer() {
            // 创建定时器，间隔为 1 秒
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300.0);
            _dispatcherTimer.Tick += delegate(object sender, EventArgs args) {
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainerHelper.ProcessName);
                if (_isProcOpen) {
                    if (!_isTRainerOpen) return;

                    //z轴 ZAxis
                    // float zAxis = MemoryDllUtils.ReadFloat(TRainerHelper.ZAxis);
                    // this.TB_ZAxis.Text = ((int) zAxis).ToString();
                } else {
                    Console.WriteLine($"openProcessSuccess: {_isProcOpen}");
                    UnfreezeAll();
                }

                this.Border_Running.Visibility = _isProcOpen ? Visibility.Visible : Visibility.Collapsed;
                this.Border_Stopped.Visibility = _isProcOpen ? Visibility.Collapsed : Visibility.Visible;
            };
            _dispatcherTimer.Start();
        }

        /// <summary>
        /// 初始化键盘鼠标hook
        /// </summary>
        private void InitializeMouseKeyHook() {
            // // Note: for the application hook, use the Hook.AppEvents() instead
            // m_GlobalHook = Hook.GlobalEvents();
            // m_GlobalHook.KeyUp += GlobalHookKeyUp;
            //
            //
            // //1. Define key combinations
            // //                                                               +数字无效???
            // // Combination combinationMoney = Combination.FromString("Control+0");
            // // Combination combinationBeer = Combination.FromString("Control+1");
            // // Key.LeftCtrl;
            // // Key.D0;
            // Combination combinationMoney = Combination.TriggeredBy(Keys.D0).With(Keys.Control);
            // // Combination combinationMoney = Combination.TriggeredBy(Keys.NumPad0).With(Keys.Control);
            // Combination combinationBeer = Combination.TriggeredBy(Keys.D1).Control();
            // Combination combinationHeightMinus = Combination.TriggeredBy(Keys.L).Control();
            //
            // //2. Define actions
            // Action actionMoney = () => {
            //     if (!_isProcOpen) return;
            //     if (!_isTRainerOpen) return;
            //     TRainerHelper.MoneyAdd();
            // };
            // Action actionBeer = () => {
            //     if (!_isProcOpen) return;
            //     if (!_isTRainerOpen) return;
            //     TRainerHelper.LevelAdd();
            // };
            // Action actionHeightMinus = () => {
            //     if (!_isProcOpen) return;
            //     if (!_isTRainerOpen) return;
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.ZAxisEdit(false, value);
            // };
            //
            // //3. Assign actions to key combinations
            // var assignment = new Dictionary<Combination, Action> {
            //     {combinationMoney, actionMoney},
            //     {combinationBeer, actionBeer},
            //     {combinationHeightMinus, actionHeightMinus}
            // };
            //
            // //4. Install listener
            // // Hook.GlobalEvents().OnCombination(assignment);
            // m_GlobalHook.OnCombination(assignment);
        }

        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
            // // KeyCode = Left, KeyData = Left, KeyValue = 37, SuppressKeyPress = False
            // // KeyCode = Up, KeyData = Up, KeyValue = 38, SuppressKeyPress = False
            // // KeyCode = Right, KeyData = Right, KeyValue = 39, SuppressKeyPress = False
            // // KeyCode = Down, KeyData = Down, KeyValue = 40, SuppressKeyPress = False
            // // Console.WriteLine($"KeyCode = {e.KeyCode}, KeyData = {e.KeyData}, KeyValue = {e.KeyValue}, SuppressKeyPress = {e.SuppressKeyPress}");
            //
            // if (!_isProcOpen) return;
            // if (!_isTRainerOpen) return;
            //
            // if (e.KeyCode == Keys.Right) {  //东
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoRightOrLeft(true, value);
            //     return;
            // }
            // if (e.KeyCode == Keys.Left) {   //西
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoRightOrLeft(false, value);
            //     return;
            // }
            // if (e.KeyCode == Keys.Up) {     //北
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoFrontOrBack(true, value);
            //     return;
            // }
            // if (e.KeyCode == Keys.Down) {   //南
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoFrontOrBack(false, value);
            //     return;
            // }
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            
            TRainerHelper.PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //激活修改器(TRainer activate)
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = TRainerHelper.GetSwitchUri(_isTRainerOpen);
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                TRainerHelper.PlayActivate(_isTRainerOpen);
                if (!_isTRainerOpen) {
                    UnfreezeAll();
                }
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(TRainerHelper.StrAbout, "说明(explain):");
                return;
            }
            //问号❓️❔️
            // if (name == this.Image_Question_Mark.Name) {
            //     MessageBox.Show(TRainerHelper.StrBrightness, "亮度修改说明(Brightness explain):");
            //     return;
            // }

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                TRainerHelper.MoneyAdd(10_000_000);
                return;
            }
            
            //人物等级
            if (name == this.Btn_Level.Name) {
                TRainerHelper.LevelAdd(2);
                return;
            }
            //魅力值💃
            if (name == this.Btn_Charm.Name) {
                TRainerHelper.CharmAdd(1000);
                return;
            }
            
            //东
            // if (name == this.Btn_XAxis_Plus.Name) {
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoRightOrLeft(true, value);
            //     return;
            // }

            //冻结钱💰
            if (name == this.Image_Freeze_Money.Name) {
                isFreezeMoney = !isFreezeMoney;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeMoney);
                this.Image_Freeze_Money.Source = new BitmapImage(uri);
                TRainerHelper.FreezeMoney(isFreezeMoney);
                if (isFreezeMoney) {
                    isUnfreezeAll = false;
                }
                return;
            }
            //冻结魅力值
            if (name == this.Image_Freeze_Charm.Name) {
                isFreezeCharm = !isFreezeCharm;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeCharm);
                this.Image_Freeze_Charm.Source = new BitmapImage(uri);
                TRainerHelper.FreezeCharm(isFreezeCharm);
                if (isFreezeCharm) {
                    isUnfreezeAll = false;
                }
                return;
            }
            //冻结性饥渴(Sexual Hunger)
            if (name == this.Image_Freeze_Sexual_Hunger.Name) {
                isFreezeSexual_Hunger = !isFreezeSexual_Hunger;
                Uri uri = TRainerHelper.GetSwitchUri(isFreezeSexual_Hunger);
                this.Image_Freeze_Sexual_Hunger.Source = new BitmapImage(uri);
                TRainerHelper.FreezeSexual_Hunger(isFreezeSexual_Hunger);
                if (isFreezeSexual_Hunger) {
                    isUnfreezeAll = false;
                }
                return;
            }
        }


        /// <summary>
        /// 取消所有冻结
        /// </summary>
        private void UnfreezeAll() {
            if (isUnfreezeAll) return;
            TRainerHelper.FreezeMoney(false);
            TRainerHelper.FreezeCharm(false);
            TRainerHelper.FreezeSexual_Hunger(false);
            isUnfreezeAll = true;
        }
        
        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            
            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
            //It is recommened to dispose it
            m_GlobalHook.Dispose();
            
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
            MemoryDllUtils.CloseProcess();
        }
    }
}