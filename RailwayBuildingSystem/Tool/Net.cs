﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

namespace Tool
{
    class Net
    {
        /// <summary>
        /// 当前程序联网状态检测，用于判断是否处于联网状态
        /// </summary>
        /// <returns></returns>
        public static bool IsConnect( )
        {
            Ping ping;
            PingReply res;
            ping = new Ping();

            try
            {
                //用百度或者任意地址检测
                res = ping.Send( "www.baidu.com" );
                DebugLog.Log( res.Address.ToString() );
                if ( res.Status != IPStatus.Success ) return false;
                else return true;
            }
            catch ( Exception e )
            {
                DebugLog.Log( e.Message );
                return false;
            }
        }


        public bool IsConnectMySql( )
        {
            string str = "Server=localhost;User ID=root;Password=;Database=rbuildingdb;CharSet=utf8";
            MySqlConnection con = new MySqlConnection( str );//实例化链接
            con.Open();//开启连接
            if ( con.State == System.Data.ConnectionState.Open )
            {
                System.Console.WriteLine( "MySql connecting success.." );
                return true;
            }
            else
            {
                System.Console.WriteLine( "MySql connecting failed.." );
                return false;
            }
        }

        /// <summary>
        /// 连接Mysql并获取数据库数据
        /// </summary>
        public DataSet ConnectMySql( )
        {
            string str = "Server=localhost;User ID=root;Password=;Database=rbuildingdb;CharSet=utf8";
            //实例化链接
            MySqlConnection con = new MySqlConnection( str );
            //开启连接
            con.Open();
            //数据库表为 staffaccount
            string strcmd = "select * from staffaccount";
            MySqlCommand cmd = new MySqlCommand( strcmd , con );
            MySqlDataAdapter ada = new MySqlDataAdapter( cmd );
            //查询结果填充数据集
            DataSet ds = new DataSet();
            ada.Fill( ds );
            //关闭连接
            con.Close();
            return ds;
        }
    }
}
