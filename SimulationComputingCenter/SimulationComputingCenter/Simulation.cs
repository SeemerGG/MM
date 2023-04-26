using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationComputingCenter
{
    public class Simulation
    {
        int countTask, timeToNextTask, timeSort, timeComputing, timeErrCorection;
        double ErrProbility;

        int totalTimeWorking = 0, operatorDownTime = 0, EMC1DownTime = 0, EMC2DownTime = 0, totalTimeComputingEMC1 = 0, totalTimeComputingEMC2 = 0;
        int timeWorkingOperator = 0, totalSortTime = 0, totalTimeCorrectionErr = 0, countErr = 0, countTaskInQue = 0;
        


        public Simulation(int countTask, int timeToNextTask, int timeSort, int timeComputing, int timeErrCorection, double ErrProbility)
        {
            this.countTask = countTask;
            this.ErrProbility = ErrProbility;
            this.timeSort = timeSort;
            this.timeComputing = timeComputing;
            this.timeErrCorection = timeErrCorection;
            this.timeToNextTask = timeToNextTask;
            Simulate();
        }


        void Simulate()
        {
            Random rnd = new Random();
            int countExecuteTask = 0;
            int timeBeforNextTask = 2;
            int countTaskInQue = 0; //очередь на обработку оператором
            int timeSorting = 0;
            int timeCorrecting = 0;
            bool operatorRepairErr = false; //оператор исправляет ошибки
            bool operatorSorting = false; //оператор сортирует задачи
            int timeComputingInECM1 = 0;
            int timeComputingInECM2 = 0;
            bool EMC1Working = false;
            bool EMC2Working = false;
            int countIncomingTasks = 0;

            List<int> queErr = new List<int>(); // 1 - ЭВМ1, 2 - ЭВМ2
            List<int> queEMC1 = new List<int>(); // 0 - не обработана, 1 - была обработана
            List<int> queEMC2 = new List<int>(); // 0 - не обработана, 1 - была обработана

            do
            {
                if (timeBeforNextTask == 0)
                {
                    if (countIncomingTasks < this.countTask)
                    {
                        countTaskInQue++;
                    }
                    countIncomingTasks++;
                    timeBeforNextTask = this.timeToNextTask;
                }

                ///
                if (countTaskInQue > 0 && !operatorSorting && !operatorRepairErr)//начинает работать оператор
                {
                    timeSorting = this.timeSort;
                    countTaskInQue--;
                    operatorSorting = true;
                }

                if (timeSorting == 0 && operatorSorting && !operatorRepairErr) //оператор закончил первичную обработку задачи
                {
                    this.totalSortTime += this.timeSort;
                    operatorSorting = false;
                    if (queEMC1.Count < queEMC2.Count)
                    {
                        queEMC1.Add(0);
                    }
                    else
                    {
                        queEMC2.Add(0);
                    }
                }

                if (timeCorrecting == 0 && operatorRepairErr)
                {
                    this.totalTimeCorrectionErr += this.timeErrCorection;
                    
                    if(queErr.First() == 1)
                    {
                        EMC1Working = true;
                    }
                    else
                    {
                        EMC2Working = true;
                    }

                    queErr.RemoveAt(0);

                    operatorRepairErr = false;
                }
                ///
                //if (countTaskInQue > 0 && !operatorSorting && !operatorRepairErr)//начинает работать оператор
                //{
                //    timeSorting = this.timeSort;
                //    countTaskInQue--;
                //    operatorSorting = true;
                //}

                if (timeComputingInECM1 == 0 && EMC1Working) //вычислила задачу первая машина 
                {
                    if (queEMC1.First() == 0)
                    {
                        if (rnd.NextBool(this.ErrProbility))
                        {
                            timeComputingInECM1 += this.timeComputing;
                            EMC1Working = false;
                            queErr.Add(1);
                            //operatorRepairErr = true;
                            queEMC1[0] = 1;
                        }
                        else
                        {
                            queEMC1.RemoveAt(0);
                            countExecuteTask++;
                            EMC1Working = false;
                        }
                    }
                    else
                    {
                        if (queEMC1.First() == 1)
                        {
                            queEMC1.RemoveAt(0);
                            countExecuteTask++;
                            EMC1Working = false;
                        }
                    }
                    
                    this.totalTimeComputingEMC1 += this.timeComputing;
                }

                if (timeComputingInECM2 == 0 && EMC2Working) //вычислила задачу первая машина 
                {
                    if (queEMC2.First() == 0)
                    {
                        if (rnd.NextBool(this.ErrProbility))
                        {
                            timeComputingInECM2 += this.timeComputing;
                            EMC2Working = false;
                            queErr.Add(2);
                            //operatorRepairErr = true;
                            queEMC2[0] = 1;
                        }
                        else
                        {
                            queEMC2.RemoveAt(0);
                            countExecuteTask++;
                            EMC2Working = false;
                        }
                    }
                    else
                    {
                        if (queEMC2.First() == 1)
                        {
                            queEMC2.RemoveAt(0);
                            countExecuteTask++;
                            EMC2Working = false;
                        }
                    }
                    
                    this.totalTimeComputingEMC2 += this.timeComputing;
                }

                if(queErr.Count > 0 && !operatorRepairErr)
                {
                    timeCorrecting += this.timeErrCorection;
                    operatorRepairErr = true;
                }

                if (queEMC1.Count > 0 && !EMC1Working && queEMC1.First() == 0) // машина не работает - пусть работает
                {
                    timeComputingInECM1 = this.timeComputing;
                    EMC1Working = true;
                }

                if (queEMC2.Count > 0 && !EMC2Working && queEMC2.First() == 0)// ты тоже работай
                {
                    timeComputingInECM2 = this.timeComputing;
                    EMC2Working = true;
                }

                if (countExecuteTask == countTask)
                {
                    break;
                }

                this.totalTimeWorking++;

                timeBeforNextTask--;

                if (operatorSorting && !operatorRepairErr)
                {
                    timeSorting--;
                }

                if (operatorRepairErr)
                {
                    timeCorrecting--;
                }

                if (EMC1Working)
                {
                    timeComputingInECM1--;
                }

                if (EMC2Working)
                {
                    timeComputingInECM2--;
                }
                
            } while (true);

            this.EMC1DownTime = this.totalTimeWorking - this.totalTimeComputingEMC1;
            this.EMC2DownTime = this.totalTimeWorking - this.totalTimeComputingEMC2;
            this.timeWorkingOperator = this.totalTimeCorrectionErr + this.totalSortTime;
            this.operatorDownTime = this.totalTimeWorking - this.timeWorkingOperator;
            this.countErr = this.totalTimeCorrectionErr / this.timeErrCorection;
            this.countTaskInQue = countIncomingTasks - this.countTask;
        }

        public string GetStringInfo()
        {
            string info = $"Общее время работы: {this.totalTimeWorking} минут\nВремя работы ЭВМ1: {this.totalTimeComputingEMC1} минут\n"
                + $"Время простоя ЭВМ1: {this.EMC1DownTime} минут\nВремя работы ЭВМ2: {this.totalTimeComputingEMC2} минут\nВремя простоя ЭВМ2: {this.EMC2DownTime}\n"
                + $"Время работы оператора: {this.timeWorkingOperator} минут\nВремя коррекции ошибок оператором: {this.totalTimeCorrectionErr} минут\n"
                + $"Время регистрации и сортировки задач оператором: {this.totalSortTime} минут\nВремя ожидания оператором задач: {this.operatorDownTime} минут\n"
                + $"Количество ошибок при регистрации и сортировке: {this.countErr} штук\nКоличество заданий в очереди: {this.countTaskInQue} штук\n"
                + $"Пропускная способность: {Math.Round(this.countTask * 1.0 / this.totalTimeWorking, 2)} задач/минута\n";
            return info;
        }
    }
}
