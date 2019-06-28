# Azure Functions Dynamic SendGrid API Key

This project demonstrates how to dynamically assign a SendGrid API key by using [runtime binding](https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library#binding-at-runtime).

## Setup

Clone this repository.

```bash
git clone https://github.com/craigshoemaker/azure-functions-dynamic-sendgrid-api-key.git
```

Open in Visual Studio Code:

```bash
code ./azure-functions-dynamic-sendgrid-api-key
```

### Update local.settings.json

To run locally, you can use the following example *local.settings.json*. Be sure to swap out all placeholders for your values.

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "<AZURE_STORAGE_CONNECTION_STRING>",
        "SendGridAPIKey1": "<SENDGRID_API_KEY_1>",
        "SendGridAPIKey2": "<SENDGRID_API_KEY_1>",
        "EmailSender": "<SENDER_EMAIL_ADDRESS>",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
}
```

## Run the function

The function accepts querystring parameters for:

- the `to` email address
- the associated SendGrid API `key`

Start debugging and send a request to the following URL. Make sure you update the querystring values with your desired values.

```
http://localhost:7071/api/SendMessage?to=person@test.com&key=SendGridAPIKey1
```

> Note: The *run.http* file in the project works well with the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) Visual Studio Code extension.

## Remarks

This project is meant for instructional purposes only.

In a production context, you likely do not want to pass the SendGrid API key name with the request. In the real world you would probably determine the API key by doing a look up based on the recipient's email address or pass in some other data to help determine the API key within the function.