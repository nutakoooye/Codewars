/*How can you tell an extrovert from an introvert at NSA?
Va gur ryringbef, gur rkgebireg ybbxf ng gur BGURE thl'f fubrf.

I found this joke on USENET, but the punchline is scrambled. Maybe you can decipher it?
According to Wikipedia, ROT13 is frequently used to obfuscate jokes on USENET.

For this task you're only supposed to substitute characters. Not spaces, punctuation, numbers, etc.*/

/*Test examples:

"EBG13 rknzcyr." -> "ROT13 example."

"This is my first ROT13 excercise!" -> "Guvf vf zl svefg EBG13 rkprepvfr!"*/



static string Rot13(string input)
{
    var output = "";
    
    foreach (var character in input)
    {
        var newchar = character;
        if (character > 64 && character < 91)
        {
            newchar = (char)(character + 13 >= 91 ? (character + 13 - 64) % 26 + 64 : character + 13);
        }
        else if (character > 96 && character < 123)
        {
            newchar = (char)(character + 13 >= 123 ? (character + 13 - 96) % 26 + 96 : character + 13);
        }

        output += newchar;
        
    }
    return output;
}

Console.WriteLine(Rot13("MBBZ"));