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
   dotnet restore
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

4. **Configuration de l'API**
Après avoir ouvert le dossier vous aurez à configuerer l'API avec la base de donnée qu'il faut, vous trouverez les information requis dans les informations du serveur que vous aurez lancé sur PostgreSQL

![ConnectionString](./IntelliJ%20Snippet.png)


6. **Executer les programmes requis**
- Assurez-vous d'executer la base de donnée sur votre serveur PostgreSQL
- Assurez de lancer le Front-End sans problèmes
- Enfin lancez l'application .NET et vérifiez que les requêtes soient opérationnelles sur Swagger d'abord

## Endpoints

### Acteurs

#### Ajouter un Acteur

- **POST /actors**

  Ajoute un nouvel acteur dans le système.

  **JSON Payload:**
  ```json
  {
    "prenom": "John",
    "nom": "Doe",
    "birthdate": "1990-01-01",
    "created_date": "2023-01-01",
    "modified_date": "2023-01-01",
    "age": 33
  }
  ```
#### Supprimer un Acteur

- **DELETE /actors/{id}**

  Supprime un acteur du système en fonction de son identifiant.


## Films

#### Récupérer tous les Films

- **GET /movies**

  Récupère la liste de tous les films du système.

#### Récupérer un Film par son Identifiant

- **GET /movies/{id}**

  Récupère un film du système en fonction de son identifiant.

#### Ajouter un Nouveau Film

- **POST /movies**

  Ajoute un nouveau film dans le système.

  **JSON Payload:**

  ```json
  {
    "titre": "Inception",
    "dateSortie": 2010
  }
  ```
  
#### Mettre à Jour les Informations d'un Film

- **PUT /movies/{id}**

  Met à jour les informations d'un film dans le système.

  **JSON Payload:**

  ```json
  {
    "id": 1,
    "title": "Inception",
    "release_year": 2010,
    "created_date": "2023-01-01",
    "duration": 148
  }
  ```
## Crédits

- Front-End : Laurent Descamps
- DB : Aurore
- API : Aymeric Biaou




   
