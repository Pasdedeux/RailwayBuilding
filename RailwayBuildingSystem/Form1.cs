using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace RailwayBuildingSystem
{
    using Tool;
    public partial class Main : Form
    {
        private BuildingAndConstructionWindow _buildWindow;
        private HVACWindow _hvacWindow;
        private bool _isInit = true;
        //private List<DataProxy> _dataProxyList = new List<DataProxy>();
        private DataProxy _selectDataProxy;

        public Main( )
        {
            InitializeComponent();
        }

        private void Form1_Load( object sender , EventArgs e )
        {
            LoadingDetect();
        }

        private void LoadingDetect( )
        {
            if ( Tool.Net.IsConnect() && Tool.Net.IsConnectMySql() )
            {
                System.Console.WriteLine( "Net is connecting" );

                DataSet ds = Tool.Net.ConnectMySql();
                dataBaseViewer.DataSource = ds.Tables[ 0 ];
                dataBaseViewer.ClearSelection();
                _isInit = false;

            }
            else
            {
                MessageBoxButtons okButton = MessageBoxButtons.OK;
                MessageBoxButtons retryButton = MessageBoxButtons.RetryCancel;
                DialogResult dr = MessageBox.Show( "网络问题无法连接数据库" , "无法连接" , okButton | retryButton );

                if ( dr == DialogResult.Cancel )
                {
                    Application.Exit();
                }
                else//如果点击“重试”按钮
                {
                    LoadingDetect();
                }
            }
        }

        /// <summary>
        /// 根据选择初始化选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataBaseViewer_CellContentChange( object sender , EventArgs e )
        {
            if ( _isInit ) return;

            var collection = dataBaseViewer.SelectedRows[0].Cells;

            DataProxy dataProxy = new DataProxy();
            PropertyInfo[] propArray = dataProxy.GetType().GetProperties();
            for ( int i = 0 ; i < propArray.Length ; i++ )
            {
                propArray[ i ].SetValue( dataProxy , collection[ propArray[ i ].Name ].Value == DBNull.Value ? null : collection[ propArray[ i ].Name ].Value );
            }
            //_dataProxyList.Add( dataProxy )
            _selectDataProxy = dataProxy;

            switch ( specializeTab.SelectedIndex )
            {
                case 0:             //房建专业
                    UpdateTabPage0();
                    break;
                case 1:             //暖通专业
                    UpdateTabPage1();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化房建专业
        /// </summary>
        void UpdateTabPage0( )
        {
            //已选择一条现有数据
            if ( _selectDataProxy !=null )
            {
                DataProxy dp = _selectDataProxy.Clone() as DataProxy;
                //房屋专业
                comboBox1.Text = dp.BuildingMajors;
                //房屋类型
                comboBox2.Text = dp.BuildingType;
                //防火等级
                comboBox3.Text = dp.FireLevel.ToString();
                //房屋名称
                comboBox_RoomName.Text = dp.BuildingName;

                //房屋面积
                textBox2.Text = dp.Area.ToString();
                //房屋高度
                textBox3.Text = dp.Height.ToString();
                //所处里程
                textBox4.Text = dp.Location.ToString();
            }
        }

        /// <summary>
        /// 初始化暖通专业
        /// </summary>
        void UpdateTabPage1()
        {
            //已选择一条现有数据
            if ( _selectDataProxy != null )
            {
                DataProxy dp = _selectDataProxy.Clone() as DataProxy;
                //是否设置空调
                CheckBoxStatus( checkBox1 , textBox1 );
                checkBox1.CheckState = dp.AirConditioning == null ? CheckState.Unchecked : CheckState.Checked;
                textBox1.Text = dp.AirConditioning.ToString();
            }
        }

        void CheckBoxStatus(CheckBox checkBox, TextBox textBox)
        {

        }

    }
}
