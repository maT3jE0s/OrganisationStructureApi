tp.Test("Company is deleted with 204", () =>
{
    Equal(204, tp.Responses["DeleteCompany"].StatusCode());
});

tp.Test("Whole subtree (company,division, project, department) is deleted.", () =>
{
    Equal(404, tp.Responses["GetDeletedCompany"].StatusCode());
    Equal(404, tp.Responses["GetDeletedDivision"].StatusCode());
    Equal(404, tp.Responses["GetDeletedProject"].StatusCode());
    Equal(404, tp.Responses["GetDeletedDepartment"].StatusCode());
});

tp.Test("Second delete of the same company returns 404", () =>
{
    Equal(404, tp.Responses["DeleteCompanyAgain"].StatusCode());
});
