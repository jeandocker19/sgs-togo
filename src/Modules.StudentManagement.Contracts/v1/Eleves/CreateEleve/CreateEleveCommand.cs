using FSH.Modules.StudentManagement.Contracts.DTOs;
using Mediator;

namespace FSH.Modules.StudentManagement.Contracts.v1.Eleves.CreateEleve;

public sealed record CreateEleveCommand(
 string Matricule,
    string Nom,
    string Prenom,
    DateOnly DateNaissance,
    string? LieuNaissance,
    string Sexe,
    string Statut,
    string? Adresse,
    string? Telephone,
    string? Email,
    string? NomParent,
    string? TelephoneParent) : ICommand<Guid>;


