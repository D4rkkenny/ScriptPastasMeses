using System;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;

namespace ScriptPastasMeses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite o caminho raiz: ");
            string rootFolderPath = Console.ReadLine();

            Console.Write("Digite o nome da pasta a ser encontrada: ");
            string targetFolderName = Console.ReadLine();

            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.txt");
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                int currentYear = DateTime.Now.Year;

                if (Directory.Exists(rootFolderPath))
                {
                    logWriter.WriteLine($"[{DateTime.Now}] Iniciando o processo no caminho: {rootFolderPath}");
                    logWriter.WriteLine($"[{DateTime.Now}] Procurando pastas com o nome '{targetFolderName}'...");

                    string[] subdirectories = Directory.GetDirectories(rootFolderPath, "*", SearchOption.AllDirectories);

                    bool folderFound = false;

                    foreach (string subdirectory in subdirectories)
                    {

                        if (Path.GetFileName(subdirectory).Equals(targetFolderName, StringComparison.OrdinalIgnoreCase))
                        {
                            folderFound = true;
                            logWriter.WriteLine($"\n[{DateTime.Now}] Pasta encontrada: {subdirectory}");

                            string yearFolder = Path.Combine(subdirectory, DateTime.Now.Year.ToString());

                            if (!Directory.Exists(yearFolder))
                            {
                                Directory.CreateDirectory(yearFolder);
                                logWriter.WriteLine($"[{DateTime.Now}] Pasta do ano criada: {yearFolder}");
                            }
                            else
                            {
                                logWriter.WriteLine($"[{DateTime.Now}] Pasta do ano já existe: {yearFolder}");
                            }


                            for (int month = 1; month <= 12; month++)
                            {
                                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                                string monthFolder = Path.Combine(yearFolder, $"{month} - {monthName}");

                                if (!Directory.Exists(monthFolder))
                                {
                                    Directory.CreateDirectory(monthFolder);
                                    logWriter.WriteLine($"[{DateTime.Now}] Subpasta criada: {monthFolder}");
                                }
                                else
                                {
                                    logWriter.WriteLine($"[{DateTime.Now}] Subpasta já existente: {monthFolder}");
                                }
                            }
                        }
                    }
                    if (!folderFound)
                    {
                        logWriter.WriteLine($"[{DateTime.Now}] Nenhuma pasta com o nome '{targetFolderName}' foi encontrada.");
                        Console.WriteLine($"Nenhuma pasta com o nome '{targetFolderName}' foi encontrada.");
                    }
                    else
                    {
                        Console.WriteLine("Processo concluido com sucesso!");
                    }
                }
                else
                {
                    logWriter.WriteLine($"[{DateTime.Now}] O caminho raiz '{rootFolderPath}' não existe.");
                    Console.WriteLine("O caminho raiz especificado não existe");
                }
            }
            Console.WriteLine($"\nOs logs foram salvos em: {logFilePath}");
            Console.WriteLine("Pressione qualquer tecla para encerrar...");
            Console.ReadKey();
        }
    }
}
