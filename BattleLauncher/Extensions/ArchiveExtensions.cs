using System.IO;
using System.Text;
using System.Threading.Tasks;
using OpenMcdf;
using BattleLauncher.ViewModels;

namespace BattleLauncher.Extensions
{
    public static class ArchiveExtensions
    {
        private static string GetGameSavedName(this Stream stream)
        {
            var cf = new CompoundFile(stream);
            var archiveNameBytes = cf.RootStorage.GetStream("Scenario Description").GetData();
            var archiveName = Encoding.Unicode.GetString(archiveNameBytes);
            archiveName = archiveName.TrimEnd(new char[] { '\0' });
            return archiveName;
        }

        public static SavedGameViewModel GetSavedGameInfo(this FileInfo file) => new SavedGameViewModel
        {
            Name = file.OpenRead().GetGameSavedName(),
            Time = file.LastWriteTime,
            RealFile = file
        };
        public static async Task WriteSpawnAsync(this SavedGameViewModel vm, TextWriter writer)
        {
            await writer.WriteLineAsync(new StringBuilder()
                .AppendLine(";generated by Singleplayer Campaign Launcher")
                .AppendLine("[Settings]")
                .AppendLine("Scenario=spawnmap.ini")
                .Append("SaveGameName=")
                .AppendLine(vm.RealFile.Name)
                .AppendLine("LoadSaveGame=Yes")
                .AppendLine("SidebarHack=False")
                .AppendLine("Firestorm=No")
                .AppendLine("GameSpeed=2")
                .ToString());
            await writer.FlushAsync();
        }
    }
}