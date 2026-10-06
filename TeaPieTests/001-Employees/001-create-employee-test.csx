tp.Test("Employee is created with 201 and Location header", () =>
{
    dynamic body = tp.Responses["CreateEmployee"].GetBodyAsExpando();
    True(body.id > 0);
    Equal("Ing.", (string)body.degree);
    Equal("Jan", (string)body.name);
    Equal("Novak", (string)body.surname);
    Equal("+421900111222", (string)body.phone);
    Equal("jan.novak@example.com", (string)body.email);

    tp.SetVariable("EmployeeId", (int)body.id);
});

tp.Test("Second employee is created and its id is stored", () =>
{
    dynamic body = tp.Responses["CreateSecondEmployee"].GetBodyAsExpando();
    True(body.id > 0);
    tp.SetVariable("SecondEmployeeId", (int)body.id);
});

tp.Test("GET by id returns the same employee that was created", () =>
{
    dynamic created = tp.Responses["CreateEmployee"].GetBodyAsExpando();
    dynamic fetched = tp.Responses["GetEmployee"].GetBodyAsExpando();
    Equal(200, tp.Responses["GetEmployee"].StatusCode());
    Equal((string)created.email, (string)fetched.email);
    Equal((string)created.surname, (string)fetched.surname);
});

tp.Test("Employee list contains both created employees", () =>
{
    var body = tp.Responses["ListEmployees"].GetBody();
    Contains("jan.novak@example.com", body);
    Contains("eva.kovacova@example.com", body);
});
