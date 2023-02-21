namespace EasyRent.Identity.Requests;

public record LoginAccountRequest(string EmailAddress, string Password);