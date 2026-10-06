// OrgNodeType is serialized as a number: Company=0, Division=1, Project=2, Department=3

tp.Test("Company is created without parent (type 0)", () =>
{
    dynamic body = tp.Responses["CreateCompany"].GetBodyAsExpando();
    True(body.id > 0);
    Equal("Acme Corporation", (string)body.name);
    Equal("100", (string)body.code);
    Equal(0, (int)body.type);
    tp.SetVariable("CompanyId", (int)body.id);
});

tp.Test("Division is created under the company (type 1)", () =>
{
    dynamic body = tp.Responses["CreateDivision"].GetBodyAsExpando();
    Equal(1, (int)body.type);
    Equal(tp.GetVariable<int>("CompanyId"), (int)body.parentId);
    tp.SetVariable("DivisionId", (int)body.id);
});

tp.Test("Project is created under the division (type 2)", () =>
{
    dynamic body = tp.Responses["CreateProject"].GetBodyAsExpando();
    Equal(2, (int)body.type);
    Equal(tp.GetVariable<int>("DivisionId"), (int)body.parentId);
    tp.SetVariable("ProjectId", (int)body.id);
});

tp.Test("Department is created under the project (type 3)", () =>
{
    dynamic body = tp.Responses["CreateDepartment"].GetBodyAsExpando();
    Equal(3, (int)body.type);
    Equal(tp.GetVariable<int>("ProjectId"), (int)body.parentId);
    tp.SetVariable("DepartmentId", (int)body.id);
});

tp.Test("GET by id returns the created nodes", () =>
{
    dynamic company = tp.Responses["GetCompany"].GetBodyAsExpando();
    Equal("Acme Corporation", (string)company.name);

    dynamic department = tp.Responses["GetDepartment"].GetBodyAsExpando();
    Equal("Backend Department", (string)department.name);
});

tp.Test("Division list contains the created division and only divisions", () =>
{
    var body = tp.Responses["ListDivisions"].GetBody();
    Contains("Software Division", body);
    DoesNotContain("Acme Corporation", body);
});
