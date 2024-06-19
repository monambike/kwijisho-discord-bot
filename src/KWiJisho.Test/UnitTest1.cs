using DSharpPlus;
using KWiJisho.Scheduling;
using KWiJisho.Utils;
using Moq;
using Quartz;

namespace KWiJisho.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var mockLogs = new Mock<KWiJishoLog>(); // Mocka o KWiJishoLogs para simular seu comportamento
            var mockDiscordClient = new Mock<DiscordClient>(); // Mocka o DiscordClient para simular seu comportamento
            var mockContext = new Mock<IJobExecutionContext>(); // Mocka o IJobExecutionContext para simular seu comportamento
            var dataMap = new JobDataMap
            {
                { "DiscordClient", mockDiscordClient.Object } // Configura o JobDataMap para retornar o mock do DiscordClient
            };

            mockContext.Setup(c => c.MergedJobDataMap).Returns(dataMap); // Configura o mockContext para retornar o JobDataMap mockado

            var job = new BirthdayJob
            {
                KWiJishoLogs = mockLogs.Object,
                DiscordClient = mockDiscordClient.Object
            };

            // Act
            await job.Execute(mockContext.Object); // Executa o método a ser testado, passando o mock do contexto

            // Assert
            mockLogs.Verify(l => l.AddInfoAsync(KWiJishoLog.Module.Birthday, "Executing birthday job."), Times.Once); // Verifica se o método de log foi chamado corretamente
            mockLogs.Verify(l => l.AddInfoAsync(KWiJishoLog.Module.Birthday, "Finished birthday job."), Times.Once); // Verifica se o método de log foi chamado corretamente
            mockDiscordClient.Verify(client => client.GiveBirthdayMessage(), Times.Once); // Verifica se o método GiveBirthdayMessage foi chamado

        }
    }
}