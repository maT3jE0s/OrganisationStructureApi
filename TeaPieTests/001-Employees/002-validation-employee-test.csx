tp.Test("Empty body reports every required field", () =>
{
    var body = tp.Responses["EmptyBody"].GetBody();
    Contains("Name is required", body);
    Contains("Surname is required", body);
    Contains("Phone is required", body);
    Contains("Email is required", body);
    Contains("Degree is required", body);
});

tp.Test("Invalid e-mail is rejected", () =>
{
    Contains("Email must be a valid email address", tp.Responses["InvalidEmail"].GetBody());
});

tp.Test("Name with digits is rejected", () =>
{
    Contains("Name must contain only letters", tp.Responses["InvalidName"].GetBody());
});

tp.Test("Phone with letters is rejected", () =>
{
    Contains("Phone must be a valid phone number", tp.Responses["InvalidPhone"].GetBody());
});

tp.Test("Too long phone is rejected", () =>
{
    Contains("Phone must be at most 15 characters long", tp.Responses["TooLongPhone"].GetBody());
});

tp.Test("Too long degree is rejected", () =>
{
    Contains("Degree must be at most 10 characters long", tp.Responses["TooLongDegree"].GetBody());
});

tp.Test("Update with empty name is rejected", () =>
{
    Contains("Name is required", tp.Responses["UpdateInvalid"].GetBody());
});

tp.Test("Unknown employee returns 404 on GET and PUT", () =>
{
    Contains("not found", tp.Responses["GetUnknown"].GetBody());
});
