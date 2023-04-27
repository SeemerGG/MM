using RandomHelpers.Extensions;


void SimulationStanochek(int countTask)
{
    Random rnd = new Random();
    double totalWorkingTime = 0;
    double totalRepairTime = 0;
    double totalSettingTime = 0;
    double totalDownTime = 0;
    double timeToBreakdown = rnd.GetNextNormalDitributedNumber(20, 2);
    int countBreakdown = 0;
    

    for(int i = 0; i < countTask; i++)
    {
        if(totalWorkingTime >= timeToBreakdown)
        {
            double timeRepair = 0.1 + rnd.NextDouble() * (0.5 - 0.1);

            totalWorkingTime += timeRepair;
            totalRepairTime += timeRepair;

            timeToBreakdown =totalWorkingTime + rnd.GetNextNormalDitributedNumber(20, 2);//время до следующей поломки
            countBreakdown++;
        }

        double timeNextTask = totalWorkingTime + (1 - rnd.GetNextExponentialDistrubutedNumber(0, 1)); //время до следующей задачи //Math.Abs(rnd.GetNextExponentialDistrubutedNumber(1, 0));

        double settingTime = 0.2 + rnd.NextDouble() * (0.5 - 0.2);
        totalSettingTime += settingTime;
        totalWorkingTime += settingTime;

        double executeTime = rnd.GetNextNormalDitributedNumber(0.5, 0.1);
        totalWorkingTime += executeTime;

        if (totalWorkingTime < timeNextTask)
        {
            double downTime = timeNextTask - totalWorkingTime;
            totalDownTime += downTime;
            totalWorkingTime += downTime;
        }
        

    }

    Console.WriteLine(String.Format("Количество выполненых заданий: {0}", countTask));
    Console.WriteLine(String.Format("Общее время работы: {0}", Math.Round(totalWorkingTime, 2)));
    Console.WriteLine(String.Format("Общее время выполнения заданий: {0}", Math.Round(totalWorkingTime - totalRepairTime - totalDownTime, 2)));
   // Console.WriteLine(String.Format("Общее время наладки станка: {0}", Math.Round(totalSettingTime,2)));
    Console.WriteLine(String.Format("Общее время ремонта станка: {0}", Math.Round(totalRepairTime,2)));
    Console.WriteLine(String.Format("Общее время простоя: {0}", Math.Round(totalDownTime,2)));
    Console.WriteLine($"Количество поломок станка: {countBreakdown}");
}

//Random rnd = new Random();
//for (int i = 0; i < 1000; i++)
    //Console.WriteLine(Math.Round(1 - rnd.GetNextExponentialDistrubutedNumber(0, 1), 2));
    //Console.WriteLine(Math.Round(Math.Abs(rnd.GetNextExponentialDistrubutedNumber(2, 1)), 2));
SimulationStanochek(500);