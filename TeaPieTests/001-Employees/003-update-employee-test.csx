tp.Test("PUT returns the updated employee", () =>
{
    dynamic body = tp.Responses["UpdateSecond"].GetBodyAsExpando();
    Equal("PhD.", (string)body.degree);
    Equal("Horvathova", (string)body.surname);
    Equal("eva.horvathova@example.com", (string)body.email);
});

tp.Test("Update is persisted", () =>
{
    dynamic body = tp.Responses["GetUpdated"].GetBodyAsExpando();
    Equal("Horvathova", (string)body.surname);
    Equal("+421911555666", (string)body.phone);
});
