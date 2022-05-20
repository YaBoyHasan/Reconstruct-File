using System.Text;
using XSystem.Security.Cryptography;

MultipleFiles2SingleFile(@"C:\Users\muham\Downloads\MCMS Practical Test (C#)\MCMS Practical Test\Split File",
    "final");

/// <summary>
/// Method to merge files into single file
/// </summary>
static void MultipleFiles2SingleFile(string inputDir, string outputDir)
{
    // max file parts
    int MaxFileNum = 77;
    // min file parts
    int MinFileNum = 1;

    // outputstreamm from output file
    var outputStream = File.Create(outputDir);
    // close stream
    outputStream.Close();
    // loop over each file part
    for (int i = MinFileNum; i < MaxFileNum + 1; i++)
    {
        // file path for split file
        var inputFile = inputDir + "/part_" + i;
        // read all bytes from split file
        byte[] bytes = File.ReadAllBytes(inputFile);

        // output bytes to final file
        using (var stream = new FileStream(outputDir, FileMode.Append))
        {
            // write bytes to final file
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    // output the file hash 
    Console.WriteLine($"Output file hash {ComputeSHA1Hash("final")}");
}

/// <summary>
/// Method to compute the SHA1 hash for file
/// </summary>
static string ComputeSHA1Hash(string inputFile)
{
    // output sha1 hash
    string output = string.Empty;
    // use file stream to read file
    using (FileStream fs = new FileStream(inputFile, FileMode.Open))
    // use buffered stream to read bytes in file
    using (BufferedStream bs = new BufferedStream(fs))
    {
        // use sha1 manager to compute file hash
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            // get the hash for the buffered stream
            byte[] hash = sha1.ComputeHash(bs);
            // build the sha1 hash as string
            StringBuilder formatted = new StringBuilder(2 * hash.Length);
            // foreach byte in hash append the byte to string builder to create sha1 hash
            foreach (byte b in hash)
            {
                formatted.AppendFormat("{0:X2}", b);
            }
            // set output string to the computed sha1 hash for file
            output = formatted.ToString();
        }
    }
    // return hash
    return output;
}