// Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
// Contact: @monambike for more information.
// For license information, please see the LICENSE file in the root directory.

using DSharpPlus;
using KWiJisho.Utils;
using Moq;
using Quartz;

namespace KWiJisho.Test
{
    public class JobTesting
    {
        [Fact]
        public async Task Test1()
        {
            var mockLogs = new Mock<Log>(); // Mocka o KWiJishoLogs para simular seu comportamento
            var mockDiscordClient = new Mock<DiscordClient>(); // Mocka o DiscordClient para simular seu comportamento
            var mockContext = new Mock<IJobExecutionContext>(); // Mocka o IJobExecutionContext para simular seu comportamento
            var dataMap = new JobDataMap
            {
                { "DiscordClient", mockDiscordClient.Object } // Configura o JobDataMap para retornar o mock do DiscordClient
            };

            mockContext.Setup(c => c.MergedJobDataMap).Returns(dataMap); // Configura o mockContext para retornar o JobDataMap 
        }
    }
}