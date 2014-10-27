using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF2048
{
    class Block
    {
        public enum BlockStatus
        {
            NotMoved, Moved, Combined
        }
        public int num { set; get; }
        public int movesteps { set; get; }  //move step count
        public BlockStatus status { set; get; }     //judge the block new or die 0=>old, 1=>died, 2=>new
        public int oldRow { set; get; }     //the last Row of the block
        public int oldColumn { set; get; }  //the last Column of the block
    }
}
