using Lab4DES.Constants;
using Lab4DES.Modules;
using Lab4DES.Services;
Console.WriteLine("DES - Sarcina 2.10 (calcul Ri)");
Console.WriteLine("1 - introducere manuala");
Console.WriteLine("2 - generare aleatorie");
Console.Write("Alege: ");
string? choice = Console.ReadLine();

string sOut;
string lPrev;

if (choice == "1")
{
    sOut = InputValidator.ReadBits("S1(B1)...S8(B8) (32 biti): ", 32);
    lPrev = InputValidator.ReadBits("L(i-1) (32 biti): ", 32);
}
else
{
    sOut = BitOperations.RandomBits(32);
    lPrev = BitOperations.RandomBits(32);
    Console.WriteLine("S-out generat:  " + ConsolePrinter.Group(sOut));
    Console.WriteLine("L(i-1) generat: " + ConsolePrinter.Group(lPrev));
}

RoundResult result = DesRoundService.ComputeRi(sOut, lPrev);
ConsolePrinter.PrintRoundResult(result);