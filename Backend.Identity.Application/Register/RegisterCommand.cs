using System.Text.Json.Serialization;
using Backend.Identity.Application.Common.Abstractions.Messaging;

namespace Backend.Identity.Application.Register;

public sealed record RegisterCommand(
    string Login,
    string Password
) : ICommand<RegisterResult>;