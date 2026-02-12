namespace LuxonServer.Plugin;

public enum ErrorCodes : short
{
    MissingCallProcessing = 0,
    UnhandledException = 1,
    AsyncCallbackException = 2,
    SetPropertiesPreconditionsFail = 3,
    SetPropertiesCASFail = 4,
    SetPropertiesException = 5,

    ProductSpecificErrorCodes = 1000,

    UserDefinedErrorCodes = 2000
}
