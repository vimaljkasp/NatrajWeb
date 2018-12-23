using Newtonsoft.Json;
using Platform.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

public class ResponseWrappingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //Step 1: Wait for the Response
        var response = await base.SendAsync(request, cancellationToken);

        return BuildApiResponse(request, response);
    }

    private HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
   {
        object content;
        string modelStateErrors = string.Empty;
        bool status = false;

        //Step 2: Get the Response Content
        if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
        {
            
            HttpError error = content as HttpError;
            status = false;
            if (error != null)
            {
                //Step 3: If content is an error, return nothing for the Result.
                content = new object(); //We have errors, so don't return any content
                //Step 4: Insert the ModelState errors              
                if (error.ModelState != null)
                {
                    //Read as string
                    var httpErrorObject = response.Content.ReadAsStringAsync().Result;

                    //Convert to anonymous object
                    var anonymousErrorObject = new { message = "", ModelState = new Dictionary<string, string[]>() };

                    // Deserialize anonymous object
                    var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

                    // Get error messages from ModelState object
                    var modelStateValues = deserializedErrorObject.ModelState.Select(kvp => string.Join(". ", kvp.Value));

                    
                        modelStateErrors=modelStateValues.ElementAt(0);
                    
                }
                else
                {
                    modelStateErrors=error.FirstOrDefault().Value.ToString();
                }
            }
        }
        else
        {
            ResponseDTO responseDTO = content as ResponseDTO;
            if(responseDTO !=null)
            {
                status = responseDTO.Status;
                modelStateErrors = responseDTO.Message;
                content = responseDTO.Data;
            }
            else {
                status = true;
                modelStateErrors = "Request Successfull";
                content = new object();
            }
            

        }

        //Step 5: Create a new response
        var newResponse = request.CreateResponse(response.StatusCode, new ResponsePackage(content, modelStateErrors,status));

        //Step 6: Add Back the Response Headers
        foreach (var header in response.Headers) //Add back the response headers
        {
            newResponse.Headers.Add(header.Key, header.Value);
        }

        return newResponse;
    }




    public class ResponsePackage
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public ResponsePackage(object result, string errors,bool responseStatus)
        {
            status = responseStatus;
            message = errors;
            data = result;
           
        }
    }
}