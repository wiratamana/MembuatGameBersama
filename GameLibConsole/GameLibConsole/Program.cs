// See https://aka.ms/new-console-template for more information
using System.Globalization;
using GameCore;

Console.WriteLine("Hello, World!");
GameManager.Print();

Console.WriteLine(Get3BitValue(0b11100100, 2, 0b11));

int Get3BitValue(byte inputByte, int offset, int mask)
{
    // Shift the byte to the right by the offset to align the desired 3 bits to the rightmost position,
    // then apply the mask to get only those 3 bits.
    int result = (inputByte >> offset) & mask;

    return result;
}