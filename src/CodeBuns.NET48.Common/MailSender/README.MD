# MailSender component overview
Recently, the trial period for the SMTP provider used in the project has expired, and the email sending functionality is no longer working.
To resolve this issue, I have created a new component that allows for easy configuration and use of different SMTP providers.


## Introduction

These files are designed to facilitate sending emails using various SMTP configurations and providers.
Created in the .NET Framework 4.8 environment, because of the web.config support and the need for compatibility with legacy systems.

This component provides a flexible way to configure and send emails through different SMTP providers, allowing for easy integration into .NET applications.

_**Note**: Please don't get mad at lines like `"SELECT * FROM SmtpProviders"`, such code should be replaced with yours._

## SMTP configuration

- JSON
- Web.config
- Database

 
## SMTP providers

- Gmail
- SendGrid
- Mailgun
- ...
- Any other SMTP provider that can be configured with the necessary parameters.
 
 
## How to use

1. **Configuration**: Set up your SMTP configuration in one of the supported formats.

JSON example
```json
{
  "DefaultSmtpProvider": "SendGrid",
  "SmtpProviders": [
    {
      "Name": "SendGrid",
      "Host": "smtp.sendgrid.net",
      "Port": 587,
      "EnableSsl": true,
      "Username": "sendgrid_user",
      "Password": "sendgrid_password"
    },
    {
      "Name": "Mailgun",
      "Host": "smtp.mailgun.org",
      "Port": 587,
      "EnableSsl": true,
      "Username": "mailgun_user",
      "Password": "mailgun_password",
      "Enabled": false
    }
  ]
}
```

Web.config example
```xml
<configuration>
    <configSections>
        <section name="smtpProviders" type="CodeBuns.NET48.Common.SmtpProvidersSection, CodeBuns.NET48.Common" />
    </configSections>
    <appSettings>
        <add key="DefaultSmtpProvider" value="SendGrid" />
    </appSettings>
    <smtpProviders>
        <provider name="SendGrid" host="smtp.sendgrid.net" port="587" enableSsl="true" username="sendgrid_user" password="sendgrid_password" />
        <provider name="Mailgun" host="smtp.mailgun.org" port="587" enableSsl="true" username="mailgun_user" password="mailgun_password" enabled="false" />
    </smtpProviders>
</configuration>
```

2. **Create a MailMessage**: Create an instance of `MailMessage` with the necessary details such as sender, recipient, subject, and body.
```csharp
var mailMessage = new MailMessage
{
    From = new MailAddress("noreply@example.com"),
    Subject = "Test email",
    Body = "This is a test email.",
    IsBodyHtml = true
};
mailMessage.To.Add(new MailAddress("recipient@example.com"));
```

3. **Send the email**: Use the `MailAgent` class to send the email using the configured SMTP provider.
```csharp
var smtpConfiguration = new JsonSmtpConfiguration("<file_path>");
var mailAgent = new MailAgent(smtpConfiguration);

try
{
    mailAgent.Send(mailMessage);
    Console.WriteLine("Email sent successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to send email: {ex.Message}");
}
```

`Send` method is using fallback logic to try sending the email with the next available SMTP provider if the current one fails. This allows for robust email sending capabilities, ensuring that emails can be sent even if one provider is temporarily unavailable.