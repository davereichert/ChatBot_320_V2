
using Moq;

using ChatBot_320;

public class BotResponseManagerTests
{
    private readonly Mock<IResponseLoader> mockResponseLoader;
    private readonly Mock<IMessageShower> mockMessageShower;
    private readonly string testFilePath = "botResponses.json";

    public BotResponseManagerTests()
    {
        mockResponseLoader = new Mock<IResponseLoader>();
        mockMessageShower = new Mock<IMessageShower>();
    }

    [Fact]
    public void GetResponse_WithKnownKeyword_ReturnsExpectedResponse()
    {
        // Arrange
        var knownKeyword = "passwort";
        var knownResponse = "Wenn Sie Ihr Passwort vergessen haben, nutzen Sie bitte die Passwort vergessen Funktion oder wenden Sie sich an den Administrator.";
        var keywordResponses = new List<KeywordResponse>
    {
        new KeywordResponse { Keyword = knownKeyword, Response = knownResponse }
    };
        mockResponseLoader.Setup(x => x.LoadResponses(testFilePath)).Returns(keywordResponses);

        var botManager = new BotResponseManager(mockResponseLoader.Object, mockMessageShower.Object, testFilePath);

        // Act
        var response = botManager.GetResponse(knownKeyword);

        // Assert
        Assert.Equal(knownResponse, response);
    }

    [Fact]
    public void GetResponse_WithUnknownKeyword_ReturnsDefaultResponse()
    {
        // Arrange
        var unknownKeyword = "unknown";
        var keywordResponses = new List<KeywordResponse>();
        mockResponseLoader.Setup(x => x.LoadResponses(testFilePath)).Returns(keywordResponses);

        var botManager = new BotResponseManager(mockResponseLoader.Object, mockMessageShower.Object, testFilePath);

        // Act
        var response = botManager.GetResponse(unknownKeyword);

        // Assert
        Assert.Equal("Entschuldigung, ich habe das nicht verstanden.", response);
    }

    // Weitere Tests können hier hinzugefügt werden...
}
