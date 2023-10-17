using System;
using System.Collections.Generic;
using Xunit;

namespace ChatBot_320.Tests
{
    public class BotResponseManagerTests
    {
        /// <summary>
        /// Testet, ob die richtige Antwort zurückgegeben wird, wenn der Eingabe in den vorhandenen Antworten enthalten ist(Keyword & Response nicht vorhanden).
        /// </summary>
        [Fact]
        public void GetResponse_WhenInputContainedInResponses_ReturnsExpectedResponse()
        {
            // Arrange
            BotResponseManager.LoadResponses();
            var userInput = "hello";

            // Act
            var response = BotResponseManager.GetResponse(userInput);

            // Assert
            Assert.Equal("Hi there!", response);
        }

        /// <summary>
        /// Testet, ob die Standardantwort zurückgegeben wird, wenn der Eingabe nicht in den vorhandenen Antworten enthalten ist.
        /// </summary>
        [Fact]
        public void GetResponse_WhenInputNotContainedInResponses_ReturnsDefaultResponse()
        {
            // Arrange
            BotResponseManager.LoadResponses();
            var userInput = "random input";

            // Act
            var response = BotResponseManager.GetResponse(userInput);

            // Assert
            Assert.Equal("Entschuldigung, ich habe das nicht verstanden.", response);
        }
    }
}
