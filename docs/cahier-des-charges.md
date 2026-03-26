
# 📋 Cahier des Charges — SGS-Togo
## Système de Gestion Scolaire du Togo

**Version :** 1.0
**Date :** 26 mars 2026
**Auteur :** jeandocker19

---

## 1. 🌍 Présentation du projet

### 1.1 Contexte
Le système éducatif togolais repose encore largement sur des processus manuels :
gestion papier des inscriptions, suivi des notes sur des cahiers, absence de
communication numérique entre écoles et parents, paiements non traçables.

### 1.2 Problèmes identifiés
- ❌ Inscriptions et dossiers élèves gérés sur papier (risque de perte)
- ❌ Notes calculées manuellement (erreurs fréquentes)
- ❌ Bulletins écrits à la main (temps considérable)
- ❌ Parents non informés en temps réel (absences, résultats)
- ❌ Paiements scolaires non traçables
- ❌ Aucune statistique disponible pour le suivi

### 1.3 Solution proposée
**SGS-Togo** est une plateforme web de gestion scolaire permettant de digitaliser
l'ensemble des processus scolaires des écoles togolaises (primaire, collège, lycée).

### 1.4 Objectifs
1. Digitaliser la gestion des inscriptions et dossiers élèves
2. Automatiser le calcul des notes, moyennes et classements
3. Générer automatiquement les bulletins scolaires en PDF
4. Permettre le paiement des frais scolaires via Mobile Money (T-Money, Flooz)
5. Notifier les parents par SMS (absences, résultats, paiements)
6. Fournir des statistiques et tableaux de bord aux directeurs d'écoles

---

## 2. 👥 Utilisateurs et rôles

### 2.1 Profils utilisateurs

| Rôle | Description | Accès |
|------|------------|-------|
| **Administrateur système** | Gère la plateforme globale | Tout |
| **Directeur d'école** | Supervise son école, valide les bulletins | Son école uniquement |
| **Surveillant** | Gère absences, discipline, vie scolaire | Son école (toutes les classes) 
| **Enseignant** | Gère ses classes et saisit les notes | Ses classes uniquement |
| **Parent/Tuteur** | Suit son enfant (notes, absences, paiements) | Son/ses enfant(s) uniquement 
| **Élève** | Consulte ses informations | Ses propres données |

### 2.2 Règles d'accès
- Chaque école = un tenant isolé (les données d'une école sont invisibles pour les autres)
- Un enseignant ne voit que les classes qui lui sont affectées
- Un parent ne voit que les données de son/ses enfant(s)

---

## 3. 📦 Fonctionnalités détaillées

### 3.1 Module Gestion des écoles (SchoolManagement) ✅ Fait
- Créer/modifier/supprimer une école
- Gérer les classes (CP1 → Terminale)
- Gérer les matières et coefficients
- Gérer les années scolaires

### 3.2 Module Gestion des élèves (StudentManagement) 🔨 En cours
- Inscrire un nouvel élève (avec matricule unique)
- Modifier les informations d'un élève
- Affecter un élève à une classe
- Transférer un élève vers une autre école
- Consulter le dossier complet d'un élève
- Lier un parent/tuteur à un élève

### 3.3 Module Gestion des enseignants (TeacherManagement) 📋 À faire
- Enregistrer un enseignant
- Affecter un enseignant à une ou plusieurs classes/matières
- Consulter l'emploi du temps d'un enseignant

### 3.4 Module Gestion des notes (GradeManagement) 📋 À faire
- Saisir les notes par matière et par évaluation
- Types d'évaluation : Devoir, Interrogation, Examen
- Calcul automatique :
  - Moyenne par matière
  - Moyenne générale
  - Rang dans la classe
- Règles de calcul :
  - Moyenne pondérée par coefficient
  - Mention : Très Bien (≥16), Bien (≥14), Assez Bien (≥12), Passable (≥10)
  - Décision : Admis (≥10), Redouble (<10)

### 3.5 Module Gestion des absences (AttendanceManagement) 📋 À faire
- Pointer les présences/absences par jour et par classe
- Enregistrer les retards
- Justifier une absence
- Notifier automatiquement le parent par SMS après une absence

### 3.6 Module Bulletins scolaires (dans GradeManagement) 📋 À faire
- Générer un bulletin PDF par élève et par trimestre
- Contenu du bulletin :
  - Informations de l'école et de l'élève
  - Notes par matière (devoir, examen, moyenne, coefficient)
  - Moyenne générale, rang, mention
  - Appréciations de l'enseignant principal
  - Signature du directeur
- Téléchargeable par le parent et le directeur

### 3.7 Module Paiements (PaymentManagement) 📋 À faire
- Définir les frais de scolarité par classe et par année
- Enregistrer un paiement (montant, date, mode)
- Intégration Mobile Money :
  - T-Money (Togocom)
  - Flooz (Moov)
- Générer un reçu de paiement en PDF
- Envoyer des rappels de paiement par SMS
- Suivre les impayés

### 3.8 Module Emploi du temps (ScheduleManagement) 📋 À faire
- Créer un emploi du temps par classe
- Créneaux : Lundi → Vendredi, 07h00 → 17h00
- Éviter les conflits (enseignant dans 2 classes en même temps)
- Consultable par enseignant, élève et parent

---

## 4. ⚙️ Contraintes techniques

### 4.1 Stack technique
| Composant | Choix |
|-----------|-------|
| Backend | .NET 10 / ASP.NET Core |
| Base de données | PostgreSQL |
| Architecture | Modular Monolith (Fullstack Hero) |
| API | REST + Swagger |
| PDF | QuestPDF |
| SMS | Twilio |
| Paiement | API T-Money / Flooz |
| Déploiement | Docker |

### 4.2 Contraintes spécifiques au Togo
- **Connectivité faible** : l'API doit être légère, réponses rapides
- **Mobile-first** : la majorité des utilisateurs accéderont via smartphone
- **SMS plutôt qu'email** : beaucoup de parents n'ont pas d'email
- **Langue** : français (langue officielle du Togo)
- **Mobile Money** : principal moyen de paiement numérique

### 4.3 Sécurité
- Authentification JWT avec refresh token
- Mots de passe hashés (bcrypt)
- Données isolées par tenant (multi-école)
- Audit complet (qui fait quoi et quand)
- Soft delete (données jamais supprimées définitivement)

### 4.4 Performance
- Pagination sur toutes les listes
- Cache Redis pour les données fréquemment consultées
- Compression des réponses API

---

## 5. 📊 Niveaux scolaires supportés

| Cycle | Niveaux | Âge typique |
|-------|---------|-------------|
| Primaire | CP1, CP2, CE1, CE2, CM1, CM2 | 6-11 ans |
| Collège | 6ème, 5ème, 4ème, 3ème | 12-15 ans |
| Lycée | 2nde, 1ère, Terminale | 16-18 ans |

---

## 6. 📅 Planning prévisionnel

| Phase | Module | Durée estimée | Statut |
|-------|--------|--------------|--------|
| Phase 1 | SchoolManagement | 2 semaines | ✅ Fait |
| Phase 2 | StudentManagement | 2 semaines | 🔨 En cours |
| Phase 3 | TeacherManagement | 2 semaines | 📋 À faire |
| Phase 4 | GradeManagement | 3 semaines | 📋 À faire |
| Phase 5 | AttendanceManagement | 2 semaines | 📋 À faire |
| Phase 6 | Bulletins PDF | 2 semaines | 📋 À faire |
| Phase 7 | PaymentManagement | 3 semaines | 📋 À faire |
| Phase 8 | ScheduleManagement | 2 semaines | 📋 À faire |
| Phase 9 | Tests et corrections | 2 semaines | 📋 À faire |
| Phase 10 | Déploiement pilote | 1 semaine | 📋 À faire |

**Durée totale estimée : ~5 mois**

---

## 7. 🎯 Critères de réussite

- [ ] Un directeur peut inscrire un élève en moins de 2 minutes
- [ ] Un enseignant peut saisir les notes d'une classe en moins de 15 minutes
- [ ] Un bulletin PDF est généré en moins de 5 secondes
- [ ] Un parent reçoit un SMS dans les 5 minutes après une absence
- [ ] Un paiement Mobile Money est confirmé en moins de 30 secondes
- [ ] Le système supporte au moins 10 écoles simultanément

---

## 8. 🚀 Évolutions futures (V2)

- Application mobile (Android) pour les parents
- Mode hors-ligne avec synchronisation
- Tableau de bord statistique pour le ministère de l'éducation
- Support des langues locales (Éwé, Kabyè)
- Intégration avec les examens nationaux (CEPD, BEPC, BAC)
