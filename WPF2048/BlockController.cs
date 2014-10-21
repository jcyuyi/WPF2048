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
        public Block[,] blocks      = new Block[4, 4];
        public Block[,] movedBlocks = new Block[4, 4];// 0 means not move 
        //new game blocks
        public void initBlocks()
        {
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                {
                    blocks[j, k] = new Block();
                    blocks[j, k].num = 0;
                }
                  
            int Line = MRandom.getRandomNumber(0, 4);
            int Row = MRandom.getRandomNumber(0, 4);
            blocks[Line, Row].num = 2;
            int a = Line;
            int b = Row;
            do
            {
                Line = MRandom.getRandomNumber(0, 4);
            } while (Line == a);
            do
            {
                Row = MRandom.getRandomNumber(0, 4);
            } while (Row == b);
            blocks[Line, Row].num = 2;

        }



        //judge if can move any block to direction
        public bool canMove(Direction dir)
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
            if (n == 1)
                for (int j = 0; j < 4; j++)
                    for (int k = 3; k >= 0; k--)
                    {
                        if (blocks[k, j].num != 0)
                        {
                            int p = k - 1;
                            for (; p >= 0; p--)
                            {
                                if (blocks[p, j].num == 0)
                                    return true;
                            }
                        }
                    }

            else if (n == 2)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if (blocks[k, j].num != 0)
                        {
                            int p = k + 1;
                            for (; p < 4; p++)
                            {
                                if (blocks[p, j].num == 0)
                                    return true;
                            }
                        }
                    }

            else if (n == 3)
                for (int j = 0; j < 4; j++)
                    for (int k = 3; k >= 0; k--)
                    {
                        if (blocks[j, k].num != 0)
                        {
                            int p = k - 1;
                            for (; p >= 0; p--)
                            {
                                if (blocks[j, p].num == 0)
                                    return true;
                            }
                        }
                    }

            else if (n == 4)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if (blocks[j, k].num != 0)
                        {
                            int p = k + 1;
                            for (; p < 4; p++)
                            {
                                if (blocks[j, p].num == 0)
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
            if (n == 1)
            {
                for (int j = 0; j < 4; j++)
                    for (int k = 3; k >= 0; k--)
                    {
                        if (blocks[k, j].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 1; p < 4; p++)
                                {
                                    if (blocks[p - 1, j].num == 0)
                                    {
                                        blocks[p - 1, j].num = blocks[p, j].num;
                                        blocks[p, j].num = 0;
                                    }
                                }
                            break;
                        }
                    }
                plusup();//here..................
            }
            else if (n == 2)
            {
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if (blocks[k, j].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 2; p >= 0; p--)
                                {
                                    if (blocks[p + 1, j].num == 0)
                                    {
                                        blocks[p + 1, j].num = blocks[p, j].num;
                                        blocks[p, j].num = 0;
                                    }
                                }
                            break;
                        }
                    }
                plusdown();//here..................
            }

            else if (n == 3)
            {

                for (int j = 0; j < 4; j++)
                    for (int k = 3; k >= 0; k--)
                    {
                        if (blocks[j, k].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 1; p < 4; p++)
                                {
                                    if (blocks[j, p - 1].num == 0)
                                    {
                                        blocks[j, p - 1].num = blocks[j, p].num;
                                        blocks[j, p].num = 0;
                                    }
                                }
                            break;
                        }
                    }
                plusleft();//here..................
            }

            else if (n == 4)
            {

                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                    {
                        if (blocks[j, k].num != 0)
                        {
                            for (int t = 0; t < 4; t++)
                                for (int p = 2; p >= 0; p--)
                                {
                                    if (blocks[j, p + 1].num == 0)
                                    {
                                        blocks[j, p + 1].num = blocks[j, p].num;
                                        blocks[j, p].num = 0;
                                    }
                                }
                            break;
                        }
                    }
                plusright();//here..................
            }
            int sum = 0;
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                {
                    if (blocks[j, k].num != 0)
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


            return 2; // 暂定每次移动合并后加 2 分
        }
        private void plusup()
        {
            for (int i = 0; i < 4; i++)
            {
                int a = 0, b = 0, c = 0, d = 0;
                if (blocks[0, i].num == blocks[1, i].num)
                {
                    a = blocks[0, i].num + blocks[1, i].num;
                    if (blocks[2, i].num == blocks[3, i].num)
                    {
                        b = blocks[2, i].num + blocks[3, i].num;
                    }
                    else
                    {
                        b = blocks[2, i].num;
                        c = blocks[3, i].num;
                    }
                }
                else
                {
                    a = blocks[0, i].num;
                    if (blocks[1, i].num == blocks[2, i].num)
                    {
                        b = blocks[1, i].num + blocks[2, i].num;
                        c = blocks[3, i].num;
                    }
                    else
                    {
                        b = blocks[1, i].num;
                        if (blocks[2, i].num == blocks[3, i].num)
                            c = blocks[2, i].num + blocks[3, i].num;
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
