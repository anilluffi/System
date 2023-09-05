using System;
fconColor();
Console.WriteLine("Hello");

Random random = new Random();
while (true) 
{
    try 
    {
        List<int> list = new List<int>();
        Task startTask = new Task(() => Console.WriteLine("\nstart task"));
        Task lastTask = startTask
        .ContinueWith(t => fill(list))
        .ContinueWith(t => Show(list))
        .ContinueWith(t => RemoveDuplicate(list))
        .ContinueWith(t => Show(list))
        .ContinueWith(t => { list.Sort(); })
        .ContinueWith(t => Show(list))
        .ContinueWith(t => Find(list));

        startTask.Start();
        lastTask.Wait();
    }
    catch(Exception ex)
    { Console.WriteLine(ex.Message); }
}

void fconColor()
{ Console.ForegroundColor = ConsoleColor.Cyan; }

void Show(List<int> list)
{
    try 
    {
        for (int i = 0; i < list.Count; i++)
        { Console.Write(list[i] + " "); }
        Console.WriteLine();
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
    
}
List<int> fill(List<int> list)
{
    
    try
    {
        for (int i = 0; i < random.Next(1, 20); i++)
        { list.Add(random.Next(0, 10)); }
        return list;
    }
    catch (Exception ex)
    { 
        Console.WriteLine(ex.Message); 
        return list; 
    }
}
void RemoveDuplicate(List<int> list)
{
    try
    {
        HashSet<int> seen = new HashSet<int>();
        int index = 0;

        while (index < list.Count)
        {
            int item = list[index];
            if (seen.Contains(item))
            {
                list.RemoveAt(index);
            }
            else
            {
                seen.Add(item);
                index++;
            }
        }
    }
    catch (Exception ex)
    { Console.WriteLine(ex.Message); }

}


void Find(List<int> list)
{
    try
    {
        Console.Write("Введите значение для поиска: ");
        if (int.TryParse(Console.ReadLine(), out int valueToFind))
        {
            int index = BinarySearch(list, valueToFind);

            if (index != -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Значение {valueToFind} найдено с индексом {index}.");
                fconColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Значение {valueToFind} не найдено в списке.");
                fconColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Некорректный ввод. Введите целое число.");
            fconColor();

        }
    }
    catch (Exception ex)
    { Console.WriteLine(ex.ToString()); }
    


}
int BinarySearch(List<int> list, int value)
{
    try 
    {
        int left = 0;
        int right = list.Count - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;

            if (list[middle] == value)
            {
                return middle;
            }
            else if (list[middle] < value)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        return -1; // Возвращаем -1, если значение не найдено в списке
    }
    catch (Exception ex)
    { 
        Console.WriteLine(ex.Message);
        return -1000;
    }
    
}