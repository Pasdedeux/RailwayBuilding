using System;
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
            if ( Tool.Net.IsConnect() )
            {
                System.Console.WriteLine( "Net is connecting" );
            }
            
        }

        private void LinkToolStripMenuItem_Click( object sender , EventArgs e )
        {
            Console.WriteLine("点击链接");
        }

        private void BuildAndConstructToolStripMenuItem_Click( object sender , EventArgs e )
        {
            _buildWindow = new BuildingAndConstructionWindow();
            _buildWindow.Show();
        }

        private void HVACToolStripMenuItem_Click( object sender , EventArgs e )
        {
            _hvacWindow = new HVACWindow();
            _hvacWindow.Show();
        }
    }
}
