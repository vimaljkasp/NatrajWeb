using System.Collections.Generic;

internal class ResponsePackage
{
    private object content;
    private List<string> modelStateErrors;

    public ResponsePackage(object content, List<string> modelStateErrors)
    {
        this.content = content;
        this.modelStateErrors = modelStateErrors;
    }
}