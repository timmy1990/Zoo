﻿
using System.Collections.Generic;


public class fakeGPS
{
    private Dictionary<int, double[]> fakeCoordinates = new Dictionary<int, double[]>(){
       {0, new[] { 49.450199, 11.139338 }},
       {1, new[] { 49.4502189, 11.1392488 } },
       {2, new[] { 49.4501684, 11.1392541 } },
       {3, new[] { 49.4501169, 11.1392595 } },
       {4, new[] { 49.4500716, 11.1392568 } },
       {5, new[] { 49.4500254, 11.1392649 } },
       {6, new[] { 49.4499792, 11.1392572 } },
       {7, new[] { 49.4499383, 11.1392452 } },
       {8, new[] { 49.4498947, 11.1392291 } },
       {9, new[] { 49.4498397, 11.139213 } },
       {10, new[] { 49.4497909, 11.1391996 } },
       {11, new[] { 49.4497421, 11.1391808 } },
       {12, new[] { 49.4497002, 11.1391647 } },
       {13, new[] { 49.4496421, 11.139154 } },
       {14, new[] { 49.4495976, 11.1391432 } },
       {15, new[] { 49.4495479, 11.1391365 } },
       {16, new[] { 49.4495043, 11.1391285 } },
       {17, new[] { 49.4494634, 11.1391191 } },
       {18, new[] { 49.449418, 11.1391057 } },
       {19, new[] { 49.4493709, 11.139099 } },
       {20, new[] { 49.4493273, 11.1390909 } },
       {21, new[] { 49.4492872, 11.1390775 } },
       {22, new[] { 49.4492463, 11.139052 } },
       {23, new[] { 49.4491974, 11.1390547 } },
       {24, new[] { 49.4491565, 11.1390574 } },
       {25, new[] { 49.4491094, 11.139048 } },
       {26, new[] { 49.4490719, 11.1390614 } } };
       /*{27, new[] { 49.4490431, 11.1391084 } }, 
       {28, new[] { 49.4490222,11.1391754} },
       {29, new[] { 49.4489952,11.1392371 } },
       {30, new[] { 49.4489673, 11.1393082 } },
       {31, new[] {49.4489437,11.13939 } },
       {32, new[] { 49.4489352, 11.1394666 } },
       {33, new[] {49.4489282,11.1395471} },
       {34, new[] {49.4489499,11.1396318 } },
       {35, new[] { 49.4489621, 11.1397149 } },
       {36, new[] {49.4489656,11.1398007} },
       {37, new[] { 49.4489718, 11.1398937 } },
       {38, new[] { 49.4489788, 11.1399796 } },
       {39, new[] { 49.4489771,11.14006 } },
       {40, new[] { 49.4489753, 11.1401512 } },
       {41, new[] { 49.4489705,11.1402532 } },
       {42, new[] { 49.4489617, 11.1403712 } },  
       {43, new[] { 49.4489565,11.1404543 } },
       {44, new[] { 49.4489457,11.1405482} },
       {45, new[] {49.4489317,11.140626 } },
       {46, new[] { 49.4489195, 11.140693 } },
       {47, new[] { 49.4489073, 11.1407601 } },
       {48, new[] { 49.4488881,11.1408379 } },
       {49, new[] {49.4488742, 11.1408996 } },
       {50, new[] { 49.4488467, 11.140972 } },
       {51, new[] { 49.4488083,11.1410256 } },
       {52, new[] { 49.448763,11.14109} },
       {53, new[] {49.4487141,11.1411437 } },
       {54, new[] { 49.4486723,11.1411839 } },
       {55, new[] { 49.4486322,11.1412214 } },
       {56, new[] { 49.4485921,11.1412617 } },
       {57, new[] { 49.448499, 11.1413173 } },
       {58, new[] { 49.4484589,11.1413415 } },
       {59, new[] { 49.448417,11.1413576 } },
       {60, new[] { 49.4483839,11.141371} },
       {61, new[] { 49.4483542, 11.1413817 } },
       {62, new[] { 49.4483194, 11.1413898 } },
       {63, new[] { 49.4482827, 11.1413924 } },
       {64, new[] { 49.4482374, 11.1414085 } },
       {65, new[] {49.4481921,11.1414166 } },
       {67, new[] {49.4481119, 11.141407 } },
       {68, new[] { 49.448989,11.1390708} }};*/

    public double[] getGPS(int index)
    {
        double[] tmp = fakeCoordinates[index];
        return tmp;
    }

    public int getNumPoints()
    {
        return(fakeCoordinates.Count);
    }
}