namespace CalculatorSolution
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            for (int i = 1; i < 10; i++)
            {
                OperationChunk chunk = new OperationChunk();
                chunk.SolvedString = $"{i}={i}";
                
                Utility.History.Add(chunk);
            }

            OperationChunk chunk1 = new OperationChunk();
            chunk1.SolvedString="4+5=9";
            chunk1.SolvedValue = 9;
            Utility.Memory.Add("M3", chunk1);

            OperationChunk chunk2 = new OperationChunk();
            chunk2.SolvedString = "2+3=5";
            chunk2.SolvedValue = 5;
            Utility.Memory.Add("M7",chunk2);

            OperationChunk chunk3 = new OperationChunk();
            chunk3.SolvedString = "6*2=12";
            chunk3.SolvedValue = 12;
            Utility.Memory.Add("M1", chunk3);

            ApplicationConfiguration.Initialize();
            Application.Run(new MainCalculator());
        }
    }
}