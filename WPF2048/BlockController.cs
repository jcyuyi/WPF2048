using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF2048
{
    class BlockController
    {
        class MRandom
        {
            private static Random random;
            public static int getRandomNumber(int from, int to)
            {
                if (random == null)
                {
                    random = new Random();
                }
                int len = to - from; //if range is 2-5 then l = 3
                int n = random.Next(len); //len is 0-2
                n = n + from;
                return n;
            }
        }
        public enum Direction { Up = 1, Down = 2, Right = 3, Left = 4 };
        public Block[,] blocks = new Block[4, 4];
        public Block[,] beforeMovedBlocks = new Block[4, 4];// 0 means not move 
        //new game blocks
        public void initBlocks()
        {
            for (int Row = 0; Row < 4; Row++)
                for (int Column = 0; Column < 4; Column++)
                {
                    blocks[Row, Column] = new Block();
                    blocks[Row, Column].num = 0;
                }
            for (int Row = 0; Row < 4; Row++)
                for (int Column = 0; Column < 4; Column++)
                {
                    beforeMovedBlocks[Row, Column] = new Block();
                    beforeMovedBlocks[Row, Column].num = 0;
                }

            int startRow = MRandom.getRandomNumber(0, 4);
            int startColumn = MRandom.getRandomNumber(0, 4);
            blocks[startRow, startColumn].num = 2;
            int secondStartRow = startRow;
            int secondStartColumn = startColumn;
            do
            {
                secondStartRow = MRandom.getRandomNumber(0, 4);
            } while (secondStartRow == startRow);
            do
            {
                secondStartColumn = MRandom.getRandomNumber(0, 4);
            } while (secondStartColumn == startColumn);
            blocks[secondStartRow, secondStartColumn].num = 2;
        }

        private void assignBlocks(ref Block a, ref Block b)// first Para is Target, second Para is source, First<=Secend
        {
            a.movesteps = b.movesteps;
            a.status = b.status;
            a.num = b.num;
            a.oldColumn = b.oldColumn;
            a.oldRow = b.oldRow;
        }

        private void clearBlocks(ref Block a) // Reset Blocks Object to 0;
        {
            a.movesteps = 0;
            a.status = 0;
            a.num = 0;
            a.oldColumn = 0;
            a.oldRow = 0;
        }

        //judge if can move any block to direction
        public bool canMove(Direction dir)
        {
            return false;
        }

        private bool canMoveLeft(Block[,] blks)
        {
            for (int row = 0; row < 4; row++)
            {
                int startCol = 0;
                int endCol = 1;
                while (startCol < 4 && endCol < 4)
                {
                    while (blks[row,endCol].num == 0 && endCol < 4)
                    {
                        endCol++;
                    }
                    while (blks[row, startCol].num == 0 && startCol < 4)
                    {
                        startCol++;
                    }
                    if (startCol >= 4 || endCol >= 4)
                    {
                        return false;
                    }
                    if (blks[row,startCol].num == blks[row,endCol].num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int move(Direction dir)
        {
            int n = 1;
            switch (dir)
            {
                case Direction.Up:
                    n = 1;
                    break;
                case Direction.Down:
                    n = 2;
                    break;
                case Direction.Right:
                    n = 4;
                    break;
                case Direction.Left:
                    n = 3;
                    break;
                default:
                    break;
            }
            for (int Row = 0; Row < 4; Row++)
                for (int Column = 0; Column < 4; Column++)
                {
                    blocks[Row, Column].movesteps = 0;
                    blocks[Row, Column].status = 0;
                    blocks[Row, Column].oldColumn = Column;
                    blocks[Row, Column].oldRow = Row;
                    assignBlocks(ref beforeMovedBlocks[Row, Column], ref blocks[Row, Column]);

                }
            if (n == 1)
            {
                for (int Row = 0; Row < 4; Row++)
                    for (int Column = 3; Column >= 0; Column--)
                    {
                        if (blocks[Column, Row].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 1; p < 4; p++)
                                {
                                    if (blocks[p - 1, Row].num == 0)
                                    {
                                        blocks[p, Row].movesteps += 1;
                                        assignBlocks(ref blocks[p - 1, Row], ref blocks[p, Row]);
                                        clearBlocks(ref blocks[p, Row]);
                                    }
                                }
                            break;
                        }
                    }
                plusup();//here..................
            }
            else if (n == 2)
            {
                for (int Row = 0; Row < 4; Row++)
                    for (int Column = 0; Column < 4; Column++)
                    {
                        if (blocks[Column, Row].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 2; p >= 0; p--)
                                {
                                    if (blocks[p + 1, Row].num == 0)
                                    {
                                        blocks[p, Row].movesteps += 1;
                                        assignBlocks(ref blocks[p + 1, Row], ref blocks[p, Row]);
                                        clearBlocks(ref blocks[p, Row]);
                                    }
                                }
                            break;
                        }
                    }
                plusdown();//here..................
            }

            else if (n == 3)
            {

                for (int Row = 0; Row < 4; Row++)
                    for (int Column = 3; Column >= 0; Column--)
                    {
                        if (blocks[Row, Column].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 1; p < 4; p++)
                                {
                                    if (blocks[Row, p - 1].num == 0)
                                    {
                                        blocks[Row, p].movesteps += 1;
                                        assignBlocks(ref blocks[Row, p - 1], ref blocks[Row, p]);
                                        clearBlocks(ref blocks[Row, p]);
                                    }
                                }
                            break;
                        }
                    }
                plusleft();//here..................
            }

            else if (n == 4)
            {

                for (int Row = 0; Row < 4; Row++)
                    for (int Column = 0; Column < 4; Column++)
                    {
                        if (blocks[Row, Column].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 2; p >= 0; p--)
                                {
                                    if (blocks[Row, p + 1].num == 0)
                                    {
                                        blocks[Row, p].movesteps += 1;
                                        assignBlocks(ref blocks[Row, p + 1], ref blocks[Row, p]);
                                        clearBlocks(ref blocks[Row, p]);
                                    }
                                }
                            break;
                        }
                    }
                plusright();//here..................
            }
            int sum = 0;
            for (int Row = 0; Row < 4; Row++)
                for (int Column = 0; Column < 4; Column++)
                {
                    if (blocks[Row, Column].num != 0)
                        sum++;
                }
            if (sum == 16)
                return -1;// game over
            int TempLine, TempRow;
            do
            {
                TempLine = MRandom.getRandomNumber(0, 4);
                TempRow = MRandom.getRandomNumber(0, 4);
            } while (blocks[TempLine, TempRow].num != 0);
            blocks[TempLine, TempRow].num = 2;

            for (int Row = 0; Row < 4; Row++)
                for (int Column = 0; Column < 4; Column++)
                {
                    int t1, t2;
                    if (blocks[Row, Column].num != 0 || blocks[Row, Column].movesteps > 0)
                    {
                        t1 = blocks[Row, Column].oldRow;
                        t2 = blocks[Row, Column].oldColumn;
                        beforeMovedBlocks[t1, t2].movesteps = blocks[Row, Column].movesteps;
                        beforeMovedBlocks[t1, t2].status = blocks[Row, Column].status;
                    }
                }
            return 2; // 暂定每次移动合并后加 2 分
        }

        private void plusup()
        {
            for (int i = 0; i < 4; i++)
            {
                int a = 0, b = 0, c = 0, d = 0;
                if (blocks[0, i].num != 0 && blocks[0, i].num == blocks[1, i].num)
                {
                    a = blocks[0, i].num + blocks[1, i].num;
                    blocks[0, i].status = 2; // new
                    blocks[1, i].status = 1; //died
                    blocks[1, i].movesteps += 1;
                    if (blocks[2, i].num != 0 && blocks[2, i].num == blocks[3, i].num)
                    {
                        b = blocks[2, i].num + blocks[3, i].num;
                        blocks[2, i].status = 2; // new
                        blocks[3, i].status = 1; //died
                        blocks[2, i].movesteps += 1;
                        blocks[3, i].movesteps += 2;
                    }
                    else
                    {
                        b = blocks[2, i].num;
                        c = blocks[3, i].num;
                        blocks[2, i].movesteps += 1;
                        blocks[3, i].movesteps += 1;
                    }
                }
                else
                {
                    a = blocks[0, i].num;
                    if (blocks[1, i].num != 0 && blocks[1, i].num == blocks[2, i].num)
                    {
                        b = blocks[1, i].num + blocks[2, i].num;
                        c = blocks[3, i].num;
                        blocks[1, i].status = 2;
                        blocks[2, i].status = 1;
                        blocks[2, i].movesteps += 1;
                        blocks[3, i].movesteps += 1;

                    }
                    else
                    {
                        b = blocks[1, i].num;
                        if (blocks[2, i].num != 0 && blocks[2, i].num == blocks[3, i].num)
                        {
                            c = blocks[2, i].num + blocks[3, i].num;
                            blocks[2, i].status = 2;
                            blocks[3, i].status = 1;
                            blocks[3, i].movesteps += 1;
                        }
                        else
                        {
                            c = blocks[2, i].num;
                            d = blocks[3, i].num;
                        }
                    }
                }
                blocks[0, i].num = a;
                blocks[1, i].num = b;
                blocks[2, i].num = c;
                blocks[3, i].num = d;

            }
        }

        private void plusdown()
        {
            for (int i = 0; i < 4; i++)
            {
                int a = 0, b = 0, c = 0, d = 0;
                if (blocks[3, i].num == blocks[2, i].num)
                {
                    a = blocks[3, i].num + blocks[2, i].num;
                    if (blocks[1, i].num == blocks[0, i].num)
                    {
                        b = blocks[1, i].num + blocks[0, i].num;
                    }
                    else
                    {
                        b = blocks[1, i].num;
                        c = blocks[0, i].num;
                    }
                }
                else
                {
                    a = blocks[3, i].num;
                    if (blocks[2, i].num == blocks[1, i].num)
                    {
                        b = blocks[2, i].num + blocks[1, i].num;
                        c = blocks[0, i].num;
                    }
                    else
                    {
                        b = blocks[2, i].num;
                        if (blocks[1, i].num == blocks[0, i].num)
                            c = blocks[1, i].num + blocks[0, i].num;
                        else
                        {
                            c = blocks[1, i].num;
                            d = blocks[0, i].num;
                        }
                    }
                }
                blocks[3, i].num = a;
                blocks[2, i].num = b;
                blocks[1, i].num = c;
                blocks[0, i].num = d;

            }
        }
        private void plusleft()
        {
            for (int i = 0; i < 4; i++)
            {
                int a = 0, b = 0, c = 0, d = 0;
                if (blocks[i, 0].num == blocks[i, 1].num)
                {
                    a = blocks[i, 0].num + blocks[i, 1].num;
                    if (blocks[i, 2].num == blocks[i, 3].num)
                    {
                        b = blocks[i, 2].num + blocks[i, 3].num;
                    }
                    else
                    {
                        b = blocks[i, 2].num;
                        c = blocks[i, 3].num;
                    }
                }
                else
                {
                    a = blocks[i, 0].num;
                    if (blocks[i, 1].num == blocks[i, 2].num)
                    {
                        b = blocks[i, 1].num + blocks[i, 2].num;
                        c = blocks[i, 3].num;
                    }
                    else
                    {
                        b = blocks[i, 1].num;
                        if (blocks[i, 2].num == blocks[i, 3].num)
                            c = blocks[i, 2].num + blocks[i, 3].num;
                        else
                        {
                            c = blocks[i, 2].num;
                            d = blocks[i, 3].num;
                        }
                    }
                }
                blocks[i, 0].num = a;
                blocks[i, 1].num = b;
                blocks[i, 2].num = c;
                blocks[i, 3].num = d;

            }
        }
        private void plusright()
        {
            for (int i = 0; i < 4; i++)
            {
                int a = 0, b = 0, c = 0, d = 0;
                if (blocks[i, 3].num == blocks[i, 2].num)
                {
                    a = blocks[i, 3].num + blocks[i, 2].num;
                    if (blocks[i, 1].num == blocks[i, 0].num)
                    {
                        b = blocks[i, 1].num + blocks[i, 0].num;
                    }
                    else
                    {
                        b = blocks[i, 1].num;
                        c = blocks[i, 0].num;
                    }
                }
                else
                {
                    a = blocks[i, 3].num;
                    if (blocks[i, 2].num == blocks[i, 1].num)
                    {
                        b = blocks[i, 2].num + blocks[i, 1].num;
                        c = blocks[i, 0].num;
                    }
                    else
                    {
                        b = blocks[i, 2].num;
                        if (blocks[i, 1].num == blocks[i, 0].num)
                            c = blocks[i, 1].num + blocks[i, 0].num;
                        else
                        {
                            c = blocks[i, 1].num;
                            d = blocks[i, 0].num;
                        }
                    }
                }
                blocks[i, 3].num = a;
                blocks[i, 2].num = b;
                blocks[i, 1].num = c;
                blocks[i, 0].num = d;

            }
        }
    }
}
