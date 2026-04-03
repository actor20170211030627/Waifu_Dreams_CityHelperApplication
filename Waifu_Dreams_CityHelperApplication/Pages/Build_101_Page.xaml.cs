using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Waifu_Dreams_CityHelperApplication.Utils;

namespace Waifu_Dreams_CityHelperApplication.Pages {
    public partial class Build_101_Page : Page {
        
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

        public Build_101_Page() {
            InitializeComponent();
            
            this.Loaded += MyPage_Loaded;  // 订阅Loaded事件
            this.Unloaded += MyPage_Unloaded;  // 订阅Unloaded事件
            
            InitializeTimer();
            InitializeMouseKeyHook();

            //钱Money
            MemoryDllUtils.BindToUI<int>(TRainer101Helper.Money, delegate(string s) {
                // Console.WriteLine($"钱Money: {s}");
                // 使用 Dispatcher 切换到 UI 线程
                Dispatcher.Invoke(() => {
                    this.TB_Money.Text = s;
                });
            });
            //人物等级
            MemoryDllUtils.BindToUI<int>(TRainer101Helper.Level, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Level.Text = s;
                });
            });
            //魅力值
            MemoryDllUtils.BindToUI<int>(TRainer101Helper.Charm_Value, delegate(string s) {
                Dispatcher.Invoke(() => {
                    this.TB_Charm.Text = s;
                });
            });
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
                _isProcOpen = MemoryDllUtils.OpenProcess(TRainer101Helper.ProcessName);
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
        }

        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
        }
        

        private void Btn_OnClick(object sender, RoutedEventArgs e) {
            TRainer101Helper.PlayClick();

            if (!(sender is FrameworkElement fe)) return;
            string name = fe.Name;
            
            //激活修改器(TRainer activate)
            if (name == this.Image_TRainer_State.Name) {
                _isTRainerOpen = !_isTRainerOpen;
                Uri uri = TRainer101Helper.GetSwitchUri(_isTRainerOpen);
                this.Image_TRainer_State.Source = new BitmapImage(uri);
                TRainer101Helper.PlayActivate(_isTRainerOpen);
                if (!_isTRainerOpen) {
                    UnfreezeAll();
                }
                return;
            }
            //关于
            if (name == this.Btn_About.Name) {
                MessageBox.Show(TRainer101Helper.StrAbout, "说明(explain):");
                return;
            }

            if (!_isProcOpen) return;
            if (!_isTRainerOpen) return;
            
            //钱💰
            if (name == this.Btn_Money.Name) {
                TRainer101Helper.MoneyAdd(10_000_000);
                return;
            }
            
            //人物等级
            if (name == this.Btn_Level.Name) {
                TRainer101Helper.LevelAdd(2);
                return;
            }
            //魅力值💃
            if (name == this.Btn_Charm.Name) {
                TRainer101Helper.CharmAdd(1000);
                return;
            }
            
            //东
            // if (name == this.Btn_XAxis_Plus.Name) {
            //     if (!(ComboBox_XYZDistance.SelectedValue is int value)) return;
            //     TRainerHelper.GoRightOrLeft(true, value);
            //     return;
            // }

            //冻结性饥渴(Sexual Hunger)
            if (name == this.Image_Freeze_Sexual_Hunger.Name) {
                isFreezeSexual_Hunger = !isFreezeSexual_Hunger;
                Uri uri = TRainer101Helper.GetSwitchUri(isFreezeSexual_Hunger);
                this.Image_Freeze_Sexual_Hunger.Source = new BitmapImage(uri);
                TRainer101Helper.FreezeSexual_Hunger(isFreezeSexual_Hunger);
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
            TRainer101Helper.FreezeSexual_Hunger(false);
            isUnfreezeAll = true;
        }
        
        // 页面卸载时执行 - 这是主要的方法
        private void MyPage_Unloaded(object sender, RoutedEventArgs e) {
            Console.WriteLine("页面卸载 - 在这里清理资源");
            
            //停止定时器
            _dispatcherTimer.Stop();
            // _dispatcherTimer.Tick -= ;
            MemoryDllUtils.CloseProcess();
        }
    }
}