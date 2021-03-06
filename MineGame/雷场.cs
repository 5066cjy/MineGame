﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MineGame
{
    public partial class 雷场 : Panel
    {
        int _雷场上边距 = 20;
        int _雷场下边距 = 20;
        int _雷场左边距 = 20;
        int _雷场右边距 = 20;
        int _地雷列间距 = 2;
        int _地雷行间距 = 2;

        int _雷场总列数=10;
        public int 雷场总列数
        {
            get { return _雷场总列数; }
        }
        int _雷场总行数=10;
        public int 雷场总行数
        {
            get { return _雷场总行数; }
        }
        int _地雷总数=20;
        public int 地雷总数
        {
            get { return _地雷总数; }
        }
        /// <summary>
        /// 保存雷场中每个坐标点是否是地雷，或者是邻居地雷数量
        /// </summary>
        int[,] _地雷分布状态;
        public int[,] 地雷分布状态
        {
            get { return _地雷分布状态; }
        }

        public 雷场()
        {
            开始游戏();
        }
        /// <summary>
        /// 
        /// </summary>
        public void 开始游戏()
        {
            初始化雷场();
            设置有雷区域();
            设置安全区();
            布置雷场();
        }
        /// <summary>
        /// 
        /// </summary>
        void 初始化雷场()
        {
            _地雷分布状态 = new int[_雷场总列数, _雷场总行数];

            this.Size = new System.Drawing.Size(
                _雷场左边距 + (区块.长度 + _地雷列间距) * _雷场总列数 - _地雷列间距 + _雷场右边距,
                _雷场上边距 + (区块.高度 + _地雷行间距) * _雷场总行数 - _地雷行间距 + _雷场下边距);            
        }
        /// <summary>
        /// 生成地雷的坐标
        /// </summary>
        void 设置有雷区域()
        {
            Random rnd = new Random();

            for (int 计数器 = 0; 计数器 < _地雷总数; 计数器++)
            {
                int 行坐标, 列坐标;
                do
                {
                    行坐标 = rnd.Next(0, _雷场总行数);
                    列坐标 = rnd.Next(0, _雷场总列数);
                    //-1表示区域为地雷
                } while (_地雷分布状态[行坐标, 列坐标] == -1);

                _地雷分布状态[行坐标, 列坐标] = -1;
            }
        }
        /// <summary>
        /// 计算周围地雷数量
        /// </summary>
        void 设置安全区()
        {
            for (int 行坐标 = 0; 行坐标 < _雷场总行数; 行坐标++)
                for (int 列坐标 = 0; 列坐标 < _雷场总列数; 列坐标++)
                {
                    if (_地雷分布状态[行坐标, 列坐标] != -1)
                    {
                        try
                        {
                            //左上角
                            if (_地雷分布状态[行坐标 - 1, 列坐标 - 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正左边
                            if (_地雷分布状态[行坐标, 列坐标 - 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //左下角
                            if (_地雷分布状态[行坐标 + 1, 列坐标 - 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正上方
                            if (_地雷分布状态[行坐标 - 1, 列坐标] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正下方
                            if (_地雷分布状态[行坐标 + 1, 列坐标] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //右上角
                            if (_地雷分布状态[行坐标 - 1, 列坐标 + 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //正右边
                            if (_地雷分布状态[行坐标, 列坐标 + 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }
                        try
                        {
                            //右下角
                            if (_地雷分布状态[行坐标 + 1, 列坐标 + 1] == -1)
                                _地雷分布状态[行坐标, 列坐标]++;
                        }
                        catch (Exception) {  /*坐标越界*/ }

                    }
                }
        }
        /// <summary>
        /// 根据雷场状态，加载地雷控件到雷场
        /// </summary>
        void 布置雷场()
        {
            
            for (int 行坐标 = 0; 行坐标 < _雷场总行数; 行坐标++)
                for (int 列坐标 = 0; 列坐标 < _雷场总列数; 列坐标++)
                {
                    区块 _地雷;                    

                    _地雷 = _地雷分布状态[行坐标, 列坐标] == -1 ? new 区块(true,this) : new 区块(false,this);                    

                    //设置地雷在雷场中的行列位置
                    _地雷.在雷场中的行位置 = 行坐标;
                    _地雷.在雷场中的列位置 = 列坐标;

                    //设置地雷控件在雷场控件中的具体坐标
                    _地雷.Location = new System.Drawing.Point(
                        _雷场左边距 + (区块.长度 + _地雷列间距) * 列坐标,
                        _雷场上边距 + (区块.高度 + _地雷行间距) * 行坐标);

                    this.Controls.Add(_地雷);
                }
        }      
    }

}
