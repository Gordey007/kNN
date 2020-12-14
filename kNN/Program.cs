using System;
using System.Collections.Generic;
using System.Linq;

namespace kNN
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> data = new List<List<string>>();
            List<List<string>> distance = new List<List<string>>();
            List<List<string>> list = new List<List<string>>();

            List<string> triangle = new List<string>();

            List<string> classList = new List<string>();
            List<List<string>> knnList = new List<List<string>>();

            List<string> classIf = new List<string>();

            int knn, countClass, min = 0, a = 0, maxClass, classNumber = -1, options = 0;

            kNNn();

            for (int i = 0; i < list.Count-1; i++)
            {
                sorting(distance[i][0], min);
            }

            Console.Write("kNN = ");
            knn = Convert.ToInt32(Console.ReadLine());

            if ((knn % 2) != 0 && (knn != 0) && (knn > 0))
            {
                for (int i = distance.Count - knn; i < distance.Count; i++)
                {
                    knnList.Add(distance[i]);
                }
                for (int i = 0; i < classList.Count; i++)
                {
                    for (int j = 0; j < knnList.Count; j++)
                    {
                        if ((knnList[j][1]) == (classList[a]))
                        {
                            classIf[a] = (Convert.ToInt32(classIf[a]) + 1).ToString();
                        }
                    }
                    a += 1;
                }
            }
            else
            {
                Console.WriteLine("Введите другое целое число ближайших соседей, нечетное");
                knn = Convert.ToInt32(Console.ReadLine());
            }

            for (int i = 0; i < classList.Count; i++)
            {
                Console.WriteLine("Соседи " + classList[i] + " = " + classIf[i]);
            }

            maxClass = Convert.ToInt32(classIf[0]);
           
            for (int i = 0; i < classIf.Count; i++)
            {
                if (Convert.ToInt32(classIf[i]) >= maxClass)
                {
                    maxClass = Convert.ToInt32(classIf[i]);
                    classNumber = i;
                }
            }
            Console.WriteLine($"Введенный элемент - { classList[classNumber]}");


            void kNNn()
            {
                int counter = 0;
                string line;

                System.IO.StreamReader file = new System.IO.StreamReader(@"F:\kNNn.txt");
                while ((line = file.ReadLine()) != null)
                {
                    list.Add(new List<string> {line});
                    pars(counter, line);
                    counter++;
                }
                file.Close();

                Console.WriteLine("Данные");
                foreach (List<string> subList in list)
                {
                    foreach (string item in subList)
                    {
                        Console.Write(" " + item);
                    }
                    Console.WriteLine();
                }

                distanceСalculation();
                for (int i = 0; i < data[data.Count - 1].Count - 1; i++)
                {
                    if (i == 0)
                    {
                        classList.Add(data[data.Count - 1][i]);
                    }
                    if (data[data.Count - 1][i] != data[data.Count - 1][i + 1])
                    {
                        classList.Add(data[data.Count - 1][i + 1]);
                    }
                }

                countClass = classList.Count;

                for (int i = 0; i < countClass; i++)
                {
                    classIf.Add(0.ToString());
                }
            }


            void pars(int counter, string str)
            {
                string str1 = "";
                int param = 0;
                int paramStart = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        if (counter == 0)
                        {
                            data.Add(new List<string> { });
                        }
                        param++;
                    }
                }

                options = param;

                if (counter == 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (param != 0)
                        {
                            for (int j = i; j < str.Length; j++)
                            {
                                if (str[j] != ' ')
                                {
                                    str1 += str[j];
                                }
                                if (str[j] == ' ')
                                {
                                    triangle.Add(str1);
                                    str1 = null;
                                    paramStart++;
                                    i = j;
                                    break;
                                }
                            }
                            param--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (counter > 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (param != 0)
                        {
                            for (int j = i; j < str.Length; j++)
                            {
                                if (str[j] != ' ')
                                {
                                    str1 += str[j];
                                }
                                if (str[j] == ' ')
                                {
                                    data[paramStart].Add(str1);
                                    str1 = null;
                                    paramStart++;
                                    i = j;
                                    break;
                                }
                            }
                            param--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            void distanceСalculation()
            {
                for (int i = 0; i < list.Count-1; i++)
                {
                    double p = 0;
                    double sum = 0;
                    for (int j = 0; j < options - 1; j++)
                    {
                        p = Math.Pow((Convert.ToInt32(triangle[j]) - Convert.ToInt32(data[j][i])), 2);
                        sum = sum + p;
                    }

                    p = Math.Sqrt(sum);

                    distance.Add(new List<string> { });
                    distance[i].Add(p.ToString());
                    distance[i].Add(data[options - 1][i].ToString());
                }
            }


            void sorting(string kolon, int min1)
            {
                List<List<string>> temp = new List<List<string>>();
                for (int i = 0; i < distance.Count - 1; i++)
                {
                    bool f = false;
                    for (int j = 0; j < distance.Count - i - 1; j++)
                    {
                        if (Convert.ToDouble(distance[j + 1][0]) > Convert.ToDouble(distance[j][0]))
                        {
                            f = true;
                            temp.Add(distance[j + 1]);
                            distance[j + 1] = distance[j];
                            distance[j] = temp[temp.Count - 1];
                        }
                    }
                    if (!f) break;
                }
            }
        }
    }
}
