﻿using Microsoft.Win32;
using SocketDA.Models;
using System;
using System.Net.Sockets;
using System.Windows.Threading;

namespace SocketDA.ViewModels
{
    class MainWindowViewModel : MainWindowBase
    {
        #region 字段
        private Socket SSocket = null;

        private string DataRecvPath = string.Empty;   /* 数据接收路径 */
        #endregion

        public SocketModel SocketModel { get; set; }
        public SendModel SendModel { get; set; }
        public RecvModel RecvModel { get; set; }
        public TimerModel TimerModel { get; set; }
        public HelpModel HelpModel { get; set; }

        #region 状态栏- 信息描述
        private string _DepictInfo;
        public string DepictInfo
        {
            get { return _DepictInfo; }
            set
            {
                if (_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged(nameof(DepictInfo));
                }
            }
        }
        #endregion

        #region 打开/关闭套接字
        public void OpenSocket()
        {
            try
            {
                if (SocketModel.SocketProtocolSelectedIndex == 0)
                {
                    
                }
                else if (SocketModel.SocketProtocolSelectedIndex == 1)
                {

                }
                else if (SocketModel.SocketProtocolSelectedIndex == 2)
                {

                }
                else if (SocketModel.SocketProtocolSelectedIndex == 3)
                {

                }
            }
            catch
            {

            }
        }
        #endregion

        #region 辅助区
        private bool _HexSend;
        public bool HexSend
        {
            get
            {
                return _HexSend;
            }
            set
            {
                if (_HexSend != value)
                {
                    _HexSend = value;
                    RaisePropertyChanged(nameof(HexSend));

                    if (HexSend == true)
                    {
                        DepictInfo = string.Format(cultureInfo, "请输入十六进制数据用空格隔开，比如A0 B1 C2 D3");
                    }
                    else
                    {
                        DepictInfo = string.Format(cultureInfo, "网络端口调试助手");
                    }
                }
            }
        }

        private bool _AutoSend;
        public bool AutoSend
        {
            get
            {
                return _AutoSend;
            }
            set
            {
                if (_AutoSend != value)
                {
                    _AutoSend = value;
                    RaisePropertyChanged(nameof(AutoSend));
                }

                if (AutoSend == true)
                {
                    if (SendModel.AutoSendNum <= 0)
                    {
                        DepictInfo = string.Format(cultureInfo, "请输入正确的发送时间间隔");
                        return;
                    }

                    StartAutoSendTimer(SendModel.AutoSendNum);
                }
                else
                {
                    StopAutoSendTimer();
                }
            }
        }

        private bool _SaveRecv;
        public bool SaveRecv
        {
            get
            {
                return _SaveRecv;
            }
            set
            {
                if (_SaveRecv != value)
                {
                    _SaveRecv = value;
                    RaisePropertyChanged(nameof(SaveRecv));
                }

                if (SaveRecv)
                {
                    DepictInfo = "接收数据默认保存在程序基目录，可以点击路径选择操作更换";
                }
                else
                {
                    DepictInfo = "网络端口调试助手";
                }
            }
        }
        #endregion

        #region 自动发送定时器实现
        private readonly DispatcherTimer AutoSendDispatcherTimer = new DispatcherTimer();

        private void InitAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Tick += AutoSendDispatcherTimer_Tick;
        }

        private void AutoSendDispatcherTimer_Tick(object sender, EventArgs e)
        {
            Send();
        }

        private void StartAutoSendTimer(int interval)
        {
            AutoSendDispatcherTimer.IsEnabled = true;
            AutoSendDispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
            AutoSendDispatcherTimer.Start();
        }

        private void StopAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Stop();
        }
        #endregion

        #region 发送
        public void Send()
        {

        }
        #endregion

        #region 发送文件
        public void SendFile()
        {

        }
        #endregion

        #region 路径选择
        public void SaveRecvPath()
        {
            SaveFileDialog ReceDataSaveFileDialog = new SaveFileDialog
            {
                Title = string.Format(cultureInfo, "接收数据保存"),
                FileName = string.Format(cultureInfo, "{0}", DateTime.Now.ToString("yyyyMMdd", cultureInfo)),
                DefaultExt = ".txt",
                Filter = string.Format(cultureInfo, "文本文件|*.txt")
            };

            if (ReceDataSaveFileDialog.ShowDialog() == true)
            {
                DataRecvPath = ReceDataSaveFileDialog.FileName;
            }
        }
        #endregion

        #region 清接收区
        public void ClarReceData()
        {

        }
        #endregion

        #region 清发送区
        public void ClearSendData()
        {
            SendModel.SendData = string.Empty;
        }
        #endregion

        #region 清空计数
        public void ClearCount()
        {

        }
        #endregion

        #region Combobox Support
        public void ProtocolComboBox_SelectionChanged()
        {
            if(SocketModel.SocketProtocolSelectedIndex == 0)
            {
                SocketModel.DestinationVisibility = "Collapsed";
                SocketModel.OpenCloseSocket = string.Format(cultureInfo, "TCP 侦听");
            }
            else if (SocketModel.SocketProtocolSelectedIndex == 1)
            {
                SocketModel.DestinationVisibility = "Visible";
                SocketModel.OpenCloseSocket = string.Format(cultureInfo, "TCP 连接");
            }
            else if (SocketModel.SocketProtocolSelectedIndex == 2)
            {
                SocketModel.DestinationVisibility = "Collapsed";
                SocketModel.OpenCloseSocket = string.Format(cultureInfo, "UDP 侦听");
            }
            else if (SocketModel.SocketProtocolSelectedIndex == 3)
            {
                SocketModel.DestinationVisibility = "Visible";
                SocketModel.OpenCloseSocket = string.Format(cultureInfo, "UDP 连接");
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            SocketModel = new SocketModel();
            SocketModel.SocketDataContext();

            SendModel = new SendModel();
            SendModel.SendDataContext();

            RecvModel = new RecvModel();
            RecvModel.RecvDataContext();

            TimerModel = new TimerModel();
            TimerModel.TimerDataContext();

            HelpModel = new HelpModel();
            HelpModel.HelpDataContext();

            SaveRecv = false;
            HexSend = false;
            AutoSend = false;
            InitAutoSendTimer();

            DepictInfo = string.Format(cultureInfo, "网络端口调试助手");
        }
    }
}
