using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TF_discreta
{
    class MatrizQ
    {
        private int num;
        private int[][] matrix;
        private int nhab;
        private int ai = 0;
        private int[] arr;
        public MatrizQ(int nu)
        {
            num = nu;
            arr = new int[num];
            for (int i = 0; i < num; i++)
            {
                arr[i] = -1;
            }
            matrix = new int[num][];
            for (int i = 0; i < num; i++)
            {
                matrix[i] = new int[num];
            }
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (i == j)
                    {
                        matrix[i][j] = 1;//refelxiva
                    }
                    else
                        matrix[i][j] = 0;
                }
            }


        }
        public int getN() { return num; }
        public int getP(int i, int j) { return matrix[i][j]; }
        public int[][] returnMa() { return matrix; }
        public void setP(int i, int j, int value) //simetrica
        {
            matrix[i][j] = value;
            matrix[j][i] = value;
        }
        public void Print()//no sirve porque en android no se ve xD
        {
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }
        public void setMat(int[][] mat) { matrix = mat; }
        public void Calc()
        {
            nhab = 0;
            while (true)
            {

                for (int aj = ai; aj < num; aj++)
                {

                    if (getP(aj, ai) == 1)
                    {

                        bool add = true;

                        for (int i = 0; i < num; i++)
                        {

                            if (arr[i] == nhab && getP(aj, i) == 0)
                                add = false;
                        }

                        if (add && arr[aj] == -1)
                            arr[aj] = nhab;
                    }
                }

                nhab++;
                bool stop = true;
                for (int i = 0; i < num; i++)
                {

                    if (arr[i] == -1)
                    {
                        ai = i;
                        i = num;
                        stop = false;
                    }
                }
                if (stop)
                {
                    break;
                }
            }
        }
        public void Ordenar()
        {
            // int aux2;
            // for (int i = 0; i < num; i++)
            // {
            //     for (int j = 0; j < num; j++)
            //     {
            //         if (arr[i] < arr[j])
            //         {
            //             aux2 = arr[i];
            //             arr[i] = arr[j];
            //             arr[j] = aux2;
            //         }
            //     }
            //
            // }
        }
        public int getHab() { return nhab; }
        public int[] getArr() { return arr; }
        public int getPos(int i) { return arr[i] + 1; }
    }

}