tp.Test("Employees are deleted with 204", () =>
{
    Equal(204, tp.Responses["DeleteLeader"].StatusCode());
    Equal(204, tp.Responses["DeleteSecond"].StatusCode());
});

tp.Test("Deleted employee is no longer available", () =>
{
    Equal(404, tp.Responses["GetDeletedEmployee"].StatusCode());
    Equal(404, tp.Responses["DeleteEmployeeAgain"].StatusCode());
});
