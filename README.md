# Movies API

Cette API permet la gestion des acteurs et des films dans un système de gestion de films.

## Table des matières

- [Création et Configuration du Projet](#création-et-configuration-du-projet)
- [Endpoints](#endpoints)
  - [Acteurs](#acteurs)
    - [Ajouter un Acteur](#ajouter-un-acteur)
    - [Supprimer un Acteur](#supprimer-un-acteur)
  - [Films](#films)
    - [Récupérer tous les Films](#récupérer-tous-les-films)
    - [Récupérer un Film par son Identifiant](#récupérer-un-film-par-son-identifiant)
    - [Ajouter un Nouveau Film](#ajouter-un-nouveau-film)
    - [Mettre à Jour les Informations d'un Film](#mettre-à-jour-les-informations-dun-film)

## Création et Configuration du Projet

### Prérequis

Assurez-vous d'avoir installé les outils suivants sur votre machine :

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Étapes de Configuration

1. **Cloner le Projet**

   ```bash
   git clone https://github.com/MarsaColombo/MovieAPI.git
   cd MoviesAPI
   ```
2. **Cloner la DataBase**

    ```bash
    git clone https://github.com/MarsaColombo/MoviesDB.git
    ```
    
3. **Cloner le Front**

    ```bash
    git clone https://github.com/simplon-lille-csharp-dotnet/MovieReactFront.git
    npm i
    npm run dev
    ```

4. **Executer les programmes requis**
- Assurez-vous d'executer la base de donnée sur votre serveur PostgreSQL
- Assurez de lancer le Front-End sans problèmes
- Enfin lancez l'application .NET et vérifiez que les requêtes soient opérationnelles sur Swagger d'abord




   
