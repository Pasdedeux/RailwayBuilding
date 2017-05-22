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
        private DataProxy _selectDataProxy, _operateDataProxy;

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

            UpdateData();
        }

        void UpdateData()
        {
            _operateDataProxy = null;
            _operateDataProxy = _selectDataProxy.Clone() as DataProxy;
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
                //房屋专业
                comboBox1.Text = _operateDataProxy.BuildingMajors;
                //房屋类型
                comboBox2.Text = _operateDataProxy.BuildingType;
                //防火等级
                comboBox3.Text = _operateDataProxy.FireLevel.ToString();
                //房屋名称
                comboBox_RoomName.Text = _operateDataProxy.BuildingName;

                //房屋面积
                textBox2.Text = _operateDataProxy.Area.ToString();
                //房屋高度
                textBox3.Text = _operateDataProxy.Height.ToString();
                //所处里程
                textBox4.Text = _operateDataProxy.Location.ToString();
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
                //是否设置空调
                CheckBoxStatus( checkBox1 , textBox1 , _operateDataProxy.AirConditioning );
                CheckBoxStatus( checkBox2 , textBox5 , _operateDataProxy.Wind );
                CheckBoxStatus( checkBox3 , textBox6 , _operateDataProxy.Firehydrant );
                CheckBoxStatus( checkBox4 , textBox7 , _operateDataProxy.GasFirehydrant );
                CheckBoxStatus( checkBox5 , textBox8 , _operateDataProxy.WaterFirehydrant );
                CheckBoxStatus( checkBox6 , textBox9 , _operateDataProxy.FireCannon );
                CheckBoxStatus( checkBox7 , textBox10 , _operateDataProxy.Extinguisher);
            }
        }

        void CheckBoxStatus( CheckBox checkBox , TextBox textBox , int? data )
        {
            checkBox.CheckState = data == null ? CheckState.Unchecked : CheckState.Checked;
            textBox.Enabled = data == null ? false : true;
            textBox.Text = data.ToString();
            
        }

        #region HVAC点击委托

        private void checkBox1_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox1.Enabled = checkBox1.CheckState == CheckState.Checked;
        }

        private void checkBox2_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox5.Enabled = checkBox2.CheckState == CheckState.Checked;
        }

        private void checkBox3_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox6.Enabled = checkBox3.CheckState == CheckState.Checked;
        }

        private void checkBox5_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox8.Enabled = checkBox5.CheckState == CheckState.Checked;
        }

        private void checkBox4_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox7.Enabled = checkBox4.CheckState == CheckState.Checked;
        }

        private void checkBox6_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox9.Enabled = checkBox6.CheckState == CheckState.Checked;
        }

        private void checkBox7_CheckedChanged( object sender , EventArgs e )
        {
            this.textBox10.Enabled = checkBox7.CheckState == CheckState.Checked;
        }
        #endregion

        private void ResetButton_Click( object sender , EventArgs e )
        {
            UpdateData();
        }

        private void SaveButton_Click( object sender , EventArgs e )
        {
            //TODO 上传数据
            //
        }
    }
}
