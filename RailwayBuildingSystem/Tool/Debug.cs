using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Tool
{
    /// <summary>
    /// 日志类，用于各类日志打印
    /// </summary>
    class DebugLog
    {
        public static void Log( string content )
        {
            FileInfo fileinfo = logConditionDetection();

            //创建只写字节流
            using ( FileStream fs = fileinfo.OpenWrite() )
            {
                //根据上面的文件流创建写数据流
                StreamWriter sw = new StreamWriter( fs );

                //设置写数据流的起始位置为文件流末尾
                sw.BaseStream.Seek( 0 , SeekOrigin.End );

                //写入日志
                sw.Write( "\n\rLog Entry: " );
                sw.Write( "{0} {1} \n\r " , DateTime.Now.ToLongTimeString() , DateTime.Now.ToLongDateString() );
                sw.Write( content + "\n\r" );

                //将缓冲区内容写入文件流后清空缓冲区
                sw.Flush();
                //关闭写数据流
                sw.Close();
            }
        }

        public static void ErrorLog(string error  )
        {
            FileInfo fileinfo = logConditionDetection();

            //创建只写字节流
            using ( FileStream fs = fileinfo.OpenWrite() )
            {
                //根据上面的文件流创建写数据流
                StreamWriter sw = new StreamWriter( fs );

                //设置写数据流的起始位置为文件流末尾
                sw.BaseStream.Seek( 0 , SeekOrigin.End );

                //写入日志
                sw.Write( "\n\rError Log Entry: " );
                sw.Write( "{0} {1} \n\r " , DateTime.Now.ToLongTimeString() , DateTime.Now.ToLongDateString() );
                sw.Write( error + "\n\r" );

                //将缓冲区内容写入文件流后清空缓冲区
                sw.Flush();
                //关闭写数据流
                sw.Close();
            }
        }

        private static FileInfo logConditionDetection()
        {
            //指定日志文件目录
            string filename = Directory.GetCurrentDirectory() + "\\LogFile.txt";
            FileInfo fileinfo = new FileInfo( filename );

            //判断日志文件是否存在
            if ( !fileinfo.Exists )
            {
                FileStream fs = File.Create( filename );
                fs.Close();
                fileinfo = new FileInfo( filename );
            }

            //判断日志文件是否大于10M（字节数）
            if ( fileinfo.Length > 1024 * 1024 * 10 )
            {
                //超过了则重新建立日志文件
                File.Move( filename , Directory.GetCurrentDirectory() + DateTime.Now.TimeOfDay + "\\LogFile.txt" );
            }

            return fileinfo;
        }
    }
}
