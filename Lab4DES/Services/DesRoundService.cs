using Lab4DES.Constants;
using Lab4DES.Modules;

namespace Lab4DES.Services;

public static class DesRoundService
{
    public static RoundResult ComputeRi(string sOut, string lPrev)
    {
        string f = BitOperations.Permute(sOut, DesTables.P);
        string ri = BitOperations.Xor(lPrev, f);

        return new RoundResult
        {
            SOut = sOut,
            LPrev = lPrev,
            F = f,
            Ri = ri
        };
    }
}