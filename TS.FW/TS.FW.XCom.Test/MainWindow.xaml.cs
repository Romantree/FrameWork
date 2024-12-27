using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TS.FW.XCom.SDC;
using TS.FW.XCom.SDC.OLED.V227;

namespace TS.FW.XCom.Test
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private SdcXComManager xcom;

        public MainWindow()
        {
            var list = new List<Test>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Test(i));
            }

            list.Remove(new Test(10));

            InitializeComponent();

            xcom = new SdcXComManager("OLED", "V227");

            xcom.OnStateChangingEvent += Xcom_OnStateChangingEvent;
            xcom.OnStateChangedEvent += Xcom_OnStateChangedEvent;
            xcom.OnRecvMessageEvent += Xcom_OnRecvMessageEvent;
            xcom.OnSendMessageEvent += Xcom_OnSendMessageEvent;

            xcom.Start();
        }

        private void Xcom_OnSendMessageEvent(object sender, IXComModel e)
        {
            
        }

        private void Xcom_OnRecvMessageEvent(object sender, IXComModel e)
        {
            if(e.StreamFunction == "S1F3")
            {
                this.xcom.SendMsg(new S1F4_SelectedEQStateData_Model(), e.SysByte);
            }
            else if(e.StreamFunction == "S1F1")
            {
                this.xcom.SendMsg(new S1F2_OnLineData_EQ_Model()
                {
                    VERSION = "v1.0",
                    SPEC_CODE = "씽크소프트",
                    MODULE_ID = "TEST_MODULE_001",
                    MCMD = 0,
                }, e.SysByte);
            }
            else if(e.StreamFunction == "S1F6")
            {
                MessageBox.Show(e.CreateJson());
            }
            else if(e.StreamFunction == "S2F41")
            {
                MessageBox.Show(e.CreateJson());

                this.xcom.SendMsg(new S2F42_HostCommandAck_EQCmd_Model(), e.SysByte);
            }
        }

        private void Xcom_OnStateChangedEvent(object sender, SECS_STATE e)
        {
            if (e == SECS_STATE.SELECTED)
            {
                this.xcom.SendMsg(new S1F1_AreyouThereReq_Model());
            }
        }

        private void Xcom_OnStateChangingEvent(object sender, SECS_STATE e)
        {
            if (e == SECS_STATE.SELECTED)
            {

            }
        }

    }

    public class Test
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Test(int id)
        {
            this.ID = id;
            this.Name = string.Format("Name {0}", id);
        }

        public override string ToString()
        {
            return string.Format("ID={0} Name={1}", this.ID, this.Name);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (object.Equals(this, null) && object.Equals(obj, null)) return true;
            if (object.Equals(this, null)) return false;
            if (object.Equals(obj, null)) return false;

            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
