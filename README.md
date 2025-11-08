# Bank Account System — OOAD + Entity Framework Core (​.NET 8)

A clean, educational .NET 8 solution that models a mini banking domain using **Object-Oriented Analysis & Design** and **Entity Framework Core**.  
It includes account types with reward-points logic, unique account number generators, validation/crypto helpers, and LINQ-based reporting services over a relational schema.

![OOAD Class Diagram](images/alltypes.png)
![Bank DB Diagram](images/banksystem.png)

---

## Table of Contents
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Diagrams](#diagrams)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Usage Examples](#usage-examples)
  - [Domain (OOAD)](#domain-ooad)
  - [Services (EF Core + LINQ)](#services-ef-core--linq)
- [Project Structure](#project-structure)
- [Entity Framework](#entity-framework)
- [Troubleshooting](#troubleshooting)
- [FAQ](#faq)
- [Roadmap](#roadmap)
- [License](#license)

---

## Key Features

- **Banking domain model**
  - `BankAccount` (abstract) with concrete types:
    - `StandardAccount` — no overdraft, balance-based points
    - `SilverAccount` — overdraft tied to bonus points, balance + amount points
    - `GoldAccount` — larger overdraft, ceiling-based points
  - `AccountOwner` with multiple accounts
  - `AccountCashOperation` for deposits/withdrawals (audit trail)

- **Reward points engine**
  - Overridden per account type:
    - `CalculateDepositRewardPoints(amount)`
    - `CalculateWithdrawRewardPoints(amount)`

- **Unique number generation**
  - `IUniqueNumberGenerator` contract
  - `GuidGenerator` (GUID-based)
  - `BasedOnTickUniqueNumberGenerator` (tick/time-based hashed)
  - `SimpleGenerator` (Singleton, sequential, hashed)

- **Validation & crypto helpers**
  - `ValidatorService` — currency & email validation
  - `CryptoHelper.GenerateHash(string)` — MD5 (demo only)

- **EF Core data models & services**
  - Entities configured with **Data Annotations**
  - LINQ projections:
    - `AccountService.GetBankAccountsFullInfo()` → `BankAccountFullInfoModel`
    - `OwnerService.GetAccountOwnersTotalBalance()` → `AccountOwnerTotalBalanceModel` (sorted desc, includes decimal↔double conversions as required)

---

## Architecture

- **Domain layer** — OOAD types, rules, and behaviors (no EF dependencies)
- **DAL layer** — EF Core entities, DbContext, mappings (Data Annotations)
- **Services layer** — LINQ queries that project to read models (DTOs)
- **Images/docs** — diagrams and assignment docs

> Detailed assignments:
> - [OOAD description](/ooad.md)  
> - [Entity Framework Core description](/entity-framework-core.md)

---

## Diagrams

- **OOAD Class Diagram**: `images/alltypes.png`  
- **Database Schema**: `images/banksystem.png`

Both are included for quick orientation of types and relationships.

## Structure

BankSystemWithEF/
├─ BankSystem.Domain/            # OOAD domain classes & helpers
│  ├─ Accounts/                  # BankAccount + Standard/Silver/Gold
│  ├─ Generators/                # IUniqueNumberGenerator & implementations
│  └─ Services/                  # ValidatorService, CryptoHelper
├─ BankSystem.DAL/               # EF Core infrastructure
│  ├─ Entities/                  # Entity classes (Data Annotations)
│  ├─ BankSystemDbContext.cs     # DbContext
│  └─ Migrations/                # EF migrations (if present)
├─ BankSystem.Services/          # LINQ projections + services
│  ├─ Models/                    # BankAccountFullInfoModel, AccountOwnerTotalBalanceModel
│  └─ Services/                  # AccountService, OwnerService
├─ images/                       # Diagrams (OOAD & DB)
│  ├─ alltypes.png
│  └─ banksystem.png
├─ ooad.md                       # OOAD assignment spec (repo doc)
└─ entity-framework-core.md      # EF Core assignment spec (repo doc)

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/) installed
- SQL provider (SQL Server / SQLite / LocalDB) and a matching connection string

### Restore & Build
```bash
dotnet restore
dotnet build -c Release
