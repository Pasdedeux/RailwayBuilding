﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayBuildingSystem
{
    public partial class Main : Form
    {
        private BuildingAndConstructionWindow _buildWindow;
        private HVACWindow _hvacWindow;

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

                //TODO 
                //可以通过这种方式输出需要的数据
                var list = ds.Tables[ 0 ].CreateDataReader();
                while ( list.Read() )
                {
                    Console.WriteLine( list[ "Password" ] );
                }
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

        private void LinkToolStripMenuItem_Click( object sender , EventArgs e )
        {
            Console.WriteLine( "点击链接" );
        }

    }
}
