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
        private List<DataProxy> _dataProxyList = new List<DataProxy>();

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
            _dataProxyList.Add( dataProxy );
        }
    }
}
