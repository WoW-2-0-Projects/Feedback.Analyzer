﻿using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Services;

/// <summary>
/// Defines a contract for services that generate and validate passwords.
/// </summary>
public interface IPasswordGeneratorService
{
    /// <summary>
    /// Generates a new, random password.
    /// </summary>
    /// <returns>A string representing the generated password.</returns>
    string GeneratePassword();

    /// <summary>
    /// Validates a password against specific criteria.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <param name="client"></param>
    /// <returns>
    /// A string indicating the validation result. This might be a success message or an error message specifying the reason for failure.
    /// </returns>
    string GetValidatedPassword(string password, Client client);
}