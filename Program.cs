int kumulacja;
int start = 20;
Random generator = new Random();
int saldo = start;
int czas = 0;
do
{
    saldo = start;
    czas = 0;
    ConsoleKey wybor;
    do
    {
        kumulacja = generator.Next(2, 37) * 1000000;
        czas++;
        int los = 0;
        List<int[]> kupon = new List<int[]>();
        do
        {
            Console.Clear();
            Console.WriteLine("Dzień: {0}",czas);
            Console.WriteLine("Dzisiejsza wygrana wynosi {0} zł \n",kumulacja);
            Console.WriteLine("Stan Konta: {0}zł\n \n", saldo);
            Wkupin(kupon);

            if (saldo >= 2 && los < 8)
            {
                Console.WriteLine("1 - Postaw los 2 zł [{0}/8]", los + 1);
            }
            Console.WriteLine("2 - Sprawdź kupon");
            Console.WriteLine("3 - Zakoncz grę");
           
            wybor = Console.ReadKey().Key;
            if(wybor == ConsoleKey.D1 && saldo >=2 && los < 8)
            {
                kupon.Add(PostawLos());
                saldo -= 2;
                los++;
            }

        } while (wybor == ConsoleKey.D1);
        Console.Clear();
        if(kupon.Count > 0)
        {
            int wygrana = sprawdz(kupon);
            if(wygrana > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Brawo wygrałeś {0}zł",wygrana);
                Console.ResetColor();
                saldo += wygrana;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n niestety nic wygrałeś");
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine("nie grałeś w tym losowaniu");
        }
        Console.WriteLine("enter kontynuuj.");
        Console.ReadKey();

    } while (saldo >= 3 && wybor != ConsoleKey.D3);



    Console.Clear();
    Console.WriteLine("Dzień {0}. \nKoniec gry, twój wynik to: {1} zł",czas,saldo - start);
    Console.WriteLine("Enter - Nowa gra");
} while (Console.ReadKey().Key == ConsoleKey.Enter);

int sprawdz(List<int[]> kupon)
{
    int wygrana = 0;
    int[] LiczbyLotka =new int[6];
    for (int i = 0; i < LiczbyLotka.Length; i++)
    {
        int los = generator.Next(1, 50);
        if (!LiczbyLotka.Contains(los))
        {
            LiczbyLotka[i] = los;
        }
        else
        {
            i--;
        }

    }
    Array.Sort(LiczbyLotka);
    Console.WriteLine("Wylosowane liczby to: ");
    foreach (int liczba in LiczbyLotka)
    {
        Console.Write(liczba + ", ");
    }
    int[] trafione = SprawdzKupon(kupon, LiczbyLotka);

    int wartosc = 0;
    Console.WriteLine();
    if(trafione[0] >0)
    {
        wartosc = trafione[0] * 24;
        Console.WriteLine("3 Trafienia: {0} +{1}zł",trafione[0],wartosc);
        wygrana += wartosc;
    }
    if (trafione[1] > 0)
    {
        wartosc = trafione[1] * generator.Next(100,300);
        Console.WriteLine("3 Trafienia: {0} +{1}zł", trafione[1], wartosc);
        wygrana += wartosc;
    }
    if (trafione[2] > 0)
    {
        wartosc = trafione[2] * generator.Next(4000,8001);
        Console.WriteLine("3 Trafienia: {0} +{1}zł", trafione[2], wartosc);
        wygrana += wartosc;
    }
    if (trafione[3] > 0)
    {
        wartosc = (trafione[3] * kumulacja)/ (trafione[3] + generator.Next(0,5));
        Console.WriteLine("3 Trafienia: {0} +{1}zł", trafione[3], wartosc);
        wygrana += wartosc;
    }

    return wygrana;
}

int[] SprawdzKupon(List<int[]> kupon, int[] liczbyLotka)
{
    int[] wygrane = new int[4];
    int i = 0;
    Console.WriteLine("\n\n Twój kupon");
    foreach(int[] los in kupon)
    {
        i++;
        Console.WriteLine(i + ", ");
        int trafien = 0;
        foreach(int liczba in los) 
        {
            if(liczbyLotka.Contains(liczba))
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write(liczba + ", ");
                Console.ResetColor();
                trafien++;
            }
            else
            {
                Console.Write(liczba + ", ");
            }
        }
        switch (trafien)
        {
            case 3:
                wygrane[0]++;
            break;
            case 4:
                wygrane[1]++;
            break;
            case 5:
                wygrane[2]++;
            break;
            case 6:
                wygrane[3]++;
            break;
        }
        Console.WriteLine(" - Trafiono {0}/6",trafien);
    }
    return wygrane;
}

int[] PostawLos()
{
    int[] liczby = new int[6];
    int liczba = -1;
    for (int i = 0; i < liczby.Length; i++)
    {
        liczba = -1;
        Console.Clear();
        Console.Write("Postawione liczby: ");
        foreach (int l in liczby)
        {
            if(l > 0 )
            {
                Console.Write(l + ", ");
            }
        }
        Console.WriteLine("\n \n Wybierz liczbę od 1 do 49: ");
        Console.Write("{0}/6: ", i+1);
        bool prawidlowa = int.TryParse(Console.ReadLine(), out liczba);
        if(prawidlowa && liczba >= 1 && liczba <= 49 && !liczby.Contains(liczba))
        {
            liczby[i]= liczba; 
        }
        else
        {
            Console.WriteLine("Niestety. błędna liczba.");
            i--;
            Console.ReadKey();
        }
        
    }
    Array.Sort(liczby);
    return liczby;
}

void Wkupin(List<int[]> kupon)
{
    if(kupon.Count == 0)
    {
        Console.WriteLine("Brak losów.");
    }
    else
    {
        int i = 0;
        Console.WriteLine("Twój kupon:");
        foreach (int[] losy in kupon)
        {
            i++;
            Console.Write(i + ": ");
            foreach (int liczba in losy)
            {
                Console.Write(liczba + ", ");
            }
            Console.WriteLine();

        }
    }
}