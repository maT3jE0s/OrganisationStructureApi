tp.Test("Division update is returned and persisted", () =>
{
    dynamic updated = tp.Responses["UpdateDivision"].GetBodyAsExpando();
    Equal("Software Engineering Division", (string)updated.name);
    Equal("100-11", (string)updated.code);
    Equal(tp.GetVariable<int>("SecondEmployeeId"), (int)updated.leaderId);

    dynamic fetched = tp.Responses["GetUpdatedDivision"].GetBodyAsExpando();
    Equal("Software Engineering Division", (string)fetched.name);
    Equal(tp.GetVariable<int>("SecondEmployeeId"), (int)fetched.leaderId);
});

tp.Test("Update with unknown leader is rejected", () =>
{
    Equal(400, tp.Responses["UpdateUnknownLeader"].StatusCode());
    Contains("Leader with ID 999999 not found", tp.Responses["UpdateUnknownLeader"].GetBody());
});

tp.Test("Update of a non-existing node returns 404", () =>
{
    Equal(404, tp.Responses["UpdateUnknownNode"].StatusCode());
});
