
namespace FSH.Modules.StudentManagement.Contracts.DTOs;

public sealed record EleveDto(
    Guid Id,
    string Matricule,
    string Nom,
    string Prenom,
    DateOnly DateNaissance,
    string? LieuNaissance,
    string Sexe,
    string Statut,
    string? Telephone,
    string? Email,
    string? NomParent,
    string? TelephoneParent
);