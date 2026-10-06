tp.Test("Deleting a leader of an org node is rejected", () =>
{
  Equal(400, tp.Responses["DeleteSecondWhileLeader"].StatusCode());
  Contains("leader", tp.Responses["DeleteSecondWhileLeader"].GetBody());
});

tp.Test("The leader still exists after the rejected delete", () =>
{
  Equal(200, tp.Responses["GetSecondStillExists"].StatusCode());
});
